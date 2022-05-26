using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using System.Collections.Generic;
using UnityEngine;
using Project.Modules.Network;

namespace Project.Core.Features.GameState.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class GameFinishedSystem : ISystem, IAdvanceTick, IUpdate
    {
        public World world { get; set; }

        private GameStateFeature _feature;
        private Filter _playerFilter;
        private bool _finished;

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
            if (!world.HasSharedData<GameFinished>() || _finished) return;

            //TODO: use game type in join request
            if (NetworkData.Team == string.Empty)
            {
                DeathMatchGame();
            }
            else
            {
                TeamGame();
            }
        }

        private void DeathMatchGame()
        {
            var winner = GetWinnerEntity();

            foreach (var player in _playerFilter)
            {
                if (player == winner)
                    world.GetFeature<EventsFeature>().Victory.Execute(player);
                else
                    world.GetFeature<EventsFeature>().Defeat.Execute(player);
            }

            Debug.Log("SendGameResult");
            SendGameResult(winner);
            _finished = true;
        }

        private void TeamGame()
        {
            var winnerTeam = GetWinnerTeam();

            foreach (var player in _playerFilter)
            {
                if (player.Read<PlayerTag>().Team == winnerTeam)
                    world.GetFeature<EventsFeature>().Victory.Execute(player);
                else
                    world.GetFeature<EventsFeature>().Defeat.Execute(player);
            }

            Debug.Log("SendTeamGameResult");
            SendTeamGameResult(winnerTeam);
            _finished = true;
        }

        private Entity GetWinnerEntity()
        {
            var mostKillsPlayers = GetPlayersWithMostKills();

            if (mostKillsPlayers.Count == 1)
            {
                Debug.Log("Most kills");
                return mostKillsPlayers[0];
            }

            var fewestDeathsPlayers = GetPlayersWithFewestDeaths(mostKillsPlayers);

            if (fewestDeathsPlayers.Count == 1)
            {
                Debug.Log("Fewest Deaths");
                return fewestDeathsPlayers[0];
            }

            var mostHealthPlayers = GetPlayersWithMostHealth(fewestDeathsPlayers);

            if (mostHealthPlayers.Count == 1)
            {
                Debug.Log("Most Health");
                return mostHealthPlayers[0];
            }

            Debug.Log("Random Winner");
            return GetRandomWinner(mostHealthPlayers);
        }

        private string GetWinnerTeam()
        {
            Team team1 = new Team();
            Team team2 = new Team();

            GetTeamFullStats(team1, "blue");
            GetTeamFullStats(team2, "red");

            if (team1.Kills > team2.Kills)
                return "blue";
            else if (team1.Kills < team2.Kills)
                return "red";

            if (team1.Deaths < team2.Deaths)
                return "blue";
            else if (team1.Deaths > team2.Deaths)
                return "red";

            if (team1.Health > team2.Health)
                return "blue";
            else if (team1.Health < team2.Health)
                return "red";

            //TODO: Add random
            return "blue";
        }

        private void GetTeamFullStats(Team team, string teamColor)
        {
            foreach (var player in _playerFilter)
            {
                if (player.Read<PlayerTag>().Team != teamColor) continue;

                team.Kills += player.Read<PlayerScore>().Kills;
                team.Deaths += player.Read<PlayerScore>().Kills;
                team.Health += player.Read<PlayerAvatar>().Value.Read<PlayerHealth>().Value;
            }
        }

        private void SendGameResult(Entity winner)
        {
            List<PlayerStats> stats = new List<PlayerStats>();

            foreach (var player in _playerFilter)
            {
                var kills = player.Read<PlayerScore>().Kills;
                var deaths = player.Read<PlayerScore>().Kills;
                var id = player.Read<PlayerTag>().PlayerServerID;

                stats.Add(new PlayerStats() { Kills = (uint)kills, Deaths = (uint)deaths, PlayerId = id });
            }

            var winnerId = winner.Read<PlayerTag>().PlayerServerID;

            SystemMessages.SendEndGameStats(stats, winnerId);
        }

        private void SendTeamGameResult(string winnerTeam)
        {
            List<PlayerStats> stats = new List<PlayerStats>();

            foreach (var player in _playerFilter)
            {
                var kills = player.Read<PlayerScore>().Kills;
                var deaths = player.Read<PlayerScore>().Kills;
                var id = player.Read<PlayerTag>().PlayerServerID;
                var team = player.Read<PlayerTag>().Team;


                stats.Add(new PlayerStats() { Kills = (uint)kills, Deaths = (uint)deaths, PlayerId = id, Team = team });
            }

            SystemMessages.SendTeamGameStats(stats, winnerTeam);
        }

        private List<Entity> GetPlayersWithMostKills()
        {
            var maxKills = int.MinValue;
            List<Entity> winners = new List<Entity>();

            foreach (var player in _playerFilter)
            {
                var kills = player.Read<PlayerScore>().Kills;

                if (kills > maxKills)
                {
                    winners.Clear();
                    winners.Add(player);
                    maxKills = kills;
                }
                else if (kills == maxKills)
                {
                    winners.Add(player);
                }
            }

            return winners;
        }

        private List<Entity> GetPlayersWithFewestDeaths(List<Entity> playersList)
        {
            var minDeaths = int.MaxValue;
            List<Entity> winners = new List<Entity>();

            foreach (var player in playersList)
            {
                var deaths = player.Read<PlayerScore>().Deaths;

                if (deaths < minDeaths)
                {
                    winners.Clear();
                    winners.Add(player);
                    minDeaths = deaths;
                }
                else if (deaths == minDeaths)
                {
                    winners.Add(player);
                }
            }

            return winners;
        }

        private List<Entity> GetPlayersWithMostHealth(List<Entity> playersList)
        {
            var maxHealth = float.MinValue;
            List<Entity> winners = new List<Entity>();

            foreach (var player in playersList)
            {
                if (!player.IsAlive()) continue;
                var health = player.Read<PlayerAvatar>().Value.Read<PlayerHealth>().Value;

                if (health > maxHealth)
                {
                    winners.Clear();
                    winners.Add(player);
                    maxHealth = health;
                }
                else if (health == maxHealth)
                {
                    winners.Add(player);
                }
            }

            return winners;
        }

        private Entity GetRandomWinner(List<Entity> playersList)
        {
            var number = world.GetRandomRange(0, playersList.Count);
            return playersList[number];
        }

        void IUpdate.Update(in float deltaTime) { }
    }

    public class Team
    {
        public int Kills = 0;
        public int Deaths = 0;
        public float Health = 0;
    }
}