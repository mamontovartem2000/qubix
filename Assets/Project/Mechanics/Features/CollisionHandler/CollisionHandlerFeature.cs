using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Core.Features.GameState.Components;
using Project.Mechanics.Features.CollisionHandler.Components;
using Project.Mechanics.Features.CollisionHandler.Systems;
using UnityEngine;

namespace Project.Mechanics.Features.CollisionHandler 
{
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class CollisionHandlerFeature : Feature
    {
        public MonoBehaviourViewBase ExplosionVFX;
        public MonoBehaviourViewBase PlayerDeathVFX;
        public MonoBehaviourViewBase HealVFX;
        public MonoBehaviourViewBase TakeDamageVFX;

        public ViewId ExplosionID, PlayerDeathID, HealID, TakeDamageID;
        public float DefaultTimer;
        
        protected override void OnConstruct()
        {
            AddSystem<HealthCollisionSystem>();
            AddSystem<MineCollisionSystem>();
            AddSystem<ExplosionSystem>();
            AddSystem<ProjectileCollisionSystem>();

            ExplosionID = world.RegisterViewSource(ExplosionVFX);
            PlayerDeathID = world.RegisterViewSource(PlayerDeathVFX);
            HealID = world.RegisterViewSource(HealVFX);
            TakeDamageID = world.RegisterViewSource(TakeDamageVFX);
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