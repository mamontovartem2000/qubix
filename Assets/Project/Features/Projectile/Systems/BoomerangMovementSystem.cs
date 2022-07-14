using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Unity.Mathematics;

namespace Project.Features.Projectile.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class BoomerangMovementSystem : ISystemFilter {
        
        private ProjectileFeature feature;
        
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
            
            return Filter.Create("Filter-BoomerangMovementSystem")
                .With<BoomerangEffect>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var speed = ref entity.Get<ProjectileSpeed>().Value;
            
            if (speed > 0)
            {
                speed -= deltaTime * entity.Read<BoomerangEffect>().Value;
            }
            else
            {
                speed -= deltaTime * entity.Read<BoomerangEffect>().Value / 8f;
                
                if (fpmath.distancesq(entity.GetPosition(), entity.Owner().Avatar().GetPosition()) > 1f) return;
            
                entity.Destroy();
            }
        }
    }
}