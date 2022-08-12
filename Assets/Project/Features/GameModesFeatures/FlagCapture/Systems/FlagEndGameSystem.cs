using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;

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

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (world.HasSharedData<WinningTeam>())
            {
                var team = world.ReadSharedData<WinningTeam>().Team;
                DisplayGameResults(team);
                return;
            }
            
            if (!world.HasSharedData<EndOfGameStage>()) return;
            
            var stage = world.ReadSharedData<EndOfGameStage>().StageNumber;
            
            if (stage == 1)
            {
                var winTeam = GetWinningTeamByFlagCount();

                if (winTeam == 0)
                {
                    world.RemoveSharedData<EndOfGameStage>();
                    world.SetSharedData(new MatchPoint());
                    world.SetSharedData(new GameStage { StageNumber = 2, Time = GameConsts.GameModes.FlagCapture.SECOND_GAME_PHASE_TIME});
                }
                else
                {
                    DisplayGameResults(winTeam);
                }
                
                return;
            }

            if (stage == 2)
            {
                //TODO: compare team kills
                //TODO: random winner if kills equals
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

        private void SetGameFinished()
        {
            world.GetFeature<EventsFeature>().OnGameFinished.Execute();
            world.SetSharedData(new GameFinished());
            world.SetSharedData(new GamePaused());
        }
        
        void IUpdate.Update(in float deltaTime) { }

    }
}