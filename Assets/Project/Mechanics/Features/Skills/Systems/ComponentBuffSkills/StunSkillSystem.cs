using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Mechanics.Features.VFX;
using UnityEngine;


namespace Project.Mechanics.Features.Skills.Systems.ComponentBuffSkills
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class StunSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-StunSkillSystem")
				.With<StunAffect>()
				.With<ActivateSkill>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			if (!entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.IsAlive()) return;

			ref readonly var owner = ref entity.Read<Owner>().Value;
			ref var avatar = ref owner.Get<PlayerAvatar>().Value;
			ref readonly var rightWeapon = ref avatar.Read<WeaponEntities>().RightWeapon;

			rightWeapon.Set(new StunModifier{Value = 20});

			rightWeapon.Get<AmmoCapacityDefault>().Value = 5;

			rightWeapon.Get<ReloadTime>().Value = 
				rightWeapon.Read<ReloadTimeDefault>().Value;

            world.GetFeature<EventsFeature>().RightWeaponDepleted.Execute(owner);
            
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
            _vfx.SpawnVFX(VFXFeature.VFXType.StatusStunEffect, avatar.GetPosition(), avatar);

			entity.Remove<ActivateSkill>();
		}
	}
}