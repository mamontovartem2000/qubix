using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Common.Components
{
	public struct ProjectileView : IComponent
	{
		public MonoBehaviourViewBase Value;
	}
	
	public struct ProjectileAlternativeView : IComponent
	{
		public MonoBehaviourViewBase Value;
	}
	
	public struct SecondaryDamage : IComponent
	{
		public DataConfig Value;
	}
	
	public struct ExplosionSound : IComponent
	{
		public DataConfig Value;
	}

	public struct ProjectileDirection : IComponent
	{
		public fp3 Value;
	}

	public struct ProjectileSpeed : IComponent
	{
		public float Value;
	}

	public struct ProjectileDamage : IComponent
	{
		public float Value;
	}

	public struct Slowness : IComponent
	{
		public float Value;
		public float LifeTime;
	}

	public struct Linear : IComponent
	{
		public float StartDelay;
		public float EndDelay;
	}

	public struct LinearIndex : IComponent
	{
		public int Value;
	}

	public struct LinearActive : IComponent {}

	public struct MeleeActive : IComponent {}

	public struct ProjectileActive : IComponent {}
	
	public struct Trajectory : IComponent
	{
		public float Value;
	}
	
	public struct Debuff : IComponent {}
	
	public struct Grenade : IComponent {}
	
	public struct Storm : IComponent {}

	public struct LinearVisual : IComponent {}
	
	public struct DamageSource : IComponent {}
}