using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player.Components;
using Project.Input.InputHandler.Markers;
using Project.Modules;
using System;

namespace Project.Core.Features.Player.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class HandleInputSystem : ISystem, IUpdate
	{
		public World world { get; set; }
		private Filter _playerFilter, _weaponFilter;

		private RPCId _forward, _backward, _left, _right, _mouseLeft, _mouseRight, _lockDirection;

		void ISystemBase.OnConstruct()
		{
			var net = world.GetModule<NetworkModule>();
			net.RegisterObject(this);
			RegisterRPSs(net);

			Filter.Create("Filter-Players")
				.With<PlayerTag>()
				.Push(ref _playerFilter);

            Filter.Create("Weapon-Filter")
                .With<WeaponTag>()
                .Push(ref _weaponFilter);
        }

		private void RegisterRPSs(NetworkModule net)
		{
			_forward = net.RegisterRPC(new Action<ForwardMarker>(ForwardKey_RPC).Method);
			_backward = net.RegisterRPC(new Action<BackwardMarker>(BackwardKey_RPC).Method);
			_left = net.RegisterRPC(new Action<LeftMarker>(LeftKey_RPC).Method);
			_right = net.RegisterRPC(new Action<RightMarker>(RightKey_RPC).Method);
			_mouseLeft = net.RegisterRPC(new Action<MouseLeftMarker>(LeftMouse_RPC).Method);
			_mouseRight = net.RegisterRPC(new Action<MouseRightMarker>(RightMouse_RPC).Method);
			_lockDirection = net.RegisterRPC(new Action<LockDirectionMarker>(SpaceKey_RPC).Method);
		}

		void ISystemBase.OnDeconstruct() {}

		void IUpdate.Update(in float deltaTime)
		{ 
			if(world.GetMarker(out ForwardMarker fm))
			{
				var net = world.GetModule<NetworkModule>();
				net.RPC(this, _forward, fm);
			}

			if (world.GetMarker(out BackwardMarker bm))
			{
				var net = world.GetModule<NetworkModule>();
				net.RPC(this, _backward, bm);
			}
			
			if (world.GetMarker(out LeftMarker lm))
			{
				var net = world.GetModule<NetworkModule>();
				net.RPC(this, _left, lm);
			}
			
			if (world.GetMarker(out RightMarker rm))
			{
				var net = world.GetModule<NetworkModule>();
				net.RPC(this, _right, rm);
			}
			
			if (world.GetMarker(out MouseLeftMarker mlm))
			{
				var net = world.GetModule<NetworkModule>();
				net.RPC(this, _mouseLeft, mlm);
			}
			
			if (world.GetMarker(out MouseRightMarker mrm))
			{
				var net = world.GetModule<NetworkModule>();
				net.RPC(this, _mouseRight, mrm);
			}

			if (world.GetMarker(out LockDirectionMarker sm))
			{
				var net = world.GetModule<NetworkModule>();
				net.RPC(this, _lockDirection, sm);
			}
		}
		private void ForwardKey_RPC(ForwardMarker fm)
		{
			foreach (var entity in _playerFilter)
			{
				if(entity.Read<PlayerTag>().PlayerID != fm.ActorID) return;

				entity.Get<MoveInput>().Axis = fm.Axis;

				switch (fm.State)
				{
					case InputState.Pressed:
					{
							entity.Get<MoveInput>().Value.y += 1;
							break;
					}
					case InputState.Released:
					{
							entity.Get<MoveInput>().Value.y -= 1;
							break;
					}
				}
			}
		}

		private void BackwardKey_RPC(BackwardMarker bm)
		{
			foreach (var entity in _playerFilter)
			{
				if (entity.Read<PlayerTag>().PlayerID != bm.ActorID) return;

				entity.Get<MoveInput>().Axis = bm.Axis;

				switch (bm.State)
				{
					case InputState.Pressed:
					{
							entity.Get<MoveInput>().Value.y -= 1;
							break;
					}
					case InputState.Released:
					{
							entity.Get<MoveInput>().Value.y += 1;
							break;
					}
				}
			}
		}

		private void LeftKey_RPC(LeftMarker lm)
		{
			foreach (var entity in _playerFilter)
			{
				if (entity.Read<PlayerTag>().PlayerID != lm.ActorID) return;

				entity.Get<MoveInput>().Axis = lm.Axis;

				switch (lm.State)
				{
					case InputState.Pressed:
					{
						entity.Get<MoveInput>().Value.x -= 1;
						break;
					}
					case InputState.Released:
					{
						entity.Get<MoveInput>().Value.x += 1;
						break;
					}
				}
			}
		}
		private void RightKey_RPC(RightMarker rm)
		{
			foreach (var entity in _playerFilter)
			{
				if (entity.Read<PlayerTag>().PlayerID != rm.ActorID) return;

				entity.Get<MoveInput>().Axis = rm.Axis;

				switch (rm.State)
				{
					case InputState.Pressed:
					{
						entity.Get<MoveInput>().Value.x += 1;
						break;
					}
					case InputState.Released:
					{
						entity.Get<MoveInput>().Value.x -= 1;
						break;
					}
				}
			}
		}

		private void LeftMouse_RPC(MouseLeftMarker mlm)
		{
			foreach (var entity in _weaponFilter)
			{
				if(mlm.ActorID != entity.Read<WeaponTag>().ActorID) return;
				
				switch (mlm.State)
				{
					case InputState.Pressed:
					{
						if (entity.Read<WeaponTag>().Hand == WeaponHand.Left)
						{
							entity.Set(new WeaponShot());
						}
						
						break;
					}
					case InputState.Released:
					{
						if (entity.Read<WeaponTag>().Hand == WeaponHand.Left)
						{
							entity.Remove<WeaponShot>();
							entity.Remove<LinearActive>();
						}
						break;
					}
				}
			}
		}

		private void RightMouse_RPC(MouseRightMarker mrm)
		{
			foreach (var entity in _weaponFilter)
			{
				if(mrm.ActorID != entity.Read<WeaponTag>().ActorID) return;

				switch (mrm.State)
				{
					case InputState.Pressed:
					{
						if (entity.Read<WeaponTag>().Hand == WeaponHand.Right)
							entity.Set(new WeaponShot());
						break;
					}
					case InputState.Released:
					{
						if (entity.Read<WeaponTag>().Hand == WeaponHand.Right)
							entity.Remove<WeaponShot>();
						break;
					}
				}
			}
		}

		private void SpaceKey_RPC(LockDirectionMarker sm)
		{
			foreach (var entity in _playerFilter)
			{
				if (entity.Read<PlayerTag>().PlayerID != sm.ActorID) return;

				switch (sm.State)
				{
					case InputState.Pressed:
					{
						entity.Set(new LockTarget());
						break;
					}
					case InputState.Released:
					{
						entity.Remove<LockTarget>();
						break;
					}
				}
			}
		}
	}
}