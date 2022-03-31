using ME.ECS;
using UnityEngine;

namespace Project.Mechanics.Features.Weapon.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Common.Components;
    using Projectile;
    using Projectile.Components;
    using Lifetime.Components;
    
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class LaserActivateSystem : ISystem, IAdvanceTick, IUpdate {
        
        private WeaponFeature _feature;
        private ProjectileFeature _projectileFeature;

        private Filter _activeLaser, _inactiveLaser, _projectileFilter;
        private bool _isLaserActive = false;
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {

            world.GetFeature(out _projectileFeature);
            this.GetFeature(out this._feature);
            Filter.Create("LaserShooting-Filter")
                .With<WeaponShot>()
                .Without<LaserDeactive>()
                .Without<WeaponReloadTime>()
                .Without<WeaponCooldown>()
                .Push(ref _activeLaser);
                
            Filter.Create("LaserStopShooting-Filter")
                .With<LaserDeactive>()
                .Push(ref _inactiveLaser);

            Filter.Create("Projectile_Filter")
                .With<ProjectileTag>()
                .Push(ref _projectileFilter);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        void IAdvanceTick.AdvanceTick(in float deltaTime) 
        {
            _isLaserActive = false;
            foreach(var entity in _activeLaser)
            {
                entity.Get<WeaponAmmo>().Value--;
                if (entity.Read<WeaponTag>().Hand == WeaponHand.Left)
                {
                   var  direction = _feature.LeftDestinationPoint.GetPosition() - entity.GetPosition();
                    _projectileFeature.SpawnProjectile(entity, direction, _feature.CurrentLeft);
                }

                entity.Get<WeaponCooldown>().Value = entity.Read<WeaponCooldownDefault>().Value;

                if (entity.Get<WeaponAmmo>().Value < 1)
                {
                    entity.Get<WeaponReloadTime>().Value = entity.Read<WeaponReloadTimeDefault>().Value;
                    CloseLaser();
                }

            }
            foreach(var projectile in _projectileFilter)
            {
                if (projectile.IsAlive())
                    _isLaserActive = true;
            }
            
            foreach(var entity in _inactiveLaser)
            {
                CloseLaser();
                if(_isLaserActive == false)
                    entity.Remove<LaserDeactive>();
            }
            
        }
        
        void IUpdate.Update(in float deltaTime) {}
        
        private void CloseLaser()
        {
            foreach (var projectile in _projectileFilter)
                {
                    if(projectile.Read<ProjectileSpeed>().Value > 0)
                    {
                        projectile.Get<ProjectileSpeed>().Value *= -1;
                        projectile.Get<LifeTime>().Value = projectile.Read<LifeTimeDefault>().Value - projectile.Get<LifeTime>().Value;                
                    }
                }
        }
    }
    
}