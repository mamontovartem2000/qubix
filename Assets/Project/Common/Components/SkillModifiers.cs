using ME.ECS;
namespace Project.Common.Components
{
	public struct MoveSpeedModifier : IComponent
	{
		public float Value;
	}

	public struct WeaponCritChanceDefault : IComponent
	{
		public float Value;
	}
	public struct CriticalHitModifier : IComponent
	{
		public float LifeTime;
		public float Value;
	}
	
	public struct CriticalHit : IComponent
	{
		public float Value;
	}

	public struct Stun : IComponent
	{
		public float Value;
	}
	
	public struct StunModifier : IComponent
	{
		public int Value;
	}

	public struct DashModifier : IComponent
	{
		public int Step;
	}

	public struct SecondLifeModifier : IComponent {}
	
	public struct EMP : IComponent
	{
		public float LifeTime;
	}

	public struct EMPModifier : IComponent
	{
		public float LifeTime;
		public int AmmoCapacityDefault;
	}

	public struct FireRateModifier : IComponent
	{
		public int Value;
	}

	public struct ForceShieldModifier : IComponent
	{
		public float Value;
	}

	public struct LinearPowerModifier : IComponent
	{
		public float Damage;
	}
}