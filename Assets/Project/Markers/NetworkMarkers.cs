using ME.ECS;

namespace Project.Markers
{
    public struct NetworkSetActivePlayer : IMarker
    {
        public int ActorLocalID;
        public string ServerID;
        public string Nickname;
        public int Team;
    }

    public struct NetworkPlayerDisconnected : IMarker
    {
        public int ActorID;
    }
}