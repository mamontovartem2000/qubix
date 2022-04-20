using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.Skills.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class BuffTriggerSystem : ISystemFilter
	{
		private SkillsFeature _feature;

		public World world { get; set; }

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
			return Filter.Create("Filter-SelfTargetedSkillsSystem")
				.With<BuffTrigger>()
				.Without<Cooldown>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var effect = new Entity("effect");
			effect.Set(new EffectTag());

			var skillValue = entity.Read<SkillAmount>().Value / 100f;

			if (entity.Has<StatsBuff>())
			{
				effect.Set(new StatsBuff());
				effect.Get<SkillAmount>().Value = skillValue;

				if (entity.Has<MoveSpeedAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<MoveSpeedModifier>().Value += skillValue;

				if (entity.Has<FireRateAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<FireRateModifier>().Value += skillValue;
			
				if(entity.Has<WeaponDamageAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<WeaponDamageModifier>().Value += skillValue;
			
				if(entity.Has<FiringRangeAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<FiringRangeModifier>().Value += skillValue;
			}
			else
			{
				effect.Set(new ComponentBuff());
				
				if (entity.Has<StunAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Set(new StunAffect());

				if (entity.Has<SkillSilenceAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Set(new SkillSilenceAffect());
			
				if(entity.Has<LifeStealAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Set(new LifeStealAffect());
				
				if(entity.Has<ForceShieldAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Set(new ForceShieldAffect());
				
				if(entity.Has<LinearPowerAffect>())
					entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Set(new LinearPowerAffect());
			}

			effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;
			effect.Get<Owner>().Value = entity.Read<Owner>().Value;
			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
		}
	}
}