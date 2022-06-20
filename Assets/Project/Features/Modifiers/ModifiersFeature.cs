using ME.ECS;

namespace Project.Mechanics.Features {

    using Components; using Modules; using Systems; using Features; using Markers;
    using Modifiers.Components; using Modifiers.Modules; using Modifiers.Systems; using Modifiers.Markers;
    
    namespace Modifiers.Components {}
    namespace Modifiers.Modules {}
    namespace Modifiers.Systems {}
    namespace Modifiers.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class ModifiersFeature : Feature {

        protected override void OnConstruct()
        {
            AddSystem<SlownessModifier>();
            AddSystem<MovementSkillModifier>();

            AddSystem<LinearWeaponModifier>();
        }

        protected override void OnDeconstruct() {
            
        }

    }

}