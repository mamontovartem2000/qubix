using ME.ECS;

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
}