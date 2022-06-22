using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.PostLogicTick.Systems {

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
    public sealed class StunBulletDisposeSystem : ISystemFilter {
        
        private PostLogicTickFeature _feature;
        private VFXFeature _vfx;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this._feature);
            
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-StunBUlletDisposeSystem")
                .With<ProjectileActive>()
                .With<Collided>()
                .With<StunModifier>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.TryReadCollided(out var from, out var owner) == false) return;
            var pos = entity.GetPosition();
            
            if (owner.Has<PlayerAvatar>())
            {
                var player = owner.Avatar();
                _vfx.SpawnVFX(VFXFeature.VFXType.SkillStun, pos, player, entity.Read<StunModifier>().Value);
                player.Set(new Stun { Value = entity.Read<StunModifier>().Value });
            }
            entity.Destroy();
        }
    
    }
    
}