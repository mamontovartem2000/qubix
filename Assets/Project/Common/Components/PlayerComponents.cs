using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using Project.Modules.Network;
using Unity.Mathematics;

namespace Project.Common.Components
{
    public struct AvatarTag : IComponent {}
    public struct AvatarView : IComponent {public MonoBehaviourViewBase Value;}
    public struct PlayerAvatar : IComponent {public Entity Value;}
    public struct RespawnTime : IComponent {public float Value;}
    public struct FaceDirection : IComponent {public float3 Value;}
    public struct PlayerArmor : IComponent {public float Value;}
    public struct PlayerTag : IComponent 
    {
        public int PlayerLocalID;
        public string PlayerServerID;
        public string Nickname;
        public TeamTypes Team;
    }
    public struct WeaponEntities : IComponent
    {
        public Entity LeftWeapon;
        public Entity RightWeapon;
    }
    public struct PlayerScore : IComponent
    {
        public int Kills;
        public int Deaths;
        public float DealtDamage;
    }
    public struct PlayerConfig : IComponent 
    {
        public DataConfig AvatarConfig;
        public DataConfig LeftWeaponConfig;
        public DataConfig RightWeaponConfig;
        
        public DataConfig FirstSkillConfig;
        public DataConfig SecondSkillConfig;
        public DataConfig ThirdSkillConfig;
        public DataConfig FourthSkillConfig;
    }
    public struct SkillEntities : IComponent
    {
        public Entity FirstSkill;
        public Entity SecondSkill;
        public Entity ThirdSkill;
        public Entity FourthSkill;
    }

    public struct Hover : IComponent
    {
        public bool Direction;
        public float Amount;
    }

    public struct PlayerDamaged : IComponent
    {
        public float Value;
    }

    public struct PlayerDamagedCounter : IComponent
    {
        public float Value;
    }
}