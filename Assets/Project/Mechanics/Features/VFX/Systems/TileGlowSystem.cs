using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.VFX.Systems {

    #pragma warning disable
    using Project.Modules;
    using Project.Markers;
    using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class TileGlowSystem : ISystemFilter {
        
        private VFXFeature feature;
        
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
            
            return Filter.Create("Filter-TileGlowSystem")
                .With<GlowTile>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var up = ref entity.Get<GlowTile>().Direction;
            ref var amount = ref entity.Get<GlowTile>().Amount;

            if (entity.Has<DestructibleTag>())
            {
                amount += up ? 0.04f : -0.05f;
                
                if (amount >= 4.5)
                {
                    up = false;
                }
                else if (amount <= 1.5)
                {
                    up = true;
                }
            }
            else
            {
                amount += up ? 0.04f : -0.04f;
                
                if (amount >= 2.9)
                {
                    up = false;
                }
                else if (amount <= 1.0)
                {
                    up = true;
                }
            }
        }
    }
}