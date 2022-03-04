using ME.ECS;
using Photon.Realtime;
using UnityEngine;

namespace Project.Markers
{
    public struct NetworkSetActivePlayer : IMarker
    {
        public Photon.Realtime.Player Player;
    }

    public struct NetworkPlayerDisconnected : IMarker
    {
        public Photon.Realtime.Player Player;
    }

    public struct NetworkPlayerConnectedTimeSynced : IMarker
    {
        public int ActorID;
    }
}