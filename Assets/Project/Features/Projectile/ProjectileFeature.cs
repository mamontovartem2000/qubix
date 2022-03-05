using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Features.Projectile.Views;
using UnityEngine;

namespace Project.Features {
    #region usage

    

    using Components; using Modules; using Systems; using Features; using Markers;
    using Projectile.Components; using Projectile.Modules; using Projectile.Systems; using Projectile.Markers;
    
    namespace Projectile.Components {}
    namespace Projectile.Modules {}
    namespace Projectile.Systems {}
    namespace Projectile.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class ProjectileFeature : Feature
    {
        public BulletMono BulletView;
        public RocketMono RocketView;

        public DataConfig BulletConfig;
        public DataConfig RocketConfig;
        
        private ViewId _bulletID, _rocketID;
        
        protected override void OnConstruct()
        {
            _bulletID = world.RegisterViewSource(BulletView);
            _rocketID = world.RegisterViewSource(RocketView);

            AddSystem<LeftWeaponFiringSystem>();
            AddSystem<RightWeaponFiringSystem>();

            AddSystem<LeftWeaponReloadSystem>();
            
            AddSystem<ProjectileMovementSystem>();
            AddSystem<ProjectileDeathSystem>();
            AddSystem<ProjectileLifeTimeSystem>();
        }

        protected override void OnDeconstruct() {}

        public ViewId GetBulletViewID()
        {
            return _bulletID;
        }

        public ViewId GetRocketViewID()
        {
            return _rocketID;
        }
        
        public void SpawnProjectile(Vector3 point, Vector3 direction, ViewId viewId, int id, DataConfig config)
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