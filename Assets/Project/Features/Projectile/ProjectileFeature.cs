using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Features.Projectile.Views;

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
            this.AddSystem<ProjectileSpawnSystem>();
            this.AddSystem<ProjectileMovementSystem>();
            this.AddSystem<ProjectileDeathSystem>();

            _bulletID = world.RegisterViewSource(BulletView);
            _rocketID = world.RegisterViewSource(RocketView);
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
    }
}