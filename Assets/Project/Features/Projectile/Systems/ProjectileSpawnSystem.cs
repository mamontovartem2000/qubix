using ME.ECS;
using ME.ECS.DataConfigs;
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
    public sealed class ProjectileSpawnSystem : ISystemFilter 
    {
        private ProjectileFeature feature;
        private Vector3 _rightOffset, _leftOffset;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this.feature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-ProjectileSpawnSystem")
                .With<PlayerShot>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            // Debug.Log(entity.Read<PlayerTag>().PlayerID);

            var ammo = entity.Read<PlayerShot>().Ammo;
            var actorID = entity.Read<PlayerTag>().PlayerID;

            var spawnPoint = entity.Read<PlayerTag>().FaceDirection + entity.GetPosition() +
                             entity.Read<PlayerShot>().SpawnPoint;
            var direction = entity.Read<PlayerTag>().FaceDirection;

            if (direction == Vector3.forward)
            {
                _leftOffset = new Vector3(-0.515f, 0.5f, -1.5f);
                _rightOffset = new Vector3(0.515f, 0.5f, -1.5f);
            }
            else if (direction == Vector3.left)
            {
                _leftOffset = new Vector3(1.5f, 0.5f, -0.515f);
                _rightOffset = new Vector3(1.5f, 0.5f, 0.515f);
            }
            else if (direction == Vector3.back)
            {
                _leftOffset = new Vector3(0.515f, 0.5f, 1.5f);
                _rightOffset = new Vector3(-0.515f, 0.5f, 1.5f);
            }
            else if (direction == Vector3.right)
            {
                _leftOffset = new Vector3(-1.5f, 0.5f, 0.515f);
                _rightOffset = new Vector3(-1.5f, 0.5f, -0.515f);
            }

            switch (ammo)
            {
                case AmmoType.Bullet:
                    SpawnProjectile(spawnPoint + _leftOffset, direction, feature.GetBulletViewID(), actorID,
                        feature.BulletConfig);
                    break;
                case AmmoType.Rocket:
                    SpawnProjectile(spawnPoint + _rightOffset, direction, feature.GetRocketViewID(), actorID,
                        feature.RocketConfig);
                    break;
            }
        }

        private void SpawnProjectile(Vector3 point, Vector3 direction, ViewId viewId, int id, DataConfig config)
        {
            var entity = new Entity("projectile");
            
            entity.InstantiateView(viewId);
            
            entity.SetPosition(point);
            entity.SetRotation(Quaternion.Euler(direction));

            entity.Set(new ProjectileTag {ActorID = id});
            entity.Set(new ProjectileDirection {Value = direction});

            entity.SetTimer(0, 5f);
            
            config.Apply(entity);
        }
    }
}