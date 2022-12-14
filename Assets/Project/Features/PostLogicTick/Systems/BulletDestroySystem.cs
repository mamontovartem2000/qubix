using ME.ECS;
using Project.Common.Components;
using Project.Features.PreLogicTick;
using UnityEngine;

namespace Project.Features.PostLogicTick.Systems {

    #pragma warning disable
#pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class BulletDestroySystem : ISystemFilter {
        
        private PreLogicTickFeature feature;
        
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
            
            return Filter.Create("Filter-BulletDestroySystem")
                .With<ShouldDestroy>()
                .With<ProjectileActive>()
                .Without<Linear>()
                .Without<ShengbiaoProjectile>()
                .Without<SpikesProjectileTag>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            // entity.Destroy();
            entity.Get<LifeTimeLeft>().Value = 0.2f;
            // entity.SetPosition(new Vector3(entity.GetPosition().x, -0.8f, entity.GetPosition().z));
            entity.DestroyAllViews();
            entity.Remove<ProjectileSpeed>();
            entity.Remove<ProjectileActive>();
        }
    }
}