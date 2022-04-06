﻿using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.Weapon.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class WeaponReloadSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private WeaponFeature _feature;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
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
                .With<ReloadTime>()
                .Push();
        }
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Read<ReloadTime>().Value - deltaTime > 0)
            {
                entity.Get<ReloadTime>().Value -= deltaTime;
            }
            else
            {
                entity.Remove<ReloadTime>();
                if (!entity.Has<LinearWeapon>())
                {
                    entity.Get<AmmoCapacity>().Value = entity.Read<AmmoCapacityDefault>().Value;
                }
            }
        }
    }
}