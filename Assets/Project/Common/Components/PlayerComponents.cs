using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Common.Components
{
    public struct Owner : IComponent
    {
        public Entity Value;
    }
    
    public struct AvatarTag : IComponent {}
    public struct NeedAvatar : IComponent
    {
        public MonoBehaviourViewBase Value;
    }
    
    public struct PlayerAvatar : IComponent
    {
        public Entity Value;
    }
    
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

    public struct WeaponEntities : IComponent
    {
        public Entity LeftWeapon;
        public Entity RightWeapon;
    }

    public struct AvatarSettings : IComponent
    {
        public DataConfig PlayerConfig;
        public DataConfig LeftWeaponConfig;
        public DataConfig RightWeaponConfig;
    }
}