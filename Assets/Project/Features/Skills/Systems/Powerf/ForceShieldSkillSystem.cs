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
	public sealed class ForceShieldSkillSystem : ISystemFilter
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
			return Filter.Create("Filter-ForceShieldSkillSystem")
				.With<ForceShieldAffect>()
				.With<ActivateSkill>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			var avatar = entity.Owner(out var owner).Avatar();
			if (avatar.IsAlive() == false) return;

			var effect = new Entity("effect");
			effect.Set(new EffectTag());
			effect.Get<Owner>().Value = owner;
			
			avatar.Set(new ForceShieldModifier{Value = 50});
		
			effect.Set(new ForceShieldModifier());
			effect.Get<LifeTimeLeft>().Value = entity.Read<SkillDurationDefault>().Value;
			
			effect.SetParent(avatar);
			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
			
			SoundUtils.PlaySound(avatar, "event:/Skills/Powerf/Shield");
			
			_vfx.SpawnVFX(VFXFeature.VFXType.PlayerShield, avatar.GetPosition(), effect);
		}
	}
}