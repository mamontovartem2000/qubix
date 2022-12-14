using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Common.Components
{
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

	public struct Freeze : IComponent
	{
		public float LifeTime;
	}
	public struct FreezeModifier : IComponent
	{
		public float LifeTime;
		public DataConfig VFXConfig;
	}

	public struct DisarmModifier : IComponent
	{
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
	
	public struct MeleeProjectile : IComponent {}

	public struct ShengbiaoProjectile : IComponent {}

	public struct LastHitEntity : IComponent
	{
		public Entity Value;
	}

	public struct ProjectileActive : IComponent {}
	
	public struct ShouldDestroy : IComponent {}
	
	public struct Trajectory : IComponent
	{
		public float Value;
	}
	
	public struct BoomerangEffect : IComponent
	{
		public float Value;
	}
	
	public struct Debuff : IComponent {}
	
	public struct Grenade : IComponent {}
	public struct GrenadeExplode : IComponent {}
	
	public struct Storm : IComponent {}

	public struct ExplodeSquaredRadius : IComponent
	{
		public float Value;
	}
	
	public struct LinearVisual : IComponent {}
}