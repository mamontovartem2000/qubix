using ME.ECS;

namespace Project.Features.Weapon.Components {

    public struct WeaponTag : IComponent
    {
        public WeaponType Weapon;
        public AmmoType Ammo;
    }

    public struct FiringCooldown : IComponent
    {
        public float Value;
    }

    public struct Magazine : IComponent
    {
        public int Rounds;
        public int Capacity;
    }

    public struct Realod : IComponent
    {
        public float Value;
    }
    
    public enum WeaponType
    {
        Gun, RocketLauncher, SniperRifle, ShotGun, Laser, FlameThrower, Grenade, Mine
    }

    public enum AmmoType
    {
        Bullet, Rocket, Beam, Grenade, Mine
    }
}