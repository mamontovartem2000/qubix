using ME.ECS;
using UnityEngine;

namespace Project.Mechanics.Features.Weapon.Components
{
    
    public struct WeaponCooldown : IComponent
    {
        public float Value;
    }

    public struct WeaponCooldownDefault : IComponent
    {
        public float Value;
    }

    public struct WeaponReloadTime : IComponent
    {
        public float Value;
    }

    public struct WeaponReloadTimeDefault : IComponent
    {
        public float Value;
    }

    public struct WeaponAmmo : IComponent
    {
        public float Value;
    }

    public struct WeaponAmmoDefault : IComponent
    {
        public float Value;
    }

    public struct WeaponSpread : IComponent
    {
        public Vector3 Value;
    }

    
}