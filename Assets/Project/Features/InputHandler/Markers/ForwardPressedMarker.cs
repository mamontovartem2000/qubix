using ME.ECS;

namespace Project.Features.InputHandler.Markers 
{
    public enum Key {Forward, Backward, Left, Right}

    public struct KeyPressedMarker : IMarker
    {
        public int ActorID;
        public Key Key;
    }

    public struct KeyReleasedMarker : IMarker
    {
        public int ActorID;
        public Key Key;
    }

    public enum Button {Left, Right}
    
    public struct ButtonPressedMarker : IMarker
    {
        public int ActorID;
        public Button Button;
    }

    public struct ButtonReleasedMarker : IMarker
    {
        public int ActorID;
        public Button Button;
    }
}