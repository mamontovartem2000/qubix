using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Core.Features.GameState.Components;
using Project.Mechanics.Features.Projectile.Components;
using Project.Mechanics.Features.Projectile.Systems;
using Project.Mechanics.Features.Projectile.Views;
using UnityEngine;

namespace Project.Mechanics.Features.Projectile {
    #region usage
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
        public SniperBulletMono SniperBulletView;

        public DataConfig GunConfig;
        public DataConfig RocketConfig;
        public DataConfig RifleConfig;

        private ViewId _bulletID, _rocketID, _sniperBulletID;
        
        protected override void OnConstruct()
        {
            _bulletID = world.RegisterViewSource(BulletView);
            _rocketID = world.RegisterViewSource(RocketView);
            _sniperBulletID = world.RegisterViewSource(SniperBulletView);

            AddSystem<LeftWeaponFiringSystem>();
            AddSystem<RightWeaponFiringSystem>();

            AddSystem<LeftWeaponReloadSystem>();
            
            AddSystem<ProjectileMovementSystem>();
            AddSystem<ProjectileDeathSystem>();
            AddSystem<ProjectileLifeTimeSystem>();
        }

        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder.WithoutShared<GamePaused>();
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

        public ViewId GetSniperBulletID()
        {
            return _sniperBulletID;
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