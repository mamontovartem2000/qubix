using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.Projectile;
using UnityEngine;

namespace Project.Features.Weapon.Systems
{
	#region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	#endregion

	public sealed class MeleeFiringSystem : ISystemFilter
	{
		public World world { get; set; }
		private WeaponFeature _feature;
		private ProjectileFeature _projectile;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			world.GetFeature(out _projectile);
		}
		void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;
		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }

		Filter ISystemFilter.CreateFilter()
		{
			return Filter.Create("Filter-MeleeFiringSystem")
				.With<MeleeWeapon>()
				.With<MeleeActive>()
				.Without<ReloadTime>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref var delay = ref entity.Get<MeleeDelay>().Value;
			ref var aim = ref entity.Get<MeleeDamageSpot>().Value;
			
			delay += deltaTime;
			
			if (delay < entity.Read<MeleeDelayDefault>().Value) return;

			Worlds.current.GetFeature<EventsFeature>().PlaySound.Execute(entity);

			entity.Get<MeleeDelay>().Value = 0;
			entity.Get<ReloadTime>().Value = entity.Read<ReloadTimeDefault>().Value;
				
			_projectile.SpawnMelee(aim, entity);
		}
	}
}