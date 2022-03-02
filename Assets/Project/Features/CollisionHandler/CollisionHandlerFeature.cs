using ME.ECS;

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
        public GlobalEvent DamageEvent;
        public GlobalEvent ScoreEvent;
        protected override void OnConstruct()
        {
            AddSystem<RegisterProjectileCollisionSystem>();
            AddSystem<RegisterPowerUpCollisionSystem>();
            AddSystem<RegisterTrapCollisionSystem>();
        }

        protected override void OnDeconstruct() {}
    }
}