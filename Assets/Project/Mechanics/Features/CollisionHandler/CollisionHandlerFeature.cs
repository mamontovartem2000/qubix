using ME.ECS;
using Project.Core.Features.GameState.Components;
using Project.Mechanics.Features.CollisionHandler.Components;
using Project.Mechanics.Features.CollisionHandler.Systems;
using Project.Mechanics.Features.CollisionHandler.Views;
using UnityEngine;

namespace Project.Mechanics.Features.CollisionHandler {
    #region usage
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
            AddSystem<HealthCollisionSystem>();
            AddSystem<MineCollisionSystem>();
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