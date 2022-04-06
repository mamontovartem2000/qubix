using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
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
    public sealed class NewRefreshAmmoUISystem : ISystemFilter
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
            return Filter.Create("Filter-NewRefreshAmmoUISystem")
                .With<LinearWeapon>()
                .Without<LinearFull>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var ammo = entity.Read<AmmoCapacity>().Value;
            var max = entity.Read<AmmoCapacityDefault>().Value;

            if (ammo != max)
            {
                world.GetFeature<EventsFeature>().leftWeaponFired.Execute(entity);
            }
            else
            {
                entity.Set(new LinearFull());
                world.GetFeature<EventsFeature>().leftWeaponFired.Execute(entity);
            }
        }
    }
}