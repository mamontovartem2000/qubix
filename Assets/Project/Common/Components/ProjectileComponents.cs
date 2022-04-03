using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Mechanics.Features.Projectile.Views;
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
		public Entity Value;
	}

	public struct LinearActive : IComponent
	{
		
	}
}