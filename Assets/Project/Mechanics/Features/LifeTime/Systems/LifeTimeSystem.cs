using ME.ECS;

namespace Project.Mechanics.Features.Lifetime.Systems
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
    using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class LifeTimeSystem : ISystemFilter {
        
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
            
            return Filter.Create("Filter-LifeTimeSystem")
                .With<LifeTimeTag>()
                .Push();
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            if (entity.Read<LifeTime>().Value - deltaTime > 0)
            {
                entity.Get<LifeTime>().Value -= deltaTime;
            }
            else
            {
                entity.Remove<LifeTimeTag>();
                entity.Destroy();
            }
        }   
    }   
}