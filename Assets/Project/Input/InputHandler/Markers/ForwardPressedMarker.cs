using ME.ECS;

namespace Project.Input.InputHandler.Markers
{
    public enum InputState { Pressed, Released }
    public enum MovementAxis {Horizontal, Vertical}

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

    public struct SkillOneMarker : IMarker
    {
        public int ActorID;
    }
    public struct SkillTwoMarker : IMarker
    {
        public int ActorID;
    }
    public struct SkillThreeMarker : IMarker
    {
        public int ActorID;
    }
    public struct SkillFourMarker : IMarker
    {
        public int ActorID;
    }
}