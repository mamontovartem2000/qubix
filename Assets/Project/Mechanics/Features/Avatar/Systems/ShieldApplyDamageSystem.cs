using ME.ECS;
using Project.Common.Components;

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
    public sealed class ShieldApplyDamageSystem : ISystemFilter {
        
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
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-ShieldApplyDamageSystem")
                .With<ApplyDamage>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var apply = ref entity.Get<ApplyDamage>();
            var from = apply.ApplyFrom;
            var to = apply.ApplyTo;
            if (!to.IsAlive()) return;
            
            var damage = apply.Damage;
            ref readonly var shield = ref to.Read<ForceShieldModifier>().Value;
            
            if (!to.Has<ForceShieldModifier>()) return;
            
            // if (shield - damage >= 0)
            // {
                to.Get<ForceShieldModifier>().Value -= damage;
            // }
            // else if (shield - damage < 0)
            // {
            //     to.Get<ForceShieldModifier>().Value -= damage;
            //     to.Remove<ForceShieldModifier>();
            // }
        }
    
    }
    
}