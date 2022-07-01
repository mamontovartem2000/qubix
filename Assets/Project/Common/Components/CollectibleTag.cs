using ME.ECS;

namespace Project.Common.Components 
{
    public struct HealthDispenserTag : IComponent {}

    public struct MineTag : IComponent {}

    public struct DispenserTag : IComponent
    {
        public float Timer;
        public float TimerDefault;
    }
    public struct PortalDispenserTag : IComponent
    {
        public float Timer;
        public float TimerDefault;
    }
    
    public struct Spawned : IComponent {}
    public struct SpawnedPortal : IComponent {public Entity Value;}

    public struct PortalTag : IComponent { }

    public struct AvoidTeleport : IComponent{public fp Value;}
}