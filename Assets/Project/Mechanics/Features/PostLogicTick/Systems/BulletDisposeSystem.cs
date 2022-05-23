using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Mechanics.Features.VFX;
using UnityEngine;

namespace Project.Mechanics.Features.PostLogicTick.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class BulletDisposeSystem : ISystemFilter
	{
		public World world { get; set; }
		
		private PostLogicTickFeature _feature;
		private VFXFeature _vfx;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			world.GetFeature(out _vfx);
		}
		void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;
		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }
		Filter ISystemFilter.CreateFilter()
		{
			return Filter.Create("Filter-BulletDisposeSystem")
				.With<ProjectileActive>()
				.With<Collided>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var applyTo = entity.Get<Collided>().ApplyTo;
			var applyFrom = entity.Get<Collided>().ApplyFrom;

			if (applyTo.Has<PlayerAvatar>())
			{
				applyTo.Get<DamagedBy>().Value = applyFrom;

				var collision = new Entity("collision");
				collision.Set(new ApplyDamage {ApplyTo = applyTo.Get<PlayerAvatar>().Value, Damage = 25f}, ComponentLifetime.NotifyAllSystems);
			}
			
			if (entity.Has<ProjectileActive>())
			{
				_vfx.SpawnVFX(VFXFeature.VFXType.BulletWall, applyTo.GetPosition());
				entity.Destroy();
			}
		}
	}
}