using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;

namespace Project.Common.Components
{
    public enum WeaponHand
    {
        Left, Right
    }
    public struct AutomaticWeapon : IComponent {}

    public struct LinearWeapon : IComponent
    {
        public int Length;
    }
    
    public struct LinearFull : IComponent {}
    public struct TrajectoryWeapon : IComponent {}

    public struct MeleeWeapon : IComponent
    {
        public int Length;
    }
    public struct WeaponAim : IComponent
    {
        public Entity Aim;
    }
    
    public struct ProjectileConfig : IComponent
    {
        public DataConfig Value;
    }

    public struct WeaponView : IComponent
    {
        public MonoBehaviourViewBase View;
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