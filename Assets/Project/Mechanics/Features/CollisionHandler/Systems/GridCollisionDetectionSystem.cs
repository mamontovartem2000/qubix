using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Mechanics.Features.CollisionHandler.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class GridCollisionDetectionSystem : ISystemFilter
	{
		public World world { get; set; }
		
		private CollisionHandlerFeature _feature;
		private Filter _damageFilter;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			Filter.Create("Filter-PlayerFilter")
				.With<CollisionDynamic>()
				.Push(ref _damageFilter);
		}
		void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;
		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }
		Filter ISystemFilter.CreateFilter()
		{
			return Filter.Create("Filter-GridCollisionDetectionSystem")
				.With<CollisionStatic>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			foreach (var source in _damageFilter)
			{
				var coll = entity.GetPosition();
				var collIndex = SceneUtils.PositionToIndex(coll);

				var playerPos = source.GetPosition();
				var sourceIndex = SceneUtils.PositionToIndex(playerPos);

				if (entity == source) continue;
				if (collIndex == sourceIndex)
				{
					Debug.Log("hehe");
					if(entity.Read<Owner>().Value == source.Read<Owner>().Value) continue;
					source.Get<Collided>().Value = entity;
				}
			}
		}
	}
}