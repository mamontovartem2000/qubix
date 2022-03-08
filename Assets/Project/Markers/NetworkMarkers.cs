using ME.ECS;
using Photon.Realtime;
using UnityEngine;

namespace Project.Markers
{
    public struct NetworkSetActivePlayer : IMarker
    {
        public int ActorID;
    }

    public struct NetworkPlayerDisconnected : IMarker
    {
        public int ActorID;
    }

    public struct NetworkPlayerConnectedTimeSynced : IMarker
    {
        public int ActorID;
    }

    public struct NetworkPlayerReady : IMarker
    {
        public int ActorID;
    }
}