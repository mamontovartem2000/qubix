using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.Skills.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SelfTriggerSystem : ISystemFilter
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
                .With<SelfTrigger>()
                .Without<Cooldown>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var effect = new Entity("effect");
            effect.Set(new EffectTag());
            effect.Set(new StatsBuff());

            var skillValue = entity.Read<SkillAmount>().Value;

            if (entity.Has<HealingAffect>())
                entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<PlayerHealth>().Value += skillValue;

            if (entity.Has<InstantReloadAffect>())
            {
                ref var enTT = ref entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<WeaponEntities>();
                enTT.LeftWeapon.Get<AmmoCapacity>().Value = enTT.LeftWeapon.Read<AmmoCapacityDefault>().Value;
                enTT.RightWeapon.Get<ReloadTime>().Value = 0;
            }

            if (entity.Has<SkillsResetAffect>())
            {
                ref var enTT = ref entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<SkillEntities>();
                enTT.FirstSkill.Get<Cooldown>().Value = 0.1f;
                enTT.SecondSkill.Get<Cooldown>().Value = 0.1f;
                enTT.ThirdSkill.Get<Cooldown>().Value = 0.1f;
                enTT.FourthSkill.Get<Cooldown>().Value = 0.1f;
            }

            //if(entity.Has<LandMineAffect>())
                //do smth
            
            //if(entity.Has<SideStepAffect>())
                //do smth
            
            //if(entity.Has<QuickStepAffect>())
                //do smth

            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
        }
    }
}