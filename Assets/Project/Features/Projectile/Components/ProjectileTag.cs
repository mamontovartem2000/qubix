﻿using ME.ECS;
using UnityEngine;

namespace Project.Features.Projectile.Components {
    public struct ProjectileTag : IComponent
    {
        public int ActorID;
    }

    public struct ProjectileType : IComponent
    {
        public ProjectileBase Type;
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

    public struct LeftWeaponCooldown : IComponent
    {
        public float Value;
    }

    public struct RightWeaponCooldown : IComponent
    {
        public float Value;
    }
}

public enum ProjectileBase
{
    Bullet, Rocket
}