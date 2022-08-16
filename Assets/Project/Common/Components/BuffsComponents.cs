using ME.ECS;
using UnityEngine;

namespace Assets.Project.Common.Components {

    public struct HealingBuff : IComponent
    {
        public float TimeInterval;
        public float HealsPercent;
        [HideInInspector] public float LastHealingTime;
    }
    
    public struct SlowdownBuff : IComponent
    {
        public float PercentValue;
    }
}