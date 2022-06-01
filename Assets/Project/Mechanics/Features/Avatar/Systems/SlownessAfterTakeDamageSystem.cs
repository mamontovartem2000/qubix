using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Mechanics.Features.Avatar.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SlownessAfterTakeDamageSystem : ISystemFilter {

        private AvatarFeature _feature;

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
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-SlownessAfterTakeDamageSystem")
                .With<ApplyDamage>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var apply = entity.Read<ApplyDamage>();
            var to = apply.ApplyTo;
            var damage = apply.Damage;
            
            if (!to.IsAlive()) return;
            
            if (to.Has<Slowness>())
            {
                to.Get<Slowness>().LifeTime = 1;
            }
            else
            {
                to.Get<Slowness>().Value = Mathf.Min(damage / 100, 0.8f);
                to.Get<Slowness>().LifeTime = 1;
            }
        }
    
    }
    
}