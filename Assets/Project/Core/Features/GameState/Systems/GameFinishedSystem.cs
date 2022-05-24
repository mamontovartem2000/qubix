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
                // .With<AvatarTag>()
                .Push(ref _playerFilter);
        }

        void ISystemBase.OnDeconstruct() { }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedData<GameFinished>() || _finished) return;

            var winner = GetWinnerEntity();

            foreach (var player in _playerFilter)
            {
                if (player == winner)
                    world.GetFeature<EventsFeature>().Victory.Execute(player);
                else
                    world.GetFeature<EventsFeature>().Defeat.Execute(player);
            }

            SendGameResult(winner);
            _finished = true;
        }

        private Entity GetWinnerEntity()
        {
            var mostKills = GetPlayersWithMostKills();

            if (mostKills.Count == 1)
            {
                Debug.Log("Most kills");
                return mostKills[0];
            }

            var fewestDeaths = GetPlayersWithFewestDeaths(mostKills);

            if (fewestDeaths.Count == 1)
            {
                Debug.Log("Fewest Deaths");
                return fewestDeaths[0];
            }

            var mostHealth = GetPlayersWithMostHealth(fewestDeaths);

            if (mostHealth.Count == 1)
            {
                Debug.Log("Most Health");
                return mostHealth[0];
            }

            Debug.Log("Random Winner");
            return GetRandomWinner(mostHealth);
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
}