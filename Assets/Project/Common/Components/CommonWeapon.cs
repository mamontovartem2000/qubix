using ME.ECS;

namespace Project.Common.Components {

    public struct WeaponTag : IComponent
    {
        public WeaponHand Hand;
        public WeaponType Gun;
    }

    public struct ProjectileTag : IComponent {}

    public struct WeaponShot : IComponent {}
    public struct LaserDeactive : IComponent {}

    public enum WeaponHand
    {
        Left,
        Right
    }

    public enum WeaponType
    {
        RocketLauncher,
        AutoRifle,
        SniperRifle,
        FlameTrower,
        GrenadeLauncher,
        LaserSword,
        Melee
    }

}