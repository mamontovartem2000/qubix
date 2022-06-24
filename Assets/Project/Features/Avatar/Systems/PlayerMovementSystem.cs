// using Codice.CM.Common;
using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using UnityEngine;

namespace Project.Features.Avatar.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class PlayerMovementSystem : ISystemFilter
	{
		private AvatarFeature _feature;

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
				.With<PlayerTag>()
				.With<MoveInput>()
				.Push();
		}
		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref readonly var moveAmount = ref entity.Read<MoveInput>().Value;
			var direction = entity.Read<MoveInput>().Axis == MovementAxis.Vertical ? Vector3.right : Vector3.back;
			
			if (entity.GetRotation() != fpquaternion.Euler(entity.Read<FaceDirection>().Value))
			{
				entity.SetRotation(Quaternion.RotateTowards(entity.GetRotation(), Quaternion.LookRotation(entity.Read<FaceDirection>().Value), 30f));
			}

			if (moveAmount != 0)
			{
				if (!entity.Has<LockTarget>())
				{
					entity.Get<FaceDirection>().Value = direction * moveAmount;
				}

				if ((entity.Read<PlayerMoveTarget>().Value - entity.GetPosition()).sqrMagnitude <= Consts.Movement.MIN_DISTANCE)
				{
					entity.SetPosition((Vector3)Vector3Int.CeilToInt(entity.Read<PlayerMoveTarget>().Value));
					
					var newTarget = entity.GetPosition() + direction * moveAmount;

					if (SceneUtils.IsWalkable(newTarget))
					{
						SceneUtils.ModifyWalkable(entity.Read<PlayerMoveTarget>().Value, true);
						SceneUtils.ModifyWalkable(newTarget, false);
						
						entity.Get<PlayerMoveTarget>().Value = newTarget;
					}
				}
			}
			
			var currentSpeed = entity.Read<MoveSpeedModifier>().Value;
			var speed = entity.Has<LockTarget>() ? currentSpeed * Consts.Movement.LOCK_SPEED_RATIO : currentSpeed;

			var pos = entity.GetPosition();
			var target = entity.Read<PlayerMoveTarget>().Value;
			ref readonly var hover = ref entity.Read<Hover>().Amount;

			entity.SetPosition(Vector3.MoveTowards(new fp3(pos.x, hover, pos.z), new fp3(target.x, hover, target.z), speed * deltaTime));

		}
	}
}