using ME.ECS;
using UnityEngine;

namespace Project.Common.Components {

    public struct PowerUpBuff : IComponent
    {
        [HideInInspector] public float Time;
    }
    
    public struct PowerUpTileTag : IComponent { }
    
    public struct PowerUpCrystalTag : IComponent { }

    public struct PowerUpNeedRespawn : IComponent
    {
        [HideInInspector] public float Delay;
    }
    
}