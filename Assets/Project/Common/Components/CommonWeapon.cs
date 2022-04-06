using ME.ECS;

namespace Project.Common.Components {

    public struct WeaponTag : IComponent
    {
        public int ActorID;
        public WeaponHand Hand;
    }

    public struct Trajectory : IComponent
    {
        public float Value;
    }
}