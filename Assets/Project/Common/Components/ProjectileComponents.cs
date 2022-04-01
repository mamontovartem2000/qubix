using ME.ECS;
using Project.Mechanics.Features.Projectile.Views;
using UnityEngine;

namespace Project.Common.Components
{
	public struct ProjectileView : IComponent
	{
		public BulletMono Value;
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
}