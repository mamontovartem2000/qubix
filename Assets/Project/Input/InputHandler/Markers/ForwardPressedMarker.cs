using ME.ECS;
using Project.Common.Components;

namespace Project.Input.InputHandler.Markers
{
    public enum InputState { Pressed, Released }

    public struct MovementMarker : IMarker
    {
        public MovementAxis Axis;
        public int Value;
    }
    public struct MouseLeftMarker : IMarker
    {
        public InputState State;
    }
    public struct MouseRightMarker : IMarker
    {
        public InputState State;
    }
    public struct LockDirectionMarker : IMarker
    {
        public InputState State;
    }

    public struct FirstSkillMarker : IMarker
    {
        public int ActorID;
    }
    public struct SecondSkillMarker : IMarker
    {
        public int ActorID;
    }
    public struct ThirdSkillMarker : IMarker
    {
        public int ActorID;
    }
    public struct FourthSkillMarker : IMarker
    {
        public int ActorID;
    }
    public struct TabulationMarker : IMarker
    {
        public InputState State;
    }
}