using ME.ECS;
using Project.Features.PostLogicTick.Systems;
using Project.Features.PreLogicTick.Systems;

namespace Project.Features.PreLogicTick
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PreLogicTickFeature : Feature
    {
        protected override void OnConstruct()
        {
            AddSystem<PlayerMovementStartValue>();
            AddSystem<LinearStatsStartValue>();
        }

        protected override void OnDeconstruct() {}
    }
}