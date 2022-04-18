using ME.ECS;
using ME.ECS.DataConfigs;

namespace Project.Common.Components
{
    public struct SkillTag : IComponent {}
    public struct PassiveSkill : IComponent {}
    public struct ActivateSkill : IComponentOneShot {}

    public struct SkillConfig : IComponent
    {
        public DataConfig Value;
    }

    public struct SkillDuration : IComponent
    {
        public float Value;
    }

    public struct SkillDurationDefault : IComponent
    {
        public float Value;
    }

    public struct SkillEffect : IComponent
    {
        public SkillAttribute Value;
    }

    public struct SkillAmount : IComponent
    {
        public float Value;
    }

    public enum SkillAttribute
    {
        MoveSpeed, AttackSpeed
    }
}