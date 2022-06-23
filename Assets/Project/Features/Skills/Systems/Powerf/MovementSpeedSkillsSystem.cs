using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.Skills.Systems.Powerf
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class MovementSpeedSkillsSystem : ISystemFilter
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
			return Filter.Create("Filter-MovementSpeedSkillsSystem")
				.With<MoveSpeedAffect>()
				.Push();
		}

		// ReSharper disable Unity.PerformanceAnalysis
		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var avatar = entity.Owner(out var owner).Avatar();
			if (avatar.IsAlive() == false) return;
			
			var effect = new Entity("effect");
			effect.Get<Owner>().Value = owner;
			effect.Set(new EffectTag());

			var amount = entity.Read<SkillAmount>().Value / 100f;
			effect.Get<MoveSpeedModifier>().Value = amount;
			effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;
			
			SoundUtils.PlaySound(avatar, "event:/Skills/Powerf/MovementSpeedIncreace");
			
			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			_vfx.SpawnVFX(VFXFeature.VFXType.SkillQuickness, avatar.GetPosition(), avatar);
			_vfx.SpawnVFX(VFXFeature.VFXType.SpeedTrail, avatar.GetPosition(),avatar, entity.Read<SkillDurationDefault>().Value);
		}
	}
}