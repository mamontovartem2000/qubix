using ME.ECS;
using Project.Common.Components;
using Project.Input.InputHandler.Markers;
using System;
using Project.Modules.Network;

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

		public NetworkModule Net;

		private PlayerFeature _feature;

		private Filter _playerFilter;

		private RPCId _forward, _backward, _left, _right, _mouseLeft, _mouseRight, _lockDirection;
		private RPCId _firstSkill, _secondSkill, _thirdSkill, _fourthSkill;

		void ISystemBase.OnConstruct()
		{
			Net = world.GetModule<NetworkModule>();

			this.GetFeature(out _feature);
			// var net = world.GetModule<NetworkModule>();
			Net.RegisterObject(this);
			RegisterRPSs(Net);

			Filter.Create("Filter-Players")
				.With<PlayerTag>()
				.Push(ref _playerFilter);
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

			_firstSkill = net.RegisterRPC(new Action<FirstSkillMarker>(FirstSkill_RPC).Method);
			_secondSkill = net.RegisterRPC(new Action<SecondSkillMarker>(SecondSkill_RPC).Method);
			_thirdSkill = net.RegisterRPC(new Action<ThirdSkillMarker>(ThirdSkill_RPC).Method);
			_fourthSkill = net.RegisterRPC(new Action<FourthSkillMarker>(FourthSkill_RPC).Method);
		}

		void ISystemBase.OnDeconstruct() {}

		void IUpdate.Update(in float deltaTime)
		{
			if (world.GetMarker(out ForwardMarker fm)) Net.RPC(this, _forward, fm);
			if (world.GetMarker(out BackwardMarker bm)) Net.RPC(this, _backward, bm);
			if (world.GetMarker(out LeftMarker lm)) Net.RPC(this, _left, lm);
			if (world.GetMarker(out RightMarker rm)) Net.RPC(this, _right, rm);
			
			if (world.GetMarker(out MouseLeftMarker mlm)) Net.RPC(this, _mouseLeft, mlm);
			if (world.GetMarker(out MouseRightMarker mrm)) Net.RPC(this, _mouseRight, mrm);
			
			if (world.GetMarker(out LockDirectionMarker sm)) Net.RPC(this, _lockDirection, sm);
			
			if(world.GetMarker(out FirstSkillMarker first)) Net.RPC(this, _firstSkill, first);
			if(world.GetMarker(out SecondSkillMarker second)) Net.RPC(this, _secondSkill, second);
			if(world.GetMarker(out ThirdSkillMarker third)) Net.RPC(this, _thirdSkill, third);
			if(world.GetMarker(out FourthSkillMarker fourth)) Net.RPC(this, _fourthSkill, fourth);
		}

		private void ForwardKey_RPC(ForwardMarker fm)
		{
			var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value;

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

		private void BackwardKey_RPC(BackwardMarker bm)
		{
			var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value;

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

		private void LeftKey_RPC(LeftMarker lm)
		{
			var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value;

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

		private void RightKey_RPC(RightMarker rm)
		{
			var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value;

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

		private void LeftMouse_RPC(MouseLeftMarker mlm)
		{
			var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value.Get<WeaponEntities>().LeftWeapon;
			
			switch (mlm.State)
			{
				case InputState.Pressed:
				{
					entity.Set(new LeftWeaponShot());
					entity.Set(new MeleeActive());
					break;
				}
				case InputState.Released:
				{
					entity.Remove<LeftWeaponShot>();
					entity.Remove<LinearActive>();
					break;
				}
			}
		}

		private void RightMouse_RPC(MouseRightMarker mrm)
		{
			var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value.Get<WeaponEntities>().RightWeapon;

			switch (mrm.State)
			{
				case InputState.Pressed:
				{
					entity.Set(new RightWeaponShot());
					break;
				}
				case InputState.Released:
				{
					entity.Remove<RightWeaponShot>();
					break;
				}
			}
		}

		private void SpaceKey_RPC(LockDirectionMarker sm)
		{
			var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value;

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

		private void FirstSkill_RPC(FirstSkillMarker fsm)
		{
			var player = _feature.GetPlayerByID(NetworkData.OrderId);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value.Get<SkillEntities>().FirstSkill;
			
			entity.SetOneShot(new ActivateSkill());
		}
		
		private void SecondSkill_RPC(SecondSkillMarker ssm)
		{
			var player = _feature.GetPlayerByID(NetworkData.OrderId);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value.Get<SkillEntities>().SecondSkill;

			entity.SetOneShot(new ActivateSkill());
		}
		
		private void ThirdSkill_RPC(ThirdSkillMarker tsm)
		{
			var player = _feature.GetPlayerByID(NetworkData.OrderId);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value.Get<SkillEntities>().ThirdSkill;

			entity.SetOneShot(new ActivateSkill());
		}
		
		private void FourthSkill_RPC(FourthSkillMarker fsm)
		{
			var player = _feature.GetPlayerByID(NetworkData.OrderId);
			if (!player.Has<PlayerAvatar>()) return;

			ref var entity = ref player.Get<PlayerAvatar>().Value.Get<SkillEntities>().FourthSkill;

			entity.SetOneShot(new ActivateSkill());
		}
	}
}