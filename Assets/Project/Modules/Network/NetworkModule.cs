using FlatBuffers;
using FlatMessages;
using ME.ECS;
using Project.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Modules.Network
{
    #region usage

    using TState = ProjectState;

    /// <summary>
    /// We need to implement our own NetworkModule class without any logic just to catch your State type into ECS.Network
    /// You can use some overrides to setup history config for your project
    /// </summary>
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

    public class NetworkModule : ME.ECS.Network.NetworkModule<TState>
    {
        public bool FakeConnect = false;

        protected override int GetRPCOrder()
        {
            return NetworkData.PlayerIdInRoom;
        }

        protected override ME.ECS.Network.NetworkType GetNetworkType()
        {
            return ME.ECS.Network.NetworkType.SendToNet | ME.ECS.Network.NetworkType.RunLocal;
        }

        protected override void OnInitialize()
        {
            if (!FakeConnect)
            {
                var instance = (ME.ECS.Network.INetworkModuleBase)this;
                instance.SetTransporter(new NetTransporter());
                instance.SetSerializer(new FSSerializer());
            }

            Worlds.currentWorld.AddMarker(new NetworkSetActivePlayer { ActorID = NetworkData.PlayerIdInRoom });
            Worlds.currentWorld.AddMarker(new NetworkPlayerConnectedTimeSynced { ActorID = NetworkData.PlayerIdInRoom });
        }
    }

    public class NetTransporter : ME.ECS.Network.ITransporter
    {
        private Queue<byte[]> _queue = new Queue<byte[]>();
        private Queue<byte[]> _queueSystem = new Queue<byte[]>();

        private int sentCount;
        private int sentBytesCount;
        private int receivedCount;
        private int receivedBytesCount;

        public NetTransporter()
        {
            if (NetworkData.Connect != null)
                NetworkData.Connect.GetMessage += GetMessage;
            else
                Debug.Log("Ошибка. Нет NetworkData.Connect");
        }

        private void GetMessage(byte[] bytes)
        {
            byte[] buffer = new byte[bytes.Length - 1];
            Array.Copy(bytes, 1, buffer, 0, buffer.Length);

            if (bytes[0] == 1)
                _queueSystem.Enqueue(buffer);
            else if (bytes[0] == 0)
                _queue.Enqueue(buffer);
        }

        public void Send(byte[] bytes)
        {
            long tick = Worlds.currentWorld.GetCurrentTick();
            byte[] tickInByte = BitConverter.GetBytes((int)tick);
            byte[] result = tickInByte.Concat(bytes).ToArray();

            NetworkData.Connect.SendMessage(result);
            this.sentBytesCount += result.Length;
            ++this.sentCount;
        }

        public void SendSystem(byte[] bytes)
        {
            //NetworkData.Connect.SendSystemMessage(bytes);
            //this.sentBytesCount += bytes.Length;
        }

        public void SendSystemHash(uint tick, int hash)
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1);
            var hashMess = ReplayFrom.CreateReplayFrom(builder, tick, hash);
            var offset = SystemMessage.CreateSystemMessage(builder, (uint)DateTime.Now.Ticks, Payload.SaveHash, hashMess.Value);
            builder.Finish(offset.Value);

            var bytes = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(bytes);
            this.sentBytesCount += bytes.Length;
        }

        private void ProcessMySystemMessage(byte[] bytes)
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
                default:
                    Debug.Log("Unknown system message!");
                    break;
            }
        }

        private void SetServerTime(TimeFromStart timeFromStart)
        {
            var world = Worlds.currentWorld;
            var serverTime = timeFromStart.Time / 1000;
            world.SetTimeSinceStart(serverTime);
        }

        private void GetReplayHash(ReplayFrom replayFrom)
        {
            //TODO: Delete hash from this mesaage
            uint ticks = replayFrom.LastTick;
            int hash = replayFrom.LastHash;
        }

        public byte[] Receive()
        {
            if (this._queue.Count == 0)
            {
                if (this._queueSystem.Count == 0) return null;

                var bytes = this._queueSystem.Dequeue();
                ProcessMySystemMessage(bytes);
                this.receivedBytesCount += bytes.Length;

                return null;
            }
            else
            {
                var bytes = this._queue.Dequeue();

                ++this.receivedCount;
                this.receivedBytesCount += bytes.Length;

                return bytes;
            }
        }

        public bool IsConnected() { return NetworkData.Connect != null; }

        public int GetEventsBytesReceivedCount() { return this.receivedBytesCount; }

        public int GetEventsBytesSentCount() { return this.sentBytesCount; }

        public int GetEventsReceivedCount() { return this.receivedCount; }

        public int GetEventsSentCount() { return this.sentCount; }
    }

    public class FSSerializer : ME.ECS.Network.ISerializer
    {
        public byte[] SerializeStorage(ME.ECS.StatesHistory.HistoryStorage historyEvent)
        {
            return ME.ECS.Serializer.Serializer.Pack(historyEvent);
        }

        public ME.ECS.StatesHistory.HistoryStorage DeserializeStorage(byte[] bytes)
        {
            return ME.ECS.Serializer.Serializer.Unpack<ME.ECS.StatesHistory.HistoryStorage>(bytes);
        }

        public byte[] Serialize(ME.ECS.StatesHistory.HistoryEvent historyEvent)
        {
            return ME.ECS.Serializer.Serializer.Pack(historyEvent);
        }

        public ME.ECS.StatesHistory.HistoryEvent Deserialize(byte[] bytes)
        {
            return ME.ECS.Serializer.Serializer.Unpack<ME.ECS.StatesHistory.HistoryEvent>(bytes);
        }

        public byte[] SerializeWorld(ME.ECS.World.WorldState data)
        {
            return ME.ECS.Serializer.Serializer.Pack(data);
        }

        public ME.ECS.World.WorldState DeserializeWorld(byte[] bytes)
        {
            return ME.ECS.Serializer.Serializer.Unpack<ME.ECS.World.WorldState>(bytes);
        }
    }
}
