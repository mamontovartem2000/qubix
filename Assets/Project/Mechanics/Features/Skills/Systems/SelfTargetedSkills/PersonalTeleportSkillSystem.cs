using ME.ECS;
using Project.Common.Components;
using Project.Core;
using UnityEngine;

namespace Project.Mechanics.Features.Skills.Systems.SelfTargetedSkills
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class PersonalTeleportSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-PersonalTeleportSkillSystem")
				.With<PersonalTeleportAffect>()
				.With<ActivateSkill>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
 			var rndX = world.GetRandomRange(3, 7);
			var rndZ = world.GetRandomRange(3, 7);
			switch(world.GetRandomRange(0, 3))
			{
				case 0:
				{
					rndX *= -1;
					break;
				}

				case 1:
				{
					rndZ *= -1;
					break;
				}

				case 2:
				{
					rndX *= -1;
					rndZ *= -1;
					break;
				}

				default:
				{
					break;
				}
			}
			var randomPlayerPos = new Vector3(entity.Read<Owner>().Value.Get<PlayerAvatar>().Value.GetPosition().x + rndX, entity.Read<Owner>().Value.Get<PlayerAvatar>().Value.GetPosition().y, entity.Read<Owner>().Value.Get<PlayerAvatar>().Value.GetPosition().z + rndZ);
			
			if (!SceneUtils.IsFree(randomPlayerPos)) return;

			SceneUtils.Move(entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.GetPosition(), randomPlayerPos);
			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.SetPosition(randomPlayerPos);
			entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Get<PlayerMoveTarget>().Value = randomPlayerPos;

			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
						
			entity.Remove<ActivateSkill>();
			Debug.Log("WHZOOKH!");

		}
	}
}