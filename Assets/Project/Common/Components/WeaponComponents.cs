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
    public struct MeleeAimer : IComponent {};
    public struct ProjectileConfig : IComponent {public DataConfig Value;}
    public struct WeaponView : IComponent {public MonoBehaviourViewBase Value;}
    public struct ReloadTime : IComponent {public float Value;}
    public struct ReloadTimeDefault : IComponent {public float Value;}
    public struct Cooldown : IComponent {public float Value;}
    public struct CooldownDefault : IComponent {public float Value;}
    public struct AmmoCapacity : IComponent {public int Value;}
    public struct AmmoCapacityDefault : IComponent {public int Value;}
    public struct SpreadAmount : IComponent {public float Value;}
    public struct MeleeDelay : IComponent {public float Value;}
    public struct MeleeDelayDefault : IComponent {public float Value;}
    public struct WeaponPosition : IComponent {public fp3 Value;}
    public struct FiringCooldownModifier : IComponent {public float Value;}
    public struct SoundPath : IComponent {public string Value;}
}