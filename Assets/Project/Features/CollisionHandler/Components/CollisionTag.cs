using ME.ECS;

namespace Project.Features.CollisionHandler.Components 
{
    public struct CollisionTag : IComponent
    {
        public Entity Collision;
    }

    public struct ApplyDamage : IComponent
    {
        public float Value;
    }

    public struct PortalTag : IComponent
    {
        public int PortalID;
    }
}