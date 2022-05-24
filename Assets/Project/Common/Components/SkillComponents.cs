using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Common.Components
{
    public struct SkillTag : IComponent 
    {
        public int id;
    }
    public struct EffectTag : IComponent {}
    public struct PassiveSkill : IComponent {}
    public struct ActivateSkill : IComponent {}
    public struct AOESkill : IComponent
    {
        public float Value;
    }
    public struct Targeted : IComponent {}

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

    public struct SkillAmount : IComponent
    {
        public float Value;
    }
    
    public struct BuffTrigger : IComponentOneShot {}
    public struct SelfTrigger : IComponentOneShot {}
    
    public struct StatsBuff : IComponent {}
    public struct ComponentBuff : IComponent {}

    public struct SkillImage : IComponent
    {
        public int Value;
    }

    public struct SkillVFX : IComponent
    {
        public MonoBehaviourViewBase Value;
    }
}