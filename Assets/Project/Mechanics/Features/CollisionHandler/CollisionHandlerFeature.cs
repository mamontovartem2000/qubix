using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.Views.Providers;
using Project.Core.Features.GameState.Components;
using Project.Mechanics.Features.CollisionHandler.Systems;
using Unity.Collections;
using UnityEngine;

namespace Project.Mechanics.Features.CollisionHandler 
{
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class CollisionHandlerFeature : Feature
    {
        protected override void OnConstruct()
        {
            AddSystem<GridCollisionDetectionSystem>();
        }
        
        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder.WithoutShared<GamePaused>();
        }

        protected override void OnDeconstruct() {}
    }
}