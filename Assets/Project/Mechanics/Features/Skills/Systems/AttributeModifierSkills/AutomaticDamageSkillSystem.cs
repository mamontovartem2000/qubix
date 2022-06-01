using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Mechanics.Features.VFX;
using UnityEngine;

namespace Project.Mechanics.Features.Skills.Systems.AttributeModifierSkills
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class AutomaticDamageSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-WeaponDamageSkillSystem")
				.With<AutomaticDamageAffect>()
				.With<ActivateSkill>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var effect = new Entity("effect");
			effect.Get<Owner>().Value = entity.Read<Owner>().Value;
			effect.Set(new EffectTag());

			var amount = entity.Read<SkillAmount>().Value / 100f;


			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			
			entity.Remove<ActivateSkill>();
		}
	}
}