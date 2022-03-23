using ME.ECS;

namespace Project.Features.SceneBuilder.Components 
{
    public struct HealPoweUpTag : IComponent {}

    public struct MineTag : IComponent {}

    public struct AmmoTag : IComponent
    {
        public Entity Spawner;
        public WeaponType WeaponType;
        public int MaxAmmoCount;
        public int AmmoCount;
        public float WeaponCooldown;
    }
    
    public struct AmmoTileTag : IComponent
    {
        public bool Spawned;
        public float Timer;
        public float TimerDefault;
    }
}