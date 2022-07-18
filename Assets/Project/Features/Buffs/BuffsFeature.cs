using ME.ECS;

namespace Project.Features
{
    #region usage
    
    using Components;
    using Modules;
    using Systems;
    using Features;
    using Markers;
    using Buffs.Components;
    using Buffs.Modules;
    using Buffs.Systems;
    using Buffs.Markers;

    namespace Buffs.Components
    {
    }

    namespace Buffs.Modules
    {
    }

    namespace Buffs.Systems
    {
    }

    namespace Buffs.Markers
    {
    }

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    
    public sealed class BuffsFeature : Feature
    {
        protected override void OnConstruct()
        {
            AddSystem<HealingBuffSystem>();
        }

        protected override void OnDeconstruct()
        {
        }
    }
}