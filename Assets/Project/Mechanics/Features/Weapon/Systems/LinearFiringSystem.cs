using ME.ECS;
using Project.Common.Components;
using Project.Mechanics.Features.Projectile;

namespace Project.Mechanics.Features.Weapon.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class LinearFiringSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private WeaponFeature _feature;
        private ProjectileFeature _projectile;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _projectile);
        }

        void ISystemBase.OnDeconstruct() {}

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-NewLinearFiringSystem")
                .With<LinearWeapon>()
                .With<LeftWeaponShot>()
                .Without<LinearActive>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if(entity.GetParent().Has<StunModifier>()) return;

            var len = entity.Read<LinearWeapon>().Length;
            _projectile.SpawnLinear(entity, len, deltaTime);
        }
    }
}