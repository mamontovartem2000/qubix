using ME.ECS;

namespace Project.Common.Components 
{   
    public struct PlayerHealth : IComponent
    {
        public float Value;
    }

    public struct PlayerDead : IComponent
    {
        public fp3 DeathPosition;
    }

    public struct PlayerHealthOverlay : IComponent
    {
        public float Value;
    }

    public struct PlayerHealthDefault : IComponent
    {
        public float Value;
    }

    public struct LifeTimeLeft : IComponent
    {
        public float Value;
    }
    
    public struct LifeTimeDefault : IComponent
    {
        public float Value;
    }
    
    public struct Invulnerable : IComponent { }
}