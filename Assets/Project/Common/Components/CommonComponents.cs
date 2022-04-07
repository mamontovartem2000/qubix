using ME.ECS;

namespace Project.Common.Components 
{
    public struct ApplyDamage : IComponent
    {
        public float Damage;
        public Entity ApplyTo;
        public Entity ApplyFrom;
    }

    public struct WeaponShot : IComponent {}
    
    public struct LifeTimeLeft : IComponent
    {
        public float Value;
    }
    
    public struct LifeTimeDefault : IComponent
    {
        public float Value;
    }
    
    public struct LeftWeaponShot : IComponent{}
    public struct RightWeaponShot : IComponent{}

}