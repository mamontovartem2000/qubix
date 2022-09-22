using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Features.GameState.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class TeamDM_EndGameSystem : ISystem, IAdvanceTick, IUpdate
    {
        public World world { get; set; }

        private GameStateFeature _feature;
        private Filter _playerFilter;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            Filter.Create("Player Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);
        }

        void ISystemBase.OnDeconstruct() { }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (world.HasSharedData<ResultSent>()) return;
            if (!world.HasSharedData<GameFinished>()) return;

            SendTeambattleResult();
            world.SetSharedData(new ResultSent());
        }

        private void SendTeambattleResult()
        {
            var winnerTeam = GetWinnerTeam();

            foreach (var player in _playerFilter)
            {
                if (player.Read<TeamTag>().Value == winnerTeam)
                    world.GetFeature<EventsFeature>().Victory.Execute(player);
                else
                    world.GetFeature<EventsFeature>().Defeat.Execute(player);
            }

            Debug.Log("SendTeamGameResult");
            SendTeamGameResult(winnerTeam);
        }

        private int GetWinnerTeam()
        {
            var team1 = ResultUtils.GetTeamFullStats(_playerFilter, 1);
            var team2 = ResultUtils.GetTeamFullStats(_playerFilter, 2);

            if (team1.Kills > team2.Kills)
                return 1;
            
            if (team1.Kills < team2.Kills)
                return 2;
            
            if (team1.Damage > team2.Damage)
                return 1;
            
            if (team1.Damage < team2.Damage)
                return 2;

            if (team1.Deaths < team2.Deaths)
                return 1;
            
            if (team1.Deaths > team2.Deaths)
                return 2;

            //TODO: Add random
            return 2;
        }

        private void SendTeamGameResult(int winnerTeam)
        {
            var stats = new List<PlayerStats>();

            foreach (var player in _playerFilter)
            {
                var kills = player.Read<PlayerScore>().Kills;
                var deaths = player.Read<PlayerScore>().Deaths;
                var id = player.Read<PlayerTag>().PlayerServerID;
                var team = player.Read<TeamTag>().Value;

                stats.Add(new PlayerStats() { Kills = (uint)kills, Deaths = (uint)deaths, PlayerId = id, Team = team });
            }

            GameStatsMessages.SendTeamGameStats(stats, winnerTeam.ToString()); //TODO: need int
        }

        void IUpdate.Update(in float deltaTime) { }
    }
}