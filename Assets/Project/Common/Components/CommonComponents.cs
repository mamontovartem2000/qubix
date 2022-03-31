using ME.ECS;

namespace Project.Common.Components 
{
    public struct ApplyDamage : IComponent
    {
        public float Damage;
        public Entity ApplyTo;
        public Entity From;
    }

    public struct WeaponShot : IComponent {}

}