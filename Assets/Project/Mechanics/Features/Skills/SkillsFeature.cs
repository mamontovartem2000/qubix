using ME.ECS;

namespace Project.Mechanics.Features {

    using Components; using Modules; using Systems; using Features; using Markers;
    using Skills.Components; using Skills.Modules; using Skills.Systems; using Skills.Markers;
    
    namespace Skills.Components {}
    namespace Skills.Modules {}
    namespace Skills.Systems {}
    namespace Skills.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SkillsFeature : Feature {

        protected override void OnConstruct() {
            
        }

        protected override void OnDeconstruct() {
            
        }

    }

}