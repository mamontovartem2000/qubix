using ME.ECS;
using Project.Common.Components;
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
            AddSystem<CheckGrenadePositionSystem>();
            AddSystem<GrenadeLifeTimeSystem>();
            AddSystem<LifeTimeSystem>();
            AddSystem<EMPLifeTimeSystem>();
            AddSystem<LinearLifeSystem>();
            AddSystem<LinearVisualLifeTimeSystem>();
            AddSystem<MeleeAimInitSystem>();
            AddSystem<SoftShieldRemoveSystem>();
            AddSystem<CritChanceLifeTimeSystem>();
            AddSystem<DestructibleHealthSystem>();
            AddSystem<AvoidTeleportLifeTimeSystem>();
            AddSystem<ShieldSkillLifetimeSystem>();
            AddSystem<SkillsUITimer>();
            AddSystem<FreezeLifeTimeSystem>();
        }

        protected override void OnDeconstruct() { }
    }
}