using ME.ECS;

namespace Project.Core.Features.Player.Markers {
    
    public struct SelectColorMarker : IMarker
    {
        public int ActorID;
        public int ColorID;
    }

    public struct PlayerReadyMarker : IMarker
    {
        public int ActorID;
    }
}