using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
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
			return Filter.Create("Filter-StunSkillSystem")
				.With<StunAffect>()
				.With<ActivateSkill>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Set(new StunModifier());

			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Read<WeaponEntities>().RightWeapon.Get<AmmoCapacityDefault>().Value = 5;

			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Read<WeaponEntities>().RightWeapon.Get<ReloadTime>().Value = 
			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Read<WeaponEntities>().RightWeapon.Read<ReloadTimeDefault>().Value;

            world.GetFeature<EventsFeature>().RightWeaponDepleted.Execute(entity.Get<Owner>().Value);
						
			entity.Remove<ActivateSkill>();
			Debug.Log("Character is stunned");
		}
	}
}