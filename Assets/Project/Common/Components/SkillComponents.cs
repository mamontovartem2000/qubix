using ME.ECS;

namespace Project.Common.Components
{
    public struct SkillComponents : IComponent {}

    public enum SkillTarget
    {
        Self, Other, Target, Area
    }

    public enum SkillAttribute
    {
        MoveSpeed, FireRate, Cooldown, HealthAmount, ArmorAmount, AmmoCap, ReloadRate
    }

    public enum RechargeType
    {
        Immediate, PostEffect
    }

    public struct ActiveSkill : IComponent
    {
        public SkillTarget Target;
        public SkillAttribute Attribute;
        public float Amount;
        public float Duration;
        public float Cooldown;
        public RechargeType Recharge;
    }
    
    public struct PassiveSkill : IComponent
    {
        public SkillAttribute Attribute;
        public float Amount;
    }
    
    public struct ToggleSkill : IComponent
    {
        public SkillTarget Target;
        public SkillAttribute Attribute;
        public float Amount;
    }
    
    public struct ChannelSkill : IComponent
    {
        public SkillAttribute Attribute;
        public float Amount;
        public float Cooldown;
        public RechargeType Recharge;
    }
    
    public struct FirstSkillTag : IComponent
    {
        public int ActorID;
    }

    public struct SecondSkillTag : IComponent
    {
        public int ActorID;
    }

    public struct ThirdSkillTag : IComponent
    {
        public int ActorID;
    }

    public struct FourthSkillTag : IComponent
    {
        public int ActorID;
    }
}