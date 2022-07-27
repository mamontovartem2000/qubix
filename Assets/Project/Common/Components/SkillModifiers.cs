using ME.ECS;
using ME.ECS.DataConfigs;

namespace Project.Common.Components
{
	public struct MoveSpeedModifier : IComponent
	{
		public float Value;
	}
	
	public struct CriticalHitModifier : IComponent
	{
		public float LifeTime;
	}
	
	public struct CriticalHit : IComponent
	{
		public float Value;
	}

	public struct Stun : IComponent
	{
		public float LifeTime;
	}
	
	public struct StunEffect : IComponent {}
	
	public struct StunModifier : IComponent
	{
		public int AmmoCapacityDefault;
		public int LifeTime;
		public DataConfig VFXConfig;
	}

	public struct DashModifier : IComponent
	{
		public int Step;
	}
	
	public struct ModifierConfig : IComponent
	{
		public DataConfig Value;
	}
	
	public struct SecondLifeModifier : IComponent {}
	
	public struct CyberVampyrModifier : IComponent {}
	
	public struct EMP : IComponent
	{
		public float LifeTime;
	}

	public struct EMPModifier : IComponent
	{
		public float LifeTime;
		public int AmmoCapacityDefault;
		public DataConfig VFXConfig;

	}
	
	public struct EMPEffect : IComponent {}

	public struct FireRateModifier : IComponent
	{
		public int Value;
	}

	public struct ForceShieldModifier : IComponent
	{
		public float Value;
	}

	public struct SoftShieldModifier : IComponent
	{
		public float LifeTime;
	}
	public struct LinearPowerModifier : IComponent
	{
		public float Damage;
	}
}