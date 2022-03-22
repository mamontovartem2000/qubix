using ME.ECS;

namespace Project.Mechanics.Features.CollisionHandler.Components 
{
    public struct CollisionTag : IComponent
    {
        public Entity Collision;
        public Entity Player;
    }

    

    public struct PortalTag : IComponent
    {
        public int PortalID;
    }
    
    public struct ExplosionTag : IComponent
    {
        public float Value;
    }
}