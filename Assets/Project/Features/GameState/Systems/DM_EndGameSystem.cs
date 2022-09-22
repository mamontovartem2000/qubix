using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.Player;
using Project.Features.Player.Systems;
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

    public sealed class DM_EndGameSystem : ISystem, IAdvanceTick, IUpdate
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

            if (NetworkData.IsLocalGame)
            {
                foreach (var player in _playerFilter)
                {
                    world.GetFeature<EventsFeature>().Victory.Execute(player);
                }
                
                NetworkEvents.DestroyWorld?.Invoke();
                return;
            }
            
            SendDeathMatchResult();
            world.SetSharedData(new ResultSent());
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
            //SendGameResult(winner.Read<PlayerTag>().PlayerServerID);
            SendExtendedGameResult(winner.Read<PlayerTag>().PlayerServerID);
        }

        private Entity GetWinnerEntity()
        {
            var mostKillsPlayers = ResultUtils.GetPlayersWithMostKills(_playerFilter);

            if (mostKillsPlayers.Count == 1)
            {
                Debug.Log("Most kills");
                return mostKillsPlayers[0];
            }

            var mostDamagePlayer = ResultUtils.GetPlayersWithMostDamage(mostKillsPlayers);

            if (mostDamagePlayer.Count == 1)
            {
                Debug.Log("Most damage");
                return mostDamagePlayer[0];
            }

            var mostHealthPlayers = ResultUtils.GetPlayersWithMostHealth(mostDamagePlayer);

            if (mostHealthPlayers.Count == 1)
            {
                Debug.Log("Most Health");
                return mostHealthPlayers[0];
            }

            Debug.Log("Random Winner");
            return GetRandomWinner(mostHealthPlayers);
        }
        
        private void SendGameResult(string winnerId)
        {
            var stats = new List<PlayerStats>();

            foreach (var player in _playerFilter)
            {
                var kills = player.Read<PlayerScore>().Kills;
                var deaths = player.Read<PlayerScore>().Deaths;
                var id = player.Read<PlayerTag>().PlayerServerID;

                stats.Add(new PlayerStats() { Kills = (uint)kills, Deaths = (uint)deaths, PlayerId = id });
            }

            GameStatsMessages.SendEndGameStats(stats, winnerId);
        }
        
        private void SendExtendedGameResult(string winnerId)
        {
            var localPlayerStats = new ExtendedPlayerStats();
            
            foreach (var player in _playerFilter)
            {
                if (player == Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom))
                {
                    localPlayerStats = new ExtendedPlayerStats
                    {
                        Kills = (ushort)player.Read<PlayerScore>().Kills,
                        Deaths = (ushort)player.Read<PlayerScore>().Deaths,
                        PlayerId = player.Read<PlayerTag>().PlayerServerID,
                        Damage = (ushort)Mathf.Round(player.Read<PlayerScore>().DealtDamage),
                        AvgLifetime = (ushort)LifetimeStats.Stats.RoundedAverageLifetime()
                    };

                    //Debug.Log($"PlayerId: {localPlayerStats.PlayerId}, Deaths: {localPlayerStats.Deaths}, Kills: {localPlayerStats.Kills}," +
                    //          $" Damage: {localPlayerStats.Damage}, AvgLifetime: {localPlayerStats.AvgLifetime}, Character: {NetworkData.LocalCharacter}, winner: {winnerId}");
                    break;
                }
            }
            
            GameStatsMessages.SendExtendedEndGameStats(localPlayerStats, winnerId);
        }

        private Entity GetRandomWinner(List<Entity> playersList)
        {
            var number = world.GetRandomRange(0, playersList.Count);
            return playersList[number];
        }

        void IUpdate.Update(in float deltaTime) { }
    }
}