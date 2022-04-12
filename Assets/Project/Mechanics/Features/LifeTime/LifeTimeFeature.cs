﻿using ME.ECS;

namespace Project.Mechanics.Features.Lifetime
{
    #region usage
    using Systems;
    namespace LifeTime.Components { }
    namespace LifeTime.Modules { }
    namespace LifeTime.Systems { }
    namespace LifeTime.Markers { }

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class LifeTimeFeature : Feature
    {
        protected override void OnConstruct()
        {
            AddSystem<LifeTimeSystem>();
            AddSystem<LinearVisualLifeTimeSystem>();
        }

        protected override void OnDeconstruct() { }
    }
}