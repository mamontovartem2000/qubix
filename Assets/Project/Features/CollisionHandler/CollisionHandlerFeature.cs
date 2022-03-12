using ME.ECS;
using Project.Features.CollisionHandler.Views;
using UnityEngine;

namespace Project.Features {
    #region usage

    

    using Components; using Modules; using Systems; using Features; using Markers;
    using CollisionHandler.Components; using CollisionHandler.Modules; using CollisionHandler.Systems; using CollisionHandler.Markers;
    
    namespace CollisionHandler.Components {}
    namespace CollisionHandler.Modules {}
    namespace CollisionHandler.Systems {}
    namespace CollisionHandler.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class CollisionHandlerFeature : Feature
    {
        public MineExlosionMono MineVFX;
        public DeathMono DeathVFX;
        public RocketExplosionMono RocketVFX;

        public ViewId _mineID, _deathID, _rocketId;
        public float _mineTimer, _deathTimer;
        
        protected override void OnConstruct()
        {
            AddSystem<ProjectileCollisionSystem>();
            AddSystem<HealthCollisionSystem>();
            AddSystem<MineCollisionSystem>();
            AddSystem<AmmoCollisionSystem>();
            AddSystem<ExplosionSystem>();

            _mineID = world.RegisterViewSource(MineVFX);
            _deathID = world.RegisterViewSource(DeathVFX);
            _rocketId = world.RegisterViewSource(RocketVFX);
        }
        
        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder.WithoutShared<GamePaused>();
        }

        public void SpawnVFX(Vector3 position, ViewId id, float time)
        {
            var vfx = new Entity("vfx");
            vfx.InstantiateView(id);
            vfx.SetPosition(position);
            vfx.Get<ExplosionTag>().Value = time;
        }
        
        protected override void OnDeconstruct() {}
    }
}