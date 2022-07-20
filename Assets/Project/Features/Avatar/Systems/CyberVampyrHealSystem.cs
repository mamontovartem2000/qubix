using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.Avatar.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class CyberVampyrHealSystem : ISystemFilter {
        
        private AvatarFeature feature;
        private VFXFeature _vfx;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
            world.GetFeature(out _vfx);

        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-CyberVampyrHealSystem")
                .With<ApplyDamage>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var apply = entity.Read<ApplyDamage>();
            var from = apply.ApplyFrom;
            var damage = apply.Damage;
            
            if (!from.Avatar().IsAlive() || !from.Avatar().Has<CyberVampyrModifier>()) return;

            from.Avatar().Get<ApplyHeal>().Value += damage * 0.4f;
            _vfx.SpawnVFX(from.Read<SkillEntities>().FourthSkill.Read<VFXConfig>().Value, from.Avatar(), 0.3f);
            
        }
    }
}