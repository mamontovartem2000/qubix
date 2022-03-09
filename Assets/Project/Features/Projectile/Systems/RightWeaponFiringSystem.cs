using ME.ECS;
using Project.Features.Player.Components;
using UnityEngine;

namespace Project.Features.Projectile.Systems 
{
    #region usage

    

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class RightWeaponFiringSystem : ISystemFilter 
    {
        public World world { set; get; }
        
        private ProjectileFeature _feature;
        private Vector3 _offset;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-RightWeaponFiringSystem")
                .With<RightWeaponShot>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var weapon = entity.Read<RightWeapon>();
            var player = entity.Read<PlayerTag>();

            if (entity.Has<RightWeaponCooldown>()) return;

            var actorID = player.PlayerID;
            var direction = player.FaceDirection;

            if (direction == Vector3.forward)
            {
                _offset = new Vector3(0.515f, 0.5f, 0f);
            }
            else if (direction == Vector3.left)
            {
                _offset = new Vector3(0f, 0.5f, 0.515f);
            }
            else if (direction == Vector3.back)
            {
                _offset = new Vector3(-0.515f, 0.5f, 0f);
            }
            else if (direction == Vector3.right)
            {
                _offset = new Vector3(0f, 0.5f, -0.515f);
            }

            var spawnPoint = entity.GetPosition() + _offset;
            var weaponType = weapon.Type;

            switch (weaponType)
            {
                case WeaponType.Rocket:
                {
                    if (entity.Get<RightWeapon>().Count > 0)
                    {
                        _feature.SpawnProjectile(spawnPoint, direction, _feature.GetRocketViewID(), actorID, _feature.RocketConfig);
                        entity.Set(new RightWeaponCooldown {Value = entity.Read<RightWeapon>().Cooldown});

                        entity.Get<RightWeapon>().Count -= 1;
                        world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity);
                    }
                }
                    break;

                case WeaponType.Rifle :
                {
                    if (entity.Get<RightWeapon>().Count > 0)
                    {
                        _feature.SpawnProjectile(spawnPoint, direction, _feature.GetBulletViewID(), actorID, _feature.RifleConfig);
                        entity.Set(new RightWeaponCooldown {Value = entity.Read<RightWeapon>().Cooldown});

                        entity.Get<RightWeapon>().Count -= 1;
                        world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity);
                    }
                }               
                    break;
            }
        }
    }
}