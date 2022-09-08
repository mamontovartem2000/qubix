using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using UnityEngine;

namespace Project.Features.Skills.Systems.Universal
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class GrenadeThrowSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-GrenadeThrowSkillSystem")
				.With<GrenadeThrowAffect>()
				.With<ActivateSkill>()
				.Push();
		}

		// ReSharper disable Unity.PerformanceAnalysis
		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var avatar = entity.Owner().Avatar();
			if (avatar.IsAlive() == false) return;

			var grenade = new Entity("grenade");
			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			grenade.Set(new Owner{Value = entity.Read<Owner>().Value});
			grenade.Set(new Grenade());
			entity.Read<ProjectileConfig>().Value.Apply(grenade);
			grenade.Get<ProjectileDirection>().Value = new Vector3(avatar.Read<FaceDirection>().Value.x * 1f, grenade.Read<Trajectory>().Value, avatar.Read<FaceDirection>().Value.z * 1f) ;
            grenade.SetPosition(avatar.GetPosition());
			
			SoundUtils.PlaySound(avatar, entity.Read<SoundPath>().Value);
			
			var view = world.RegisterViewSource(grenade.Read<ViewModel>().Value);
            grenade.InstantiateView(view);
		}
	}
}