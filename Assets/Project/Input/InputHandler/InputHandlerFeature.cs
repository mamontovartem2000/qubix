using ME.ECS;
using Project.Input.InputHandler.Modules;

namespace Project.Input.InputHandler 
{
    #region usage
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
    public sealed class InputHandlerFeature : Feature 
    {
        protected override void OnConstruct()
        {
	        AddModule<HandlePlayerInput>();
        }
        protected override void OnDeconstruct() {}
    }
}