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
    
    public struct SpikesTag : IComponent
    {
        public float Timer;
        public float TimerDefault;
    }
    
    public struct SpikesProjectileTag : IComponent {}

    public struct PortalDispenserTag : IComponent
    {
        public float Timer;
        public float TimerDefault;
    }
    
    public struct Spawned : IComponent {}
    
    public struct SpawnedPortal : IComponent {}

    public struct PortalTag : IComponent { }

    public struct AvoidTeleport : IComponent{public fp Value;}
}