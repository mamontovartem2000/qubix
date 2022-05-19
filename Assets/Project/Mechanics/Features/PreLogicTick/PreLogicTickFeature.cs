using ME.ECS;

namespace Project.Mechanics.Features {

    using Components; using Modules; using Systems; using Features; using Markers;
    using PreLogicTick.Components; using PreLogicTick.Modules; using PreLogicTick.Systems; using PreLogicTick.Markers;
    
    namespace PreLogicTick.Components {}
    namespace PreLogicTick.Modules {}
    namespace PreLogicTick.Systems {}
    namespace PreLogicTick.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class PreLogicTickFeature : Feature {

        protected override void OnConstruct()
        {
            // AddSystem<QuadTreeInitSystem>();
        }

        protected override void OnDeconstruct() {
            
        }

    }

}