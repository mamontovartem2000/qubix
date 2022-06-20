using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Mechanics.Features.Weapon.Systems
{
	#region usage
#pragma warning disable
	using Project.Components;
	using Project.Modules;
	using Project.Systems;
	using Project.Markers;
	using Components;
	using Modules;
	using Systems;
	using Markers;

#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	#endregion
	public sealed class LinearReloadSystem : ISystemFilter
	{
		public World world { get; set; }
		private WeaponFeature _feature;

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
			return Filter.Create("Filter-NewLinearReloadSystem")
				.With<LinearWeapon>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var ammoCap = entity.Read<AmmoCapacityDefault>().Value;
			ref var ammo = ref entity.Get<AmmoCapacity>().Value;
			
			var mod = entity.Read<LinearPowerModifier>().Damage > 1.3f ? 1 : 0;
			var deplete = 1 + mod;

			if (entity.Has<LinearActive>())
			{
				if (ammo - deplete > 0)
				{
					ammo -= deplete;
				}
				else
				{
					entity.Remove<LinearActive>();
					entity.Remove<LeftWeaponShot>();
				}
			}
			else
			{
				if (ammo < ammoCap)
				{
					ammo++;
				}
			}
		}
	}
}