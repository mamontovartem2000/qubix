using ME.ECS;
using Project.Features.GameState.Systems;

namespace Project.Features.GameState
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
            AddSystem<GameRunSystem>();
	        AddSystem<GameFinishedSystem>();
            AddSystem<TimerStartSystem>();
            //AddSystem<RunTimerSystem>();
        }

        protected override void OnDeconstruct() { }
    }
}