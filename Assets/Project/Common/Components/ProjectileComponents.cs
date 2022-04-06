using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Common.Components
{
	public struct ProjectileView : IComponent
	{
		public MonoBehaviourViewBase Value;
	}
	
	public struct ProjectileSpeed : IComponent
	{
		public float Value;
	}

	public struct ProjectileSpeedModifier : IComponent
	{
		public float Value;
	}

	public struct ProjectileDirection : IComponent
	{
		public Vector3 Value;
	}

	public struct ProjectileDamage : IComponent
	{
		public float Value;
	}

	public struct ProjectileDamageModifier : IComponent
	{
		public float Value;
	}

	public struct Linear : IComponent
	{
		public float StartDelay;
		public float EndDelay;
	}
	
	public struct LinearActive : IComponent {}

	public struct Melee : IComponent
	{
		public float StartDelay;
		public float EndDelay;
	}
	
	public struct MeleeActive : IComponent {}
	
	public struct ProjectileActive : IComponent {}
}