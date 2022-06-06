using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Mechanics.Features.Skills.Systems.AttributeModifierSkills
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class LinearDamageSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-LinearDamageSkillSystem")
				.With<LinearDamageAffect>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			if (!entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.IsAlive()) return;

			var effect = new Entity("effect");
			effect.Set(new EffectTag());

			effect.Get<SkillAmount>().Value = entity.Read<SkillAmount>().Value / 100f;
			effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;

			effect.Get<Owner>().Value = entity.Read<Owner>().Value;

			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
		}
	}
}