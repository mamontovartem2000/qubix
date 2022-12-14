using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.Skills.Systems.Buller
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class AmmoCapacityIncreaceSkillSystem : ISystemFilter
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
			var avatar = entity.Owner(out var owner).Avatar();
			if (avatar.IsAlive() == false) return;
			
			ref readonly var rightWeapon = ref avatar.Read<WeaponEntities>().RightWeapon;

			var effect = new Entity("effect");
			effect.Get<Owner>().Value = owner;
			effect.Set(new EffectTag());

			rightWeapon.Get<FireRateModifier>().Value = 1;
			rightWeapon.Get<AmmoCapacity>().Value = 0;
			rightWeapon.Get<AmmoCapacityDefault>().Value = GameConsts.Skills.FIRE_RATE_SKILL_AMMO_CAPACITY;
			rightWeapon.Get<ReloadTime>().Value = rightWeapon.Get<ReloadTimeDefault>().Value;
			
			SoundUtils.PlaySound(avatar, "event:/Skills/Buller/OffenciveBurst");
			
			world.GetFeature<EventsFeature>().RightWeaponDepleted.Execute(entity.Owner());
			world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity.Owner());

			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			_vfx.SpawnVFX(entity.Read<VFXConfig>().Value, avatar, rightWeapon.Get<ReloadTimeDefault>().Value);
		}
	}
}