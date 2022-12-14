using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.Skills.Systems.GoldHunter
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
			var avatar = entity.Owner(out var owner).Avatar();
			if (avatar.IsAlive() == false) return;
			
			ref readonly var rightWeapon = ref avatar.Read<WeaponEntities>().RightWeapon;
			
			SoundUtils.PlaySound(avatar, "event:/Skills/GoldHunter/StunShot");
			
			rightWeapon.Get<ModifierConfig>().Value = entity.Read<ProjectileConfig>().Value;

			rightWeapon.Set( new StunModifier { AmmoCapacityDefault = rightWeapon.Read<AmmoCapacityDefault>().Value } );

			rightWeapon.Get<AmmoCapacityDefault>().Value = 5;
			rightWeapon.Get<AmmoCapacity>().Value = 0;

			rightWeapon.Get<ReloadTime>().Value = rightWeapon.Read<ReloadTimeDefault>().Value;
			
			world.GetFeature<EventsFeature>().rightWeaponFired.Execute(owner);
			world.GetFeature<EventsFeature>().RightWeaponDepleted.Execute(owner);
            
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
            
            _vfx.SpawnVFX(entity.Read<VFXConfig>().Value, avatar, GameConsts.Main.DEFAULT_LIFETIME);
		}
	}
}