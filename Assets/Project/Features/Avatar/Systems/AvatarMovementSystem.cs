using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Features.Avatar.Systems
{
	using static SceneUtils;
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class AvatarMovementSystem : ISystemFilter
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
				.With<AvatarTag>()
				.Without<Stun>()
				.Without<DashModifier>()
				.Push();
		}
		
		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref readonly var input = ref entity.Owner().Read<MoveInput>();
			var direction = input.Axis == MovementAxis.Vertical ? new float3(1, 0, 0) : new float3(0, 0, -1);
			
			if (input.Amount != 0)
			{				
				if ((entity.Read<PlayerMoveTarget>().Value - entity.GetPosition()).sqrMagnitude <= Consts.Movement.MIN_DISTANCE)
				{
					entity.SetPosition((Vector3)Vector3Int.CeilToInt(entity.Read<PlayerMoveTarget>().Value));
					var newTarget = entity.GetPosition() + direction * input.Amount;

					if (IsWalkable(newTarget))
					{
						ModifyWalkable(entity.Read<PlayerMoveTarget>().Value, true);
						ModifyWalkable(newTarget, false);
						
						entity.Get<PlayerMoveTarget>().Value = newTarget;
					}
				}
			}
			
			var currentSpeed = entity.Read<MoveSpeedModifier>().Value;

			if (entity.Owner().Has<LockTarget>())
			{
				currentSpeed *= Consts.Movement.LOCK_SPEED_RATIO;
			}

			var pos = entity.GetPosition();
			var target = entity.Read<PlayerMoveTarget>().Value;
			ref readonly var hover = ref entity.Read<Hover>().Amount;

			var posDelta = Vector3.MoveTowards(new Vector3((float)pos.x, (float)hover, (float)pos.z),
				new Vector3((float)target.x, (float)hover, (float)target.z), (float)(currentSpeed * deltaTime));
			
			entity.SetPosition(posDelta);
		}
	}
}