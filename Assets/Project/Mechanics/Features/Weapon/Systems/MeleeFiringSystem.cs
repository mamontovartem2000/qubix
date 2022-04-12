﻿using ME.ECS;
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

		void ISystemBase.OnDeconstruct()
		{
		}
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
				.Without<Cooldown>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref var delay = ref entity.Get<MeleeDelay>().Value;
			var dir = entity.Read<WeaponAim>().Aim.GetPosition() - entity.GetPosition();

			if (delay - deltaTime > 0)
			{
				delay -= deltaTime;
			}
			else
			{
				_projectile.SpawnMelee(entity, 3, dir);
			}
		}
	}
}