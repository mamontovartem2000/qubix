using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Mechanics.Features.VFX;
using UnityEngine;

namespace Project.Mechanics.Features.Skills.Systems.SelfTargetedSkills
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SelfHealSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private SkillsFeature _feature;
        private VFXFeature _vfx;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);            
            world.GetFeature(out _vfx);

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
                .With<HealingAffect>()
                .With<ActivateSkill>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner(out var owner).Avatar();
            if (avatar.IsAlive() == false) return;

            var collision = new Entity("collision");
			collision.Set(new ApplyDamage {ApplyTo = avatar, ApplyFrom = avatar, Damage = -entity.Read<SkillAmount>().Value}, ComponentLifetime.NotifyAllSystems);
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
            
            world.GetFeature<EventsFeature>().HealthChanged.Execute(owner);
            
            _vfx.SpawnVFX(VFXFeature.VFXType.SkillHeal, avatar.GetPosition(), avatar);
			entity.Remove<ActivateSkill>();
        }
    }
}