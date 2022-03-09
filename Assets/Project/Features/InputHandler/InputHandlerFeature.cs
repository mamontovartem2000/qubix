using ME.ECS;

namespace Project.Features {
    #region usage

    

    using Components; using Modules; using Systems; using Features; using Markers;
    using InputHandler.Components;
    using InputHandler.Systems; using InputHandler.Markers;
    
    namespace InputHandler.Components {}
    namespace InputHandler.Modules {}
    namespace InputHandler.Systems {}
    namespace InputHandler.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class InputHandlerFeature : Feature {

        protected override void OnConstruct()
        {
            this.AddModule<HandlePlayerInput>();
            this.AddSystem<HandleInputSystem>();
        }

        protected override void OnDeconstruct() {
            
        }
    }
}