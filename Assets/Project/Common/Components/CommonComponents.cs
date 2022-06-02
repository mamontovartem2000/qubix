using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Common.Components 
{
    public struct ApplyDamage : IComponent
    {
        public float Damage;
        public Entity ApplyTo;
        public Entity ApplyFrom;
    }
    public struct DamagedBy : IComponent {public Entity Value;}
    public struct Owner : IComponent {public Entity Value;}
    public struct LeftWeaponShot : IComponent{}
    public struct RightWeaponShot : IComponent{}
    public struct SpeedModifier : IComponent {}
    public struct CollisionDynamic : IComponent {}
    public struct CollisionStatic : IComponent {}
    public struct Collided : IComponent
    {
        public Entity ApplyFrom;
        public Entity ApplyTo;
    }
    public struct PortalActive : IComponent {}
    public struct GamePaused : IComponent {}
    public struct GameFinished : IComponent { }
    public struct GameTimer : IComponent {public float Value;}
    public struct MapInitialized : IComponent {}
    public struct Tabulation : IComponent {}
    public struct SoundPlay : IComponent {}
    public struct SoundEffect : IComponent
    {
        public MonoBehaviourViewBase Sound0;
        public MonoBehaviourViewBase Sound1;
        public MonoBehaviourViewBase Sound2;
        public MonoBehaviourViewBase Sound3;
    }
}