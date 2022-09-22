using System.Collections.Generic;
using FlatBuffers;
using FlatMessages;
using ME.ECS;

namespace Project.Modules.Network
{
    public static class GameStatsMessages
    {
        public static void SendExtendedEndGameStats(ExtendedPlayerStats player, string winner)
        {
            var builder = new FlatBufferBuilder(1);
            var winnerId = builder.CreateString(winner);
            var playerId = builder.CreateString(player.PlayerId);
            var character = builder.CreateString(NetworkData.LocalCharacter);
            var playerStats = NewStats.CreateNewStats(builder, character, player.Kills, player.Damage, 
                player.Deaths, playerId, player.AvgLifetime, winnerId);
        
            var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.NewStats, playerStats.Value);
            builder.Finish(offset.Value);

            var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(ms);
        }
    
        public static void SendEndGameStats(List<PlayerStats> stats, string winnerId)
        {
            var builder = new FlatBufferBuilder(1);
            var winner = builder.CreateString(winnerId);
            var offsets = new Offset<Stats>[stats.Count];

            for (var i = 0; i < stats.Count; i++)
            {
                var player = stats[i];
                var playerId = builder.CreateString(player.PlayerId);
                var playerStats = Stats.CreateStats(builder, player.Kills, player.Deaths, playerId);
                offsets[i] = playerStats;
            }

            var statsArray = GameOver.CreateStatsVector(builder, offsets);
            var hash = Worlds.currentWorld.GetModule<NetworkModule>().GetSyncHash();
            var gameOver = GameOver.CreateGameOver(builder, winner, hash, statsArray);

            var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.GameOver, gameOver.Value);
            builder.Finish(offset.Value);

            var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(ms);
        }
    
        public static void SendTeamGameStats(List<PlayerStats> stats, string winnerTeam)
        {
            var builder = new FlatBufferBuilder(1);
            var winner = builder.CreateString(winnerTeam);
            var offsets = new Offset<TeamStats>[stats.Count];

            for (var i = 0; i < stats.Count; i++)
            {
                var player = stats[i];
                var playerId = builder.CreateString(player.PlayerId);
                var playerStats = TeamStats.CreateTeamStats(builder, player.Kills, player.Deaths, playerId, (byte)player.Team);
                offsets[i] = playerStats;
            }

            var statsArray = TeamGameOver.CreateStatsVector(builder, offsets);
            var hash = Worlds.currentWorld.GetModule<NetworkModule>().GetSyncHash();
            var gameOver = TeamGameOver.CreateTeamGameOver(builder, winner, hash, statsArray);

            var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.TeamGameOver, gameOver.Value);
            builder.Finish(offset.Value);

            var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(ms);
        }
    
    }
}
