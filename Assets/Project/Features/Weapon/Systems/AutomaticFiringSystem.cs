using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.Projectile;
using Project.Features.VFX;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Features.Weapon.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class AutomaticFiringSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private WeaponFeature _feature;
        private ProjectileFeature _projectile;
        private VFXFeature _vfx;
        
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _projectile);
            world.GetFeature(out _vfx);
        }
        
        void ISystemBase.OnDeconstruct(){}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-NewAutomaticFiringSystem")
                .With<AutomaticWeapon>()
                .With<RightWeaponShot>()
                .Without<ReloadTime>()
                .Without<Cooldown>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var ammo = ref entity.Get<AmmoCapacity>().Value;
            
            var currentCooldown = entity.Read<CooldownDefault>().Value;
            
            if (ammo - 1 > 0)
            {
                entity.Get<Cooldown>().Value = currentCooldown;
            }
            else
            {
                entity.Set(new ModifiersCheck(), ComponentLifetime.NotifyAllSystems);

                entity.Get<ReloadTime>().Value = entity.Read<ReloadTimeDefault>().Value;
                world.GetFeature<EventsFeature>().RightWeaponDepleted.Execute(entity.Owner());
                world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity.Owner());
            }
            
            ammo--;
            
            Worlds.current.GetFeature<EventsFeature>().PlaySound.Execute(entity);

            entity.Set(new SpawnBullet(), ComponentLifetime.NotifyAllSystems);
        }
    }
}