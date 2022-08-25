using ME.ECS;

namespace Project.Common.Components {

    public struct PowerUpTag : IComponent
    {
        public float Time;
    }
    
    public struct PowerUpTileTag : IComponent { }
    
    public struct PowerUpCrystalTag : IComponent { }
    
    public struct PowerUpNeedRespawn : IComponent { }
    
}