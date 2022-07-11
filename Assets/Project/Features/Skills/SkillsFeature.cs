using ME.ECS;
using Project.Common.Components;
using Project.Features.Skills.Systems.Buller;
using Project.Features.Skills.Systems.GoldHunter;
using Project.Features.Skills.Systems.Powerf;
using Project.Features.Skills.Systems.Silen;
using Project.Features.Skills.Systems.Solaray;
using Project.Features.Skills.Systems.Universal;

namespace Project.Features.Skills
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
        protected override void OnConstruct()
        {
            //Buller
            AddSystem<AmmoCapacityIncreaceSkillSystem>();
            AddSystem<PersonalTeleportSkillSystem>();
            AddSystem<InstantReloadSkillSystem>();
            
            //Goldhunter
            AddSystem<StunSkillSystem>();
            AddSystem<CooldownResetSkillSystem>();
            AddSystem<BlinkSkillSystem>();
            
            //Powerf
            AddSystem<MovementSpeedSkillsSystem>();
            AddSystem<LinearPowerSkillSystem>();
            AddSystem<ForceShieldSkillSystem>();
            AddSystem<SelfHealSystem>();
            
            //Silen
            AddSystem<EMPSkillSystem>();
            AddSystem<DashSystem>();
            
            //Solaray
            AddSystem<SecondLifeSkillSystem>();
            AddSystem<CriticalHitSkillSystem>();
            AddSystem<FreezeStormSkillSystem>();
            
            //Universal
            AddSystem<GrenadeThrowSkillSystem>();
            AddSystem<SphereAttackSkill>();
            
        }
        
        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder
                .With<SkillTag>()
                .Without<Cooldown>()
                .Without<EMP>();
        }
        
        protected override void OnDeconstruct() {}
    }
}