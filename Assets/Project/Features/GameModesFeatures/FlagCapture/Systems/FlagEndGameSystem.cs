using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Features.GameModesFeatures.FlagCapture.Systems
{
    #region usage
#pragma warning disable
    using Project.Components;
    using Project.Modules;
    using Project.Systems;
    using Project.Markers;
    using Components;
    using Modules;
    using Systems;
    using Markers;

#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class FlagEndGameSystem : ISystem, IAdvanceTick, IUpdate
    {
        private FlagCaptureFeature _feature;
        private Filter _playerFilter, _timerFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);
            
            Filter.Create("Player Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);
            
            Filter.Create("Timer Filter")
                .With<GameTimer>()
                .Push(ref _timerFilter);
        }

        void ISystemBase.OnDeconstruct() { }

        //TODO: Send results to server
        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (world.HasSharedData<WinningTeam>())
            {
                Debug.Log("winning team");
                var team = world.ReadSharedData<WinningTeam>().Team;
                DisplayGameResults(team);
                return;
            }
            
            if (!world.HasSharedData<EndOfGameStage>()) return;
            
            var stage = world.ReadSharedData<EndOfGameStage>().StageNumber;
            world.RemoveSharedData<EndOfGameStage>();
            
            if (stage == 1)
            {
                var winTeam = GetWinningTeamByFlagCount();

                if (winTeam == 0)
                {
                    world.SetSharedData(new MatchPoint());
                    var newTime = GameConsts.GameModes.FlagCapture.SECOND_GAME_PHASE_TIME;
                    world.SetSharedData(new GameStage { StageNumber = 2, Time = newTime});
                    UpdatePlayersBuffs();
                    UpdateTimer(newTime);
                }
                else
                {
                    Debug.Log("End first stage. Win by flag count");
                    DisplayGameResults(winTeam);
                }
                
                return;
            }

            if (stage == 2)
            {
                var score = world.ReadSharedData<CapturedFlagsScore>().Score;

                if (score[1] == 0 && score[2] == 0)
                {
                    ChoseWinningTeamByKills();
                }
                else
                {
                    DisplayGameResults(_feature.FirstCapturedFlag);
                }
            }
        }

        private void DisplayGameResults(int winnerTeam)
        {
            foreach (var player in _playerFilter)
            {
                if (player.Read<TeamTag>().Value == winnerTeam)
                    world.GetFeature<EventsFeature>().Victory.Execute(player);
                else
                    world.GetFeature<EventsFeature>().Defeat.Execute(player);
            }
            
            SetGameFinished();
        }
        
        private int GetWinningTeamByFlagCount()
        {
            ref var score = ref world.GetSharedData<CapturedFlagsScore>().Score;

            if (score[1] > score[2])
            {
                return 1;
            }
            else if (score[2] > score[1])
            {
                return 2;
            }

            return 0;
        }

        private void ChoseWinningTeamByKills()
        {
            var team1 = ResultUtils.GetTeamFullStats(_playerFilter, 1);
            var team2 = ResultUtils.GetTeamFullStats(_playerFilter, 2);

            if (team1.Kills > team2.Kills)
            {
                DisplayGameResults(1);
                Debug.Log("Team 1 win by kills");
            }
            else if (team1.Kills < team2.Kills)
            {
                DisplayGameResults(2);
                Debug.Log("Team 2 win by kills");
            }
            else
            {
                var rnd = world.GetRandomRange(1, 3);
                Debug.Log($"Team {rnd} win by random");
                DisplayGameResults(rnd);
            }
            //TODO: need refactoring for 3 and more teams
        }

        private void SetGameFinished()
        {
            world.GetFeature<EventsFeature>().OnGameFinished.Execute();
            world.SetSharedData(new GameFinished());
            world.SetSharedData(new GamePaused());
            NetworkEvents.DestroyWorld?.Invoke(); //TODO: Need add stats sending and remove this.
        }

        private void UpdateTimer(float time)
        {
            foreach (var timer in _timerFilter)
            {
                timer.Get<GameTimer>().Value = time;
                //TODO: Move to gamestate feature
            }
        }
        
        private void UpdatePlayersBuffs()
        {
            foreach (var player in _playerFilter)
            {
                if (player.Has<CarriesTheFlag>())
                    _feature.FlagBearerSecondStage.Apply(player);
                //TODO: Move to new system?
            }
        }
        
        void IUpdate.Update(in float deltaTime) { }
    }
}