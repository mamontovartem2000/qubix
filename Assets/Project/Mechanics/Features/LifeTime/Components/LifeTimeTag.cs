using ME.ECS;

namespace Project.Mechanics.Features.Lifetime.Components {

    public struct LifeTimeTag : IComponent {}

    public struct LifeTime : IComponent
    {
        public float Value;
    }

    public struct LifeTimeDefault : IComponent
    {
        public float Value;
    }
}