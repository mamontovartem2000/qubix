using FlatBuffers;
using ME.ECS;
using Project.Common.Components;
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
    public sealed class ShengbiaoSpeedControlSystem : ISystemFilter {
        
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
            
            return Filter.Create("Filter-ShengbiaoSpeedControlSystem")
                .With<ShengbiaoWeapon>()
                .With<ShengbiaoShot>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Read<ReloadTimeDefault>().Value - entity.Read<ReloadTime>().Value < Consts.Weapons.SHENGBIAO_ATTACK_SECONDS) return;

            var backwardsRatio =  (Consts.Weapons.SHENGBIAO_MIN_LENGHT - entity.Read<ShengbiaoWeapon>().Offset) / (entity.Read<ReloadTime>().Value);
            entity.Get<ShengbiaoWeapon>().MoveRatio = -backwardsRatio;
            entity.Read<ShengbiaoDamageSpot>().Value.Get<ProjectileParent>().Speed = -10f;
            entity.Remove<ShengbiaoShot>();
        }
    }
}