using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;

namespace Project.Features.Skills.Systems.Bloodlov {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SoftShieldSkillSystem : ISystemFilter {
        
        private SkillsFeature feature;
        
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
            
            return Filter.Create("Filter-SoftShieldSkillSystem")
                .With<SoftShieldAffect>()
                .With<ActivateSkill>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner(out var owner).Avatar();
            if (avatar.IsAlive() == false) return;

            // var effect = new Entity("effect");
            // effect.Set(new EffectTag());
            // effect.Get<Owner>().Value = owner;
            // effect.Set(new ForceShieldModifier());
            // effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;
            // effect.SetParent(avatar);
            
            avatar.Set(new SoftShieldModifier{LifeTime = 5});
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
        }
    }
}