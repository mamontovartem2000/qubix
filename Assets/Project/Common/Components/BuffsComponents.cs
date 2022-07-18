using ME.ECS;

namespace Assets.Project.Common.Components {

    public struct HealingBuff : IComponent
    {
        public float TimeInterval;
        public float HealsPercent;
        public float LastHealingTime;
    }
    
}