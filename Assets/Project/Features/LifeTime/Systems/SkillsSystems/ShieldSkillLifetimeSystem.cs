using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Features.LifeTime.Systems.SkillsSystems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class ShieldSkillLifetimeSystem : ISystemFilter
	{
		public World world { get; set; }
		
		private LifeTimeFeature _feature;
		
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
			return Filter.Create("Filter-ShieldSkillLifetimeSystem")
				.With<LifeTimeLeft>()
				.With<EffectTag>()
				.With<ForceShieldModifier>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			if(!entity.Get<Owner>().Value.Read<PlayerAvatar>().Value.IsAlive()) return;
			
			ref var lifeTime = ref entity.Get<LifeTimeLeft>().Value;
			lifeTime -= deltaTime;
			
			var avatar = entity.Owner().Avatar();
			if (avatar.IsAlive() == false) return;

			if (lifeTime < 0f || avatar.Get<ForceShieldModifier>().Value <= 0)
			{
				avatar.Get<ForceShieldModifier>().Value = 0;
				avatar.Remove<ForceShieldModifier>();
				Debug.Log("shield removed");
				entity.Destroy();
			}
		}
	}
}