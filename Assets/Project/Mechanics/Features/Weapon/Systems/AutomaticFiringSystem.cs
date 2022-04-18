using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Mechanics.Features.Projectile;
using UnityEngine;

namespace Project.Mechanics.Features.Weapon.Systems
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
        
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _projectile);
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
            var dir = entity.Read<WeaponAim>().Aim.GetPosition() - entity.GetPosition();
            
            if (entity.Has<SpreadAmount>())
            {
                var spread = entity.Read<SpreadAmount>().Value / 100f;
                dir += new Vector3(world.GetRandomRange(-spread, spread), 0,world.GetRandomRange(-spread, spread));
            }

            var cooldownBase = entity.Read<CooldownDefault>().Value;
            var cooldownMod = entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Read<FiringCooldownModifier>().Value * cooldownBase;
            var currentCooldown = cooldownBase + cooldownMod;
            
            if (ammo - 1 > 0)
            {
                entity.Get<Cooldown>().Value = currentCooldown;
            }
            else
            {
                entity.Get<ReloadTime>().Value = entity.Read<ReloadTimeDefault>().Value;
                world.GetFeature<EventsFeature>().RightWeaponDepleted.Execute(entity.Get<Owner>().Value);
            }
            
            ammo -= 1;
            _projectile.SpawnProjectile(entity, dir);
        }
    }
}