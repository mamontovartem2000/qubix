using System;
using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.Skills.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SkillActivationSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private SkillsFeature _feature;
        
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
        }

        void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-SkillActivationSystem")
                .With<SkillTag>()
                .With<ActivateSkill>()
                .Without<PassiveSkill>()
                .Without<Cooldown>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var type = ref entity.Get<SkillEffect>().Value;

            var effect = new Entity("effect");

            var skillValue = entity.Read<SkillAmount>().Value / 100f;
            
            switch (type)
            {
                case SkillAttribute.MoveSpeed:
                {
                    entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<MoveSpeedModifier>().Value += skillValue;
                    break;
                }
                case SkillAttribute.AttackSpeed:
                {
                    entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<FiringCooldownModifier>().Value += skillValue;
                    break;
                }
            }

            effect.Get<SkillAmount>().Value = skillValue;
            effect.Get<LifeTimeLeft>().Value = entity.Read<LifeTimeDefault>().Value;
            effect.Get<Owner>().Value = entity.Read<Owner>().Value;
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
        }
    }
}