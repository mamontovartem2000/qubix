using ME.ECS;

namespace Project.Core.Features.Player.Components 
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

    public struct DamagedBy : IComponent
    {
        public Entity Value;
    }
}