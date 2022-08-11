using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.GameState.Systems;
using Project.Modules.Network;
using UnityEngine;

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

            InitGameMode();
        }

        private void InitGameMode()
        {
            switch (NetworkData.GameMode)
            {
                case GameModes.deathmatch:
                    world.SetSharedData(new DeathmatchMode(), ComponentLifetime.NotifyAllSystems);
                    world.SetSharedData(new GameWithoutStages { Time = GameConsts.Main.DEATHMATCH_TIMER });
                    break;
                
                case GameModes.teambattle:
                    world.SetSharedData(new TeamDeathmatchMode(), ComponentLifetime.NotifyAllSystems);
                    world.SetSharedData(new GameWithoutStages { Time = GameConsts.Main.TEAM_DEATHMATCH_TIMER});
                    break;
                
                case GameModes.flagCapture:
                    world.SetSharedData(new FlagCaptureMode(), ComponentLifetime.NotifyAllSystems);
                    world.SetSharedData(new GameStage { StageNumber = 1, Time = GameConsts.GameModes.FlagCapture.FIRST_GAME_PHASE_TIME});
                    break;
                
                default:
                    Debug.Log("Unknown game mode");
                    break;
            }
        }

        protected override void OnDeconstruct() { }
    }
}