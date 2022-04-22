using ME.ECS;
using Project.Core.Features.GameState.Systems;

namespace Project.Core.Features.GameState
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class GameStateFeature : Feature {

        protected override void OnConstruct()
        {
            AddSystem<TimerStartSystem>();
            AddSystem<GameRunSystem>();
	        AddSystem<GameFinishedSystem>();
            //AddSystem<LoadingJoinSceneSystem>();            
        }

        protected override void OnDeconstruct() { }
    }
}