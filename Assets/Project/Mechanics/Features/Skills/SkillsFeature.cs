using ME.ECS;
using Project.Mechanics.Features.Skills.Systems;

namespace Project.Mechanics.Features
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class SkillsFeature : Feature
    {
        protected override void OnConstruct()
        {
            AddSystem<SkillActivationSystem>();
        }

        protected override void OnDeconstruct() {}
    }
}