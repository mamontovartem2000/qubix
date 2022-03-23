using ME.ECS;

namespace Project.Features.Player.Components 
{   
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
}