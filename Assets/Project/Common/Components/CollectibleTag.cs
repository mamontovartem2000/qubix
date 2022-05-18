using ME.ECS;

namespace Project.Common.Components 
{
    public struct HealthTag : IComponent {}

    public struct MineTag : IComponent {}

    public struct DispenserTag : IComponent
    {
        public float Timer;
        public float TimerDefault;
    }
    
    public struct Spawned : IComponent {}

    public struct PortalTag : IComponent { }

}