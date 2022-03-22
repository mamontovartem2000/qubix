using ME.ECS;

namespace Project.Core.Features.Player.Components 
{
    public struct HealthComponents : IComponent {}
    
    public struct PlayerHealth : IComponent
    {
        public float Value;
    }
    public struct DeadBody : IComponent
    {
        public int ActorID;
        public float Time;
    }
    public struct LastHit : IComponent
    {
        public Entity Enemy;
    }
    public struct RespawnTag : IComponent {}
    
}