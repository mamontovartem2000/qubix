using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
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
            Team team1 = new Team();
            Team team2 = new Team();

            GetTeamFullStats(team1, 1);
            GetTeamFullStats(team2, 2);

            if (team1.Kills > team2.Kills)
                return 1;
            else if (team1.Kills < team2.Kills)
                return 2;
            
            if (team1.Damage > team2.Damage)
                return 1;
            else if (team1.Damage < team2.Damage)
                return 2;

            if (team1.Deaths < team2.Deaths)
                return 1;
            else if (team1.Deaths > team2.Deaths)
                return 2;

            //TODO: Add random
            return 2;
        }

        private void GetTeamFullStats(Team team, int teamNumber)
        {
            foreach (var player in _playerFilter)
            {
                if (player.Read<TeamTag>().Value != teamNumber) continue;

                team.Kills += player.Read<PlayerScore>().Kills;
                team.Deaths += player.Read<PlayerScore>().Deaths;
                team.Damage += player.Read<PlayerScore>().DealtDamage;
                
                if (player.Read<PlayerAvatar>().Value.IsAlive())
                    team.Health += player.Read<PlayerAvatar>().Value.Read<PlayerHealth>().Value;
            }
        }
        private void SendTeamGameResult(int winnerTeam)
        {
            List<PlayerStats> stats = new List<PlayerStats>();

            foreach (var player in _playerFilter)
            {
                var kills = player.Read<PlayerScore>().Kills;
                var deaths = player.Read<PlayerScore>().Deaths;
                var id = player.Read<PlayerTag>().PlayerServerID;
                var team = player.Read<TeamTag>().Value;


                stats.Add(new PlayerStats() { Kills = (uint)kills, Deaths = (uint)deaths, PlayerId = id, Team = team });
            }

            SystemMessages.SendTeamGameStats(stats, winnerTeam.ToString()); //TODO: need int
        }

        void IUpdate.Update(in float deltaTime) { }
    }

    public class Team
    {
        public int Kills = 0;
        public int Deaths = 0;
        public float Damage = 0;
        public float Health = 0;
    }
}