using ME.ECS;

namespace Project.Core.Features.SceneBuilder.Components 
{
    public struct HealPoweUpTag : IComponent {}

    public struct MineTag : IComponent {}

    public struct DispenserTag : IComponent
    {
        public float Timer;
        public float TimerDefault;
    }
    
    public struct Spawned : IComponent {}

    public struct PortalTag : IComponent { }

}