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
    
    //test components for new collision detection system implementation
    public struct CollisionTag : IComponent {}
    public struct CircleRect : IComponent
    {
        public float Radius;
    }

    public struct SquareRect : IComponent
    {
        public float Width;
        public float Height;
    }

    public struct TriangleRect : IComponent
    {
        public float Length;
        public float Width;
    }
}