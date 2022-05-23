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
	public sealed class DashSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-DashSkillSystem")
				.With<DashAffect>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var nextPos = entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Get<FaceDirection>().Value * 4 + entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.GetPosition();

			if(SceneUtils.IsFree(nextPos))
			{
				SceneUtils.Move(entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.GetPosition(), nextPos);
				entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.SetPosition(nextPos);
				entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Get<PlayerMoveTarget>().Value = nextPos;
			}

			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			
			entity.Remove<ActivateSkill>();
			Debug.Log("dash activated");
		}
	}
}