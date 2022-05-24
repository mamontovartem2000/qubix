using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.Player;
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

		public World world { get; set; }

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
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
				.Without<Stun>()
				.With<MoveInput>()
				.Push();
		}
		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref readonly var moveAmount = ref entity.Read<MoveInput>().Value;
			var direction = entity.Read<MoveInput>().Axis == MovementAxis.Vertical ? Vector3.right : Vector3.back;
			
			if (entity.GetRotation() != fpquaternion.Euler(entity.Read<FaceDirection>().Value))
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
					entity.SetPosition((Vector3)Vector3Int.CeilToInt(entity.Read<PlayerMoveTarget>().Value));
					
					var newTarget = entity.GetPosition() + direction * moveAmount;

					if (SceneUtils.IsWalkable(newTarget))
					{
						SceneUtils.Move(entity.Read<PlayerMoveTarget>().Value, newTarget);
						entity.Get<PlayerMoveTarget>().Value = newTarget;
						
						// if (entity.Has<TeleportPlayer>())
						// {
						// 	if (entity.Read<TeleportPlayer>().NeedDelete)
						// 		entity.Remove<TeleportPlayer>();
						// 	else
						// 		entity.Get<TeleportPlayer>().NeedDelete = true;
						// }                      
					}
				}
			}

			if (entity.Has<AvoidTeleport>())
			{
				if (entity.Get<AvoidTeleport>().Value - deltaTime > 0f)
				{
					entity.Get<AvoidTeleport>().Value -= deltaTime;
				}
				else
				{
					entity.Remove<AvoidTeleport>();
				}
			}
			
			var speedBase = entity.Read<PlayerMovementSpeed>().Value;
			var speedMod = entity.Read<MoveSpeedModifier>().Value * speedBase * entity.Read<Slowness>().Value;
			var currentSpeed = speedMod;
			var speed = entity.Has<LockTarget>() ? currentSpeed * 0.65f : currentSpeed;
			entity.SetPosition(Vector3.MoveTowards(entity.GetPosition(), entity.Read<PlayerMoveTarget>().Value, speed * deltaTime));     
		}
	}
}