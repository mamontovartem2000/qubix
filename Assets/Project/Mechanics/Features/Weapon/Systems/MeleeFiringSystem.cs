using ME.ECS;
using Project.Common.Components;
using Project.Mechanics.Features.Projectile;
using UnityEngine;

namespace Project.Mechanics.Features.Weapon.Systems
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
			if(entity.GetParent().Has<StunModifier>()) return;

			ref var delay = ref entity.Get<MeleeDelay>().Value;
			var gunDir = entity.Read<WeaponAim>().Value.GetPosition();
			
			delay -= deltaTime;

			if (delay <= 0)
			{
				if(!entity.Has<LeftWeaponShot>())
					entity.Remove<MeleeActive>();
				
				entity.Get<MeleeDelay>().Value = entity.Read<MeleeDelayDefault>().Value;
				entity.Get<ReloadTime>().Value = entity.Read<ReloadTimeDefault>().Value;
				
				_projectile.SpawnMelee(entity, gunDir);
			}
		}
	}
}