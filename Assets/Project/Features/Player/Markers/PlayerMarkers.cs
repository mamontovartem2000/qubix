using ME.ECS;

namespace Project.Features.Player.Markers {
    
    public struct PlayerMarkers : IMarker {}

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