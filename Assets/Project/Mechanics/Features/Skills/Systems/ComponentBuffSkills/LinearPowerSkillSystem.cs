using ME.ECS;
using Project.Common.Components;
using Project.Mechanics.Features.VFX;
using UnityEngine;

namespace Project.Mechanics.Features.Skills.Systems.ComponentBuffSkills
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class LinearPowerSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-LinearPowerSkillSystem")
				.With<LinearPowerAffect>()
                .With<ActivateSkill>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var effect = new Entity("effect");
			effect.Set(new EffectTag());
			
			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Set(new LinearPowerModifier{Damage = 0.5f});
			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Read<WeaponEntities>().LeftWeapon.Remove<LinearActive>();
			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Read<WeaponEntities>().LeftWeapon.Remove<LeftWeaponShot>();
			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Read<WeaponEntities>().LeftWeapon.Set(new AmmoCapacity { Value = 100 });
			effect.Set(new LinearPowerModifier());

			effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;
			effect.Get<Owner>().Value = entity.Read<Owner>().Value;
			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			
			_vfx.SpawnVFX(VFXFeature.VFXType.SkillLinearPower, entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.GetPosition(), entity.Get<Owner>().Value.Get<PlayerAvatar>().Value);

			entity.Remove<ActivateSkill>();
			Debug.Log("Linear Power activated");
		}
	}
}