using ME.ECS;
using ME.ECS.DataConfigs;
using UnityEngine;

namespace Project.Common.Components
{
    public struct NeedAvatar : IComponent {}
    
    public struct PlayerTag : IComponent
    {
        public int PlayerID; 
    }
    
    public struct RespawnTime : IComponent
    {
        public float Value;
    }
    
    public struct FaceDirection : IComponent
    {
        public Vector3 Value;
    }

    public struct PlayerScore : IComponent
    {
        public int Kills;
        public int Deaths;
    }

    public struct PlayerEntity : IComponent
    {
        public Entity Value;
    }

    public struct AvatarSettings : IComponent
    {
        public DataConfig PlayerConfig;
        public DataConfig LeftWeaponConfig;
        public DataConfig RightWeaponConfig;
    }
}