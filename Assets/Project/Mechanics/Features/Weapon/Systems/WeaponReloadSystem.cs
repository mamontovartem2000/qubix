using ME.ECS;
using UnityEngine;

namespace Project.Mechanics.Features.Weapon.Systems
{
    #region usage
#pragma warning disable
    using Components;
    using Markers;
    using Modules;
    using Project.Components;
    using Project.Markers;
    using Project.Modules;
    using Project.Systems;
    using Common.Components;
    using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class WeaponReloadSystem : ISystemFilter
    {
        private WeaponFeature _feature;
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-WeaponReloadSystem")
                .With<WeaponReloadTime>()
                .Push();
        }
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Read<WeaponReloadTime>().Value - deltaTime > 0)
            {
                entity.Get<WeaponReloadTime>().Value -= deltaTime;
            }
            else
            {
                Debug.Log("ReloadEnded");
                entity.Remove<WeaponReloadTime>();
                entity.Get<WeaponAmmo>().Value = entity.Read<WeaponAmmoDefault>().Value;
            }
        }
    }
}