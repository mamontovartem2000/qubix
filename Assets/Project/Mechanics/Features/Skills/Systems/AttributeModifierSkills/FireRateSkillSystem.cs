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
	public sealed class FireRateSkillSystem : ISystemFilter
	{
		public World world { get; set; }
		
		private SkillsFeature _feature;
		private VFXFeature _vfx;

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
			return Filter.Create("Filter-FireRateSkillSystem")
				.With<FireRateAffect>()
				.With<ActivateSkill>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var effect = new Entity("effect");
			effect.Get<Owner>().Value = entity.Get<Owner>().Value;
			effect.Set(new EffectTag());

			// var amount = entity.Read<SkillAmount>().Value / 100f;

			// entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<FireRateModifier>().Value += amount;
			entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<WeaponEntities>().RightWeapon.Get<AmmoCapacityDefault>().Value = 2;
			entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<WeaponEntities>().RightWeapon.Get<AmmoCapacity>().Value = 2;
			// effect.Get<SkillAmount>().Value = amount;
			effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;
			effect.Set(new FireRateModifier());
			
			world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity.Get<Owner>().Value);

			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			_vfx.SpawnVFX(VFXFeature.VFXType.SkillOffenciveBurst, entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.GetPosition());
			entity.Remove<ActivateSkill>();
			Debug.Log("fire rate increased");
		}
	}
}