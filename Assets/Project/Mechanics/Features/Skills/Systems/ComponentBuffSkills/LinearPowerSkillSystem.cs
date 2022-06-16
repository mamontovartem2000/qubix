using ME.ECS;
using Project.Common.Components;
using Project.Core;
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

		// ReSharper disable Unity.PerformanceAnalysis
		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var avatar = entity.Owner(out var owner).Avatar();
			if (avatar.IsAlive() == false) return;
			
			ref readonly var leftWeapon = ref avatar.Read<WeaponEntities>().LeftWeapon;
			
			var effect = new Entity("effect");
			
			effect.Set(new EffectTag());
			
			avatar.Set(new LinearPowerModifier{ Damage = 0.5f });
			leftWeapon.Remove<LinearActive>();
			leftWeapon.Remove<LeftWeaponShot>();
			leftWeapon.Set(new AmmoCapacity { Value = 100 });
			effect.Set(new LinearPowerModifier());

			effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;
			effect.Get<Owner>().Value = owner;
			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;

			SoundUtils.PlaySound(avatar, "event:/Skills/Powerf/LinearPower");

			_vfx.SpawnVFX(VFXFeature.VFXType.SkillLinearPower, avatar.GetPosition(), avatar);

			entity.Remove<ActivateSkill>();
		}
	}
}