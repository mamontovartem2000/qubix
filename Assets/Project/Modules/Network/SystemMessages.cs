using FlatBuffers;
using FlatMessages;
using ME.ECS;
using System;
using System.Text;
using UnityEngine;

namespace Project.Modules.Network
{
    public static class SystemMessages
    {
        public static byte[] SystemHashMessage(uint tick, int hash)
        {
            var builder = new FlatBufferBuilder(1);
            var hashMess = SaveHash.CreateSaveHash(builder, tick, hash);
            var offset = SystemMessage.CreateSystemMessage(builder, GetTime(), Payload.SaveHash, hashMess.Value);
            builder.Finish(offset.Value);

            return builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
        }

        public static void ProcessSystemMessage(byte[] bytes)
        {
            if (bytes.Length == 4)
            {
                if (Encoding.UTF8.GetString(bytes) == "ping") 
                    return;
            }
            
            var data = SystemMessage.GetRootAsSystemMessage(new ByteBuffer(bytes));

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
                    if (NetworkData.BuildType == BuildTypes.Front_Hub)
                    {
#if UNITY_WEBGL && !UNITY_EDITOR
                    BrowserEvents.GameIsOver();
#endif
                    }
                    NetworkEvents.DestroyWorld?.Invoke();
                    break;
                default:
                    Debug.Log($"Unknown system message! Payload type: {data.PayloadType}");
                    break;
            }
        }

        public static void PlayerJoinedWorld()
        {
            var builder = new FlatBufferBuilder(1);
            var join = JoinedTheGame.CreateJoinedTheGame(builder, true);
            var offset = SystemMessage.CreateSystemMessage(builder, GetTime(), Payload.JoinedTheGame, join.Value);
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
            Debug.Log("Replay! Call Sergo!");
            var ticks = replayFrom.LastTick;
            Worlds.currentWorld.RewindTo(ticks);
        }

        public static uint GetTime()
        {
            return (uint) new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }
}
