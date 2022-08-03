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

            if (NetworkData.IsLocalGame)
            {
                foreach (var player in _playerFilter)
                {
                    world.GetFeature<EventsFeature>().Victory.Execute(player);
                }
                
                _finished = true;
                NetworkEvents.DestroyWorld?.Invoke();
                return;
            }
            
            switch (NetworkData.GameMode)
            {
                case GameModes.deathmatch:
                    SendDeathMatchResult();
                    break;
                case GameModes.teambattle:
                    SendTeambattleResult();
                    break;
                default:
                    Debug.Log("Unknown GameType!");
                    break;
            }
        }

        private void SendDeathMatchResult()
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

            var mostDamagePlayer = GetPlayersWithMostDamage(mostKillsPlayers);

            if (mostDamagePlayer.Count == 1)
            {
                Debug.Log("Most damage");
                return mostDamagePlayer[0];
            }

            var mostHealthPlayers = GetPlayersWithMostHealth(mostDamagePlayer);

            if (mostHealthPlayers.Count == 1)
            {
                Debug.Log("Most Health");
                return mostHealthPlayers[0];
            }

            Debug.Log("Random Winner");
            return GetRandomWinner(mostHealthPlayers);
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

        private void SendGameResult(Entity winner)
        {
            List<PlayerStats> stats = new List<PlayerStats>();

            foreach (var player in _playerFilter)
            {
                var kills = player.Read<PlayerScore>().Kills;
                var deaths = player.Read<PlayerScore>().Deaths;
                var id = player.Read<PlayerTag>().PlayerServerID;

                stats.Add(new PlayerStats() { Kills = (uint)kills, Deaths = (uint)deaths, PlayerId = id });
            }

            var winnerId = winner.Read<PlayerTag>().PlayerServerID;

            SystemMessages.SendEndGameStats(stats, winnerId);
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

        private List<Entity> GetPlayersWithMostDamage(List<Entity> playersList)
        {
            var maxDamage = int.MinValue;
            List<Entity> winners = new List<Entity>();

            foreach (var player in playersList)
            {
                var dealtDamage = player.Read<PlayerScore>().DealtDamage;

                if (dealtDamage > maxDamage)
                {
                    winners.Clear();
                    winners.Add(player);
                    maxDamage = (int)dealtDamage;
                }
                else if (dealtDamage == maxDamage)
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
                if (!player.Read<PlayerAvatar>().Value.IsAlive()) continue;
                var health = player.Read<PlayerAvatar>().Value.Read<PlayerHealth>().Value / player.Read<PlayerAvatar>().Value.Read<PlayerHealthDefault>().Value;

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
        public float Damage = 0;
        public float Health = 0;
    }
}