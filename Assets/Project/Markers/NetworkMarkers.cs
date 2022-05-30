using ME.ECS;
using Project.Modules.Network;

namespace Project.Markers
{
    public struct NetworkSetActivePlayer : IMarker
    {
        public int ActorLocalID;
        public string ServerID;
        public string Nickname;
        public TeamTypes Team;
    }

    public struct NetworkPlayerDisconnected : IMarker
    {
        public int ActorID;
    }
}