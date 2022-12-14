using System;
using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Common.Components
{
    public struct AutomaticWeapon : IComponent {}
    public struct LinearWeapon : IComponent {public int Value;}
    public struct TrajectoryWeapon : IComponent {}
    public struct MeleeWeapon : IComponent {}
    public struct LinearFull : IComponent {}
    public struct WeaponAim : IComponent {public Entity Value;}
    public struct MeleeDamageSpot : IComponent{public Entity Value;}
    public struct ShengbiaoDamageSpot : IComponent{public Entity Value;}

    public struct ProjectileParent : IComponent
    {
        public float Speed;
        public Entity Bullet;
    }
    public struct MeleeAimer : IComponent {}
    public struct ShengbiaoShot : IComponent
    {
        public float Speed;
    }

    public struct ShengbiaoWeapon : IComponent
    {
        public float Offset;
        public float MoveRatio;
    }
    public struct ProjectileConfig : IComponent {public DataConfig Value;}

    public struct VFXConfig : IComponent
    {
        public DataConfig Value;
        public DataConfig SecondaryValue;
    }
    public struct ViewModel : IComponent {public MonoBehaviourViewBase Value;}
    public struct ReloadTime : IComponent {public float Value;}
    public struct ReloadTimeDefault : IComponent {public float Value;}
    public struct Cooldown : IComponent {public float Value;}
    public struct CooldownDefault : IComponent {public float Value;}
    public struct AmmoCapacity : IComponent {public int Value;}
    public struct AmmoCapacityDefault : IComponent {public int Value;}
    public struct ModifiersCheck : IComponent {public float Value;}
    public struct MeleeDelay : IComponent {public float Value;}
    public struct MeleeDelayDefault : IComponent {public float Value;}
    public struct WeaponPosition : IComponent {public fp3 Value;}
    public struct FiringCooldownModifier : IComponent {public float Value;}
    public struct SingleBullet : IComponent {}
    public struct SpawnBullet : IComponent {public Entity LastBullet;}
    public struct Shotgun : IComponent  {public int AmmoCount;}
    public struct SoundPath : IComponent {public string Value;}
    public struct PrivateSoundPath : IComponent {public string Value;}
}