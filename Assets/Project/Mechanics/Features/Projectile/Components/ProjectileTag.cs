using ME.ECS;
using UnityEngine;

namespace Project.Mechanics.Features.Projectile.Components
{    
    public struct ProjectileSpeed : IComponent
    {
        public float Value;
    }

    public struct ProjectileDirection : IComponent
    {
        public Vector3 Value;
    }

    public struct ProjectileSpeedModifier : IComponent
    {
        public float Value;
    }

    public struct ProjectileIsLaser : IComponent {}

    public struct ProjectileLaserIsntActive : IComponent {}
}