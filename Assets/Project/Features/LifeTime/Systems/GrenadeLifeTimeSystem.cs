using ME.ECS;
using Project.Common.Components;

namespace Project.Features.LifeTime.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class GrenadeLifeTimeSystem : ISystemFilter {
        
        private LifeTimeFeature feature;
        
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
            
            return Filter.Create("Filter-GrenadeLifeTimeSystem")
                .With<Grenade>()
                .With<LifeTimeLeft>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var lifeTime = ref entity.Get<LifeTimeLeft>().Value;
            lifeTime -= deltaTime;

            if (lifeTime > 0) return;

            entity.Set(new GrenadeExplode());
        }
    }
}