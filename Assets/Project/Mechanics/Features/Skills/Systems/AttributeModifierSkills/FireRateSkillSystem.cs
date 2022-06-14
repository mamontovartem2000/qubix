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
			return Filter.Create("Filter-FireRateSkillSystem")
				.With<FireRateAffect>()
				.With<ActivateSkill>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			if (!entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.IsAlive()) return;

			ref readonly var owner = ref entity.Read<Owner>().Value;
			ref var avatar = ref owner.Get<PlayerAvatar>().Value;
			ref readonly var rightWeapon = ref avatar.Read<WeaponEntities>().RightWeapon;

			var effect = new Entity("effect");
			effect.Get<Owner>().Value = owner;
			effect.Set(new EffectTag());

			rightWeapon.Get<FireRateModifier>().Value = 1;
			rightWeapon.Get<AmmoCapacityDefault>().Value = 2;
			rightWeapon.Get<AmmoCapacity>().Value = 2;
			
			SoundUtils.PlaySound(avatar, "event:/Skills/Buller/OffenciveBurst");
			
			world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity.Get<Owner>().Value);

			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			_vfx.SpawnVFX(VFXFeature.VFXType.SkillOffenciveBurst, avatar.GetPosition(), avatar);
			entity.Remove<ActivateSkill>();
		}
	}
}