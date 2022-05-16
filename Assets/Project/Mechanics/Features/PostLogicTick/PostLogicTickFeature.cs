using ME.ECS;

namespace Project.Mechanics.Features {

    using Components; using Modules; using Systems; using Features; using Markers;
    using PostLogicTick.Components; using PostLogicTick.Modules; using PostLogicTick.Systems; using PostLogicTick.Markers;
    
    namespace PostLogicTick.Components {}
    namespace PostLogicTick.Modules {}
    namespace PostLogicTick.Systems {}
    namespace PostLogicTick.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class PostLogicTickFeature : Feature {

        protected override void OnConstruct()
        {
            AddSystem<QuadTreeDispose>();
        }

        protected override void OnDeconstruct() {
            
        }

    }

}