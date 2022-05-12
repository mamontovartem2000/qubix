using ME.ECS;
using Microsoft.Win32;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.GameState.Components;
using Project.Core.Features.Player;
using Project.Core.Features.SceneBuilder;
using Project.Mechanics.Features.VFX;
using UnityEngine;

namespace Project.Mechanics.Features.Avatar.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class PlayerMovementSystem : ISystemFilter
	{
		private PlayerFeature _feature;
		private SceneBuilderFeature _scene;
		private VFXFeature _vfx;

		public World world { get; set; }

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			world.GetFeature(out _vfx);
			world.GetFeature(out _scene);
		}
		void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;
		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }

		Filter ISystemFilter.CreateFilter()
		{
			return Filter.Create("Filter-PlayerMovementSystem")
				.WithoutShared<GameFinished>()
				.Without<StunModifier>()
				.With<MoveInput>()
				.Push();
		}
		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref readonly var moveAmount = ref entity.Read<MoveInput>().Value;
			var direction = entity.Read<MoveInput>().Axis == MovementAxis.Vertical ? Vector3.right : Vector3.back;
			
			if (entity.GetRotation() != Quaternion.Euler(entity.Read<FaceDirection>().Value))
			{
				entity.SetRotation(Quaternion.RotateTowards(entity.GetRotation(), Quaternion.LookRotation(entity.Read<FaceDirection>().Value), 40f));
			}

			if (moveAmount != 0)
			{
				if (!entity.Has<LockTarget>())
				{
					entity.Get<FaceDirection>().Value = direction * moveAmount;
				}

				if ((entity.Read<PlayerMoveTarget>().Value - entity.GetPosition()).sqrMagnitude <= 0.025f)
				{
					entity.SetPosition(Vector3Int.CeilToInt(entity.Read<PlayerMoveTarget>().Value));
					
					var newTarget = entity.GetPosition() + direction * moveAmount;

					if (SceneUtils.IsWalkable(newTarget))
					{
						_scene.Move(entity.Read<PlayerMoveTarget>().Value, newTarget);
						entity.Get<PlayerMoveTarget>().Value = newTarget;
						
						if (entity.Has<TeleportPlayer>())
						{
							if (entity.Read<TeleportPlayer>().NeedDelete)
								entity.Remove<TeleportPlayer>();
							else
								entity.Get<TeleportPlayer>().NeedDelete = true;
						}                      
					}
				}
			}

			var speedBase = entity.Read<PlayerMovementSpeed>().Value;
			var speedMod = entity.Read<MoveSpeedModifier>().Value * speedBase;
			var currentSpeed = Mathf.Max(speedBase * 0.5f,speedBase + speedMod);
			
			var speed = entity.Has<LockTarget>() ? currentSpeed * 0.65f : currentSpeed;
			entity.SetPosition(Vector3.MoveTowards(entity.GetPosition(), entity.Read<PlayerMoveTarget>().Value, speed * deltaTime));     
		}
	}
}