using ME.ECS;

namespace Project.Common.Components {

    public struct WeaponTag : IComponent
    {
        public int ActorID;
        public WeaponHand Hand;
    }

    public struct ProjectileTag : IComponent {}

    public struct LaserDeactivate : IComponent {}

    public struct Trajectory : IComponent
    {
        public float Value;
    }


   

    public enum WeaponType
    {
        RocketLauncher,
        AutoRifle,
        SniperRifle,
        Flamethrower,
        GrenadeLauncher,
        LaserSword,
        Melee
    }

}