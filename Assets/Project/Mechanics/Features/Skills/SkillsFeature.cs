using ME.ECS;
using Project.Common.Components;
using Project.Mechanics.Features.Skills.Systems.AttributeModifierSkills;
using Project.Mechanics.Features.Skills.Systems.ComponentBuffSkills;
using Project.Mechanics.Features.Skills.Systems.SelfTargetedSkills;
using Project.Mechanics.Features.Skills.Systems.TargetedSkills;

namespace Project.Mechanics.Features
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class SkillsFeature : Feature
    {
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void OnConstruct()
        {
            //attribute modifier systems
            AddSystem<MovementSpeedSkillsSystem>();
            AddSystem<AutomaticDamageSkillSystem>();
            AddSystem<FireRateSkillSystem>();
            AddSystem<FiringRangeSkillSystem>();
            
            //component buff systems
            AddSystem<ForceShieldSkillSystem>();
            AddSystem<LifeStealSkillSystem>();
            AddSystem<LinearPowerSkillSystem>();
            AddSystem<StunSkillSystem>();
            AddSystem<SilenceSkillSystem>();
            
            //self targeted systems
            AddSystem<CooldownResetSkillSystem>();
            AddSystem<DashSkillSystem>();
            AddSystem<InstantReloadSkillSystem>();
            AddSystem<LandMineSkillSystem>();
            AddSystem<PersonalTeleportSkillSystem>();
            AddSystem<SelfHealSystem>();
            AddSystem<SideStepSkillSystem>();
            
            //targeted systems
            AddSystem<GrenadeThrowSkillSystem>();
            AddSystem<MagneticStormSkillSystem>();
            AddSystem<WormholeHookSkillSystem>();
        }
        
        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder
                .With<SkillTag>()
                .Without<Cooldown>();
        }
        
        protected override void OnDeconstruct() {}
    }
}