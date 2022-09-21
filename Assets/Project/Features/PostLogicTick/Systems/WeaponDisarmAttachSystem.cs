using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;

namespace Project.Features.PostLogicTick.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class WeaponDisarmAttachSystem : ISystemFilter {
        
        private PostLogicTickFeature _feature;
        
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
            return Filter.Create("Filter-WeaponDisarmAttachSystem")
                .With<ProjectileActive>()
                .With<Collided>()
                .With<DisarmModifier>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.TryReadCollided(out var from, out var owner) == false) return;

            if (!owner.Has<PlayerAvatar>()) return;
            
            if (from.Read<TeamTag>().Value == owner.Read<TeamTag>().Value) return;
            
            var avatar = owner.Avatar();
            avatar.Set(new DisarmModifier{LifeTime = entity.Read<DisarmModifier>().LifeTime});
        }
    }
}