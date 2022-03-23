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
    public sealed class LeftWeaponFiringSystem : ISystemFilter 
    {
        public World world { get; set; }
        
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
            return Filter.Create("Filter-LeftWeaponSystem")
                .With<LeftWeaponShot>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var weapon = entity.Read<LeftWeapon>();
            var player = entity.Read<PlayerTag>();

            if(entity.Has<LeftWeaponCooldown>()) return;

            var actorID = player.PlayerID;
            //var direction = player.FaceDirection;
            var direction = Vector3.zero;


            if (direction == Vector3.forward)
            {
                _offset = new Vector3(-0.515f, 0.5f, 0f);
            }
            else if (direction == Vector3.left)
            {
                _offset = new Vector3(0f, 0.5f, -0.515f);
            }
            else if (direction == Vector3.back)
            {
                _offset = new Vector3(0.515f, 0.5f, 0);
            }
            else if (direction == Vector3.right)
            {
                _offset = new Vector3(0f, 0.5f, 0.515f);
            }

            var spawnPoint = entity.GetPosition() + _offset;
            var ammo = weapon.Type;
            var cooldown = weapon.Cooldown;

            switch (ammo)
            {
                case WeaponType.Gun:
                {

                    if (entity.Get<LeftWeapon>().Ammo > 0)
                    {
                        _feature.SpawnProjectile(spawnPoint, direction, _feature.GetBulletViewID(), actorID, _feature.GunConfig);
                        entity.Set(new LeftWeaponCooldown {Value = cooldown});
                        entity.Get<LeftWeapon>().Ammo -= 1;
                        world.GetFeature<EventsFeature>().leftWeaponFired.Execute(entity);
                    }
                }
                    break;
            }
        }
    }
}