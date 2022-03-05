using ME.ECS;

namespace Project.Features.CollisionHandler.Components 
{
    public struct CollisionTag : IComponent
    {
        public Entity Collision;
        public Entity Player;
    }

    public struct ApplyDamage : IComponent
    {
        public float Damage;
        public Entity ApplyTo;
        public Entity From;
    }

    public struct PortalTag : IComponent
    {
        public int PortalID;
    }
    
    public struct PlayerCollided : IComponent
    {
        public int Value;
    }
}