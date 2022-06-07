using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.VFX.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
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

            amount += up ? 0.07 : -0.07;

            if (entity.Has<DestructibleTag>())
            {
                amount += up ? 0.1 : -0.05;
                
                if (amount >= 6.0)
                {
                    up = false;
                }
                else if (amount <= 0.5)
                {
                    up = true;
                }
            }
            else
            {
                amount += up ? 0.07 : -0.07;
                
                if (amount >= 3.0)
                {
                    up = false;
                }
                else if (amount <= 1.5)
                {
                    up = true;
                }
            }
        }
    }
}