using ME.ECS;
namespace Project.Common.Components
{
	public struct MoveSpeedModifier : IComponent
	{
		public float Value;
	}

	public struct LifeStealModifier : IComponent
	{
		public float Value;
	}

	public struct HealingModifier : IComponent
	{
		public float Value;
	}

	public struct Stun : IComponent
	{
		public float Value;
	}
	
	public struct StunModifier : IComponent
	{
		public float Value;
	}

	public struct FireRateModifier : IComponent
	{
		public float Value;
	}

	public struct ForceShieldModifier : IComponent
	{
		public float Value;
	}

	public struct InstantReloadModifier : IComponent
	{
		public float Value;
	}

	public struct SkillsResetModifier : IComponent
	{
		public float Value;
	}

	public struct MagneticStormModifier : IComponent
	{
		public float Value;
	}

	public struct AutomaticDamageModifier : IComponent
	{
		public float Value;
	}

	public struct MeleeDamageModifier : IComponent
	{
		public float Value;
	}

	public struct SkillSilenceModifier : IComponent
	{
		public float Value;
	}

	public struct GrenadeThrowModifier : IComponent
	{
		public float Value;
	}

	public struct LandMineModifier : IComponent
	{
		public float Value;
	}

	public struct FiringRangeModifier : IComponent
	{
		public float Value;
	}

	public struct SideStepModifier : IComponent
	{
		public float Value;
	}

	public struct QuickStepModifier : IComponent
	{
		public float Value;
	}

	public struct LinearPowerModifier : IComponent
	{
		public float Damage;
		public int Speed;
	}

	public struct WormholeHookModifier : IComponent
	{
		public float Value;
	}

	public struct PersonalTeleportModifier : IComponent
	{
		public float Value;
	}
}