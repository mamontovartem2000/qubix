using ME.ECS;

namespace Project.Mechanics.Features.Skills.Components
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
}