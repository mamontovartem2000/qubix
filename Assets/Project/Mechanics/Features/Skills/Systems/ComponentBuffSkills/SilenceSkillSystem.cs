using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Mechanics.Features.Skills.Systems.ComponentBuffSkills
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class SilenceSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-SilenceSkillSystem")
				.With<SkillSilenceAffect>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var effect = new Entity("effect");
			effect.Set(new EffectTag());

			entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Set(new SkillSilenceModifier());
				
			effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;
			effect.Get<Owner>().Value = entity.Read<Owner>().Value;
			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			
			entity.Remove<ActivateSkill>();
			Debug.Log("Character is silenced");
		}
	}
}