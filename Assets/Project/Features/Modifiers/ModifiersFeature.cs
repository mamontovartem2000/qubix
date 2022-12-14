using ME.ECS;
using Project.Features.Modifiers.Systems;

namespace Project.Features.Modifiers {
   
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
            AddSystem<DashSkillModifier>();

            AddSystem<HealingBuffSystem>();
            AddSystem<SlowdownBuffSystem>();
            
            AddSystem<LinearWeaponModifier>();
        }

        protected override void OnDeconstruct() { }
    }
}