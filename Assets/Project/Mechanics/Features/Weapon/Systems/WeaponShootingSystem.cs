using ME.ECS;
using UnityEngine;
using Project.Mechanics.Features.Projectile;


namespace Project.Mechanics.Features.Weapon.Systems
{
    #region usage
#pragma warning disable
    using Components;
    using Core.Features.Player;
    using Input.InputHandler;
    using Markers;
    using Modules;
    using Project.Common.Components;
    using Project.Components;
    using Project.Markers;
    using Project.Mechanics.Features.Lifetime.Components;
    using Project.Modules;
    using Project.Systems;
    using Projectile.Components;
    using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class WeaponShootingSystem : ISystemFilter
    {
        private Filter _projectileFilter;
        private WeaponFeature _feature;
        private ProjectileFeature _projectileFeature;
        private PlayerFeature _playerFeature;
        public World world { get; set; }
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);
            world.GetFeature(out _projectileFeature);
            world.GetFeature(out _playerFeature);

            Filter.Create("Projectile_Filter")
                .With<ProjectileTag>()
                .Push(ref _projectileFilter);
        }
        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-WeaponShootingSystem")
                .With<WeaponShot>()
                .Without<WeaponCooldown>()
                .Without<WeaponReloadTime>()
                .Push();
        }
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            entity.Get<WeaponAmmo>().Value--;

            var spread = entity.Read<WeaponSpread>().Value;
            spread = new Vector3(world.GetRandomRange(-spread.x, spread.x), spread.y, world.GetRandomRange(-spread.z, spread.z));

            var direction = spread;

            if (entity.Read<WeaponTag>().Hand == WeaponHand.Left)
            {
                direction += _feature.LeftDestinationPoint.GetPosition() - entity.GetPosition();
                _projectileFeature.SpawnProjectile(entity, direction, _feature.CurrentLeft);
            }
            else
            {
                direction += _feature.RightDestinationPoint.GetPosition() - entity.GetPosition();
                _projectileFeature.SpawnProjectile(entity, direction, _feature.CurrentRight);
            }

            entity.Get<WeaponCooldown>().Value = entity.Read<WeaponCooldownDefault>().Value;
            if (entity.Get<WeaponAmmo>().Value < 1)
            {
                entity.Get<WeaponReloadTime>().Value = entity.Read<WeaponReloadTimeDefault>().Value;
                foreach (var projectile in _projectileFilter)
                {
                    if (projectile.Has<ProjectileIsLaser>())
                    {
                        projectile.Get<ProjectileSpeed>().Value *= -1;
                        projectile.Get<LifeTime>().Value = projectile.Read<LifeTimeDefault>().Value - projectile.Get<LifeTime>().Value;
                    }
                }
            }
        }
    }
}