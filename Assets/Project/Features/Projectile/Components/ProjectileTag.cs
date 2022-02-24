using ME.ECS;
using UnityEngine;

namespace Project.Features.Projectile.Components {
    public struct ProjectileTag : IComponent
    {
        public int ActorID;
    }

    public struct ProjectileSpeed : IComponent
    {
        public float Value;
    }

    public struct ProjectileDirection : IComponent
    {
        public Vector3 Value;
    }

    public struct ProjectileDamage : IComponent
    {
        public int Value;
    }
    
    public struct ProjectileShouldDie : IComponent {}
}