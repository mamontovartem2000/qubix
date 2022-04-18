using ME.ECS;
using Project.Common.Components;

namespace Project.Input.InputHandler.Markers
{
    public enum InputState { Pressed, Released }

    public struct ForwardMarker : IMarker
    {
        public int ActorID;
        public InputState State;
        public MovementAxis Axis;
    }
    public struct BackwardMarker : IMarker
    {
        public int ActorID;

        public InputState State;
        public MovementAxis Axis;
    }
    public struct LeftMarker : IMarker
    {
        public int ActorID;

        public InputState State;
        public MovementAxis Axis;
    }
    public struct RightMarker : IMarker
    {
        public int ActorID;
        public InputState State;
        public MovementAxis Axis;
    }
    public struct MouseLeftMarker : IMarker
    {
        public int ActorID;
        public InputState State;
    }
    public struct MouseRightMarker : IMarker
    {
        public int ActorID;
        public InputState State;
    }
    public struct LockDirectionMarker : IMarker
    {
        public int ActorID;
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
}