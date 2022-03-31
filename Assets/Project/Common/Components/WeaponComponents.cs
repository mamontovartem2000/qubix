using ME.ECS;
using Project.Mechanics.Features.Weapon.Views;

namespace Project.Mechanics.Components
{
    public struct AutomaticWeapon : IComponent {}
    public struct LinearWeapon : IComponent {}
    public struct TrajectoryWeapon : IComponent {}
    public struct MeleeWeapon : IComponent {}

    public struct WeaponAim : IComponent
    {
        public Entity Aim;
    }

    public struct NeedView : IComponent
    {
        public WeaponMono View;
    }

    public struct ReloadTime : IComponent
    {
        public float Value;
    }

    public struct ReloadTimeDefault : IComponent
    {
        public float Value;
    }

    public struct Cooldown : IComponent
    {
        public float Value;
    }

    public struct CooldownDefault : IComponent
    {
        public float Value;
    }

    public struct AmmoCapacity : IComponent
    {
        public int Value;
    }

    public struct AmmoCapacityDefault : IComponent
    {
        public int Value;
    }

    public struct SpreadAmount : IComponent
    {
        public float Value;
    }
}