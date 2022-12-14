using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using UnityEngine;

namespace Project.Features.Weapon.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class ShengbiaoReloadSystem : ISystemFilter {
        
        private WeaponFeature feature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
            
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-ShengbiaoReloadSystem")
                .With<ReloadTime>()
                .With<ShengbiaoWeapon>()
                .Push();
            
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            ref var reload = ref entity.Get<ReloadTime>().Value;
            reload -= deltaTime;
            
            if (reload > 0) return;
            
            entity.Remove<ReloadTime>();
            entity.Read<ShengbiaoDamageSpot>().Value.SetLocalPosition(Vector3.zero);
            entity.Read<ShengbiaoDamageSpot>().Value.Get<ProjectileParent>().Speed = 0;
            entity.Get<ShengbiaoWeapon>().MoveRatio = 0;
            entity.Get<AmmoCapacity>().Value = entity.Read<AmmoCapacityDefault>().Value;
            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity.Owner());
        }
    }
}