using FlatBuffers;
using FlatMessages;
using ME.ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Modules.Network
{
    public static class SystemMessages
    {
        public static Action DestroyWorld;

        public static byte[] SystemHashMessage(uint tick, int hash)
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1);
            var hashMess = SaveHash.CreateSaveHash(builder, tick, hash);
            var offset = SystemMessage.CreateSystemMessage(builder, GetTime(), Payload.SaveHash, hashMess.Value);
            builder.Finish(offset.Value);

            return builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
        }

        public static void ProcessSystemMessage(byte[] bytes)
        {
            SystemMessage data = SystemMessage.GetRootAsSystemMessage(new ByteBuffer(bytes));

            switch (data.PayloadType)
            {
                case Payload.NONE:
                    Debug.Log("Payload type NONE!");
                    break;
                case Payload.ReplayFrom:
                    GetReplayHash(data.PayloadAsReplayFrom());
                    break;
                case Payload.TimeFromStart:
                    SetServerTime(data.PayloadAsTimeFromStart());
                    break;
                case Payload.StatsReceive:
#if UNITY_WEBGL && !UNITY_EDITOR
                    BrowserEvents.GameIsOver();
#endif
                    DestroyWorld?.Invoke();
                    break;
                default:
                    Debug.Log("Unknown system message!");
                    break;
            }
        }

        public static void SendEndGameStats(List<PlayerStats> stats, string winnerID)
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1);
            var winner = builder.CreateString(winnerID);
            Offset<Stats>[] offsets = new Offset<Stats>[stats.Count];

            for (int i = 0; i < stats.Count; i++)
            {
                PlayerStats player = stats[i];
                var playerId = builder.CreateString(player.PlayerId);
                var playerStats = Stats.CreateStats(builder, player.Kills, player.Deaths, playerId);
                offsets[i] = playerStats;
            }

            var statsArray = GameOver.CreateStatsVector(builder, offsets);
            var hash = Worlds.currentWorld.GetModule<NetworkModule>().GetSyncHash();
            var gameOver = GameOver.CreateGameOver(builder, winner, hash, statsArray);

            var offset = SystemMessage.CreateSystemMessage(builder, GetTime(), Payload.GameOver, gameOver.Value);
            builder.Finish(offset.Value);

            var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(ms);
        }

        public static void SendTeamGameStats(List<PlayerStats> stats, string winnerTeam)
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1);
            var winner = builder.CreateString(winnerTeam);
            Offset<TeamStats>[] offsets = new Offset<TeamStats>[stats.Count];

            for (int i = 0; i < stats.Count; i++)
            {
                PlayerStats player = stats[i];
                var playerId = builder.CreateString(player.PlayerId);
                var playerTeam = builder.CreateString(player.Team);
                var playerStats = TeamStats.CreateTeamStats(builder, player.Kills, player.Deaths, playerId, playerTeam);
                offsets[i] = playerStats;
            }

            var statsArray = TeamGameOver.CreateStatsVector(builder, offsets);
            var hash = Worlds.currentWorld.GetModule<NetworkModule>().GetSyncHash();
            var gameOver = TeamGameOver.CreateTeamGameOver(builder, winner, hash, statsArray);

            var offset = SystemMessage.CreateSystemMessage(builder, GetTime(), Payload.TeamGameOver, gameOver.Value);
            builder.Finish(offset.Value);

            var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(ms);
        }

        private static void SetServerTime(TimeFromStart timeFromStart)
        {
            var world = Worlds.currentWorld;
            var serverTime = timeFromStart.Time / 1000;
            world.SetTimeSinceStart(serverTime);
        }

        private static void GetReplayHash(ReplayFrom replayFrom)
        {
            uint ticks = replayFrom.LastTick;
            Worlds.currentWorld.RewindTo(ticks);
        }

        public static uint GetTime()
        {
            return (uint) new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }

    public struct PlayerStats
    {
        public uint Kills;
        public uint Deaths;
        public string PlayerId;
        public string Team;
    }

    public struct PlayerInfo
    {
        public string Id;
        public int Slot;
        public string Nickname;
        public string Character;
        public string Icon;
    }

    public struct RoomInfo
    {
        public string Id;
        public uint PlayersCount;
        public uint MaxPlayersCount;
    }
}
