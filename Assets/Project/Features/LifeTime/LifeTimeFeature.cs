using ME.ECS;
using Project.Features.LifeTime.Systems;
using Project.Features.LifeTime.Systems.SkillsSystems;

namespace Project.Features.LifeTime
{
    #region usage
    namespace LifeTime.Components { }
    namespace LifeTime.Modules { }
    namespace LifeTime.Systems { }
    namespace LifeTime.Markers { }

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class LifeTimeFeature : Feature
    {
        protected override void OnConstruct()
        {
            AddSystem<LifeTimeSystem>();
            AddSystem<EMPLifeTimeSystem>();
            AddSystem<LinearLifeSystem>();
            AddSystem<LinearVisualLifeTimeSystem>();
            AddSystem<MeleeAimInitSystem>();
            AddSystem<MeleeAimPositionSystem>();
            AddSystem<DestructibleHealthSystem>();
            AddSystem<AvoidTeleportLifeTimeSystem>();
            AddSystem<ShieldSkillLifetimeSystem>();
            AddSystem<SkillsUITimer>();
        }

        protected override void OnDeconstruct() { }
    }
}