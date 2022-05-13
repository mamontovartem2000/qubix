using ME.ECS;

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
    public struct TestTag   : IComponent {}
}