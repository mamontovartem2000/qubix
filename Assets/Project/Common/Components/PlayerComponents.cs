using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Common.Components
{
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
        public int PlayerLocalID;
        public string PlayerServerID;
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

    public struct SkillEntities : IComponent
    {
        public Entity FirstSkill;
        public Entity SecondSkill;
        public Entity ThirdSkill;
        public Entity FourthSkill;
    }

    public struct PlayerConfig : IComponent
    {
        public DataConfig AvatarConfig;
        public DataConfig LeftWeaponConfig;
        public DataConfig RightWeaponConfig;
        
        public DataConfig FistSkillConfig;
        public DataConfig SecondSkillConfig;
        public DataConfig ThirdSkillConfig;
        public DataConfig FourthSkillConfig;
    }

    public struct PlayerArmor : IComponent
    {
        public float Value;
    }
}