using ME.ECS;

namespace Project.Features.SceneBuilder.Components {

    public struct CollectibleTag : IComponent {}

    public struct PowerUpTag : IComponent
    {
        public PowerUpType Type;
    }

    public struct TrapTag : IComponent
    {
        public TrapType Type;
    }

    public enum PowerUpType
    {
        Health
    }
    
    public enum TrapType
    {
        Mine
    }
}