using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Input.InputHandler.Markers
{
    public enum InputState { Pressed, Released }

    public struct MovementMarker : IMarker
    {
        public MovementAxis Axis;
        public int Value;
    }
    
    public struct ForwardMarker : IMarker
    {
        public InputState State;
        public MovementAxis Axis;
    }
    public struct BackwardMarker : IMarker
    {
        public InputState State;
        public MovementAxis Axis;
    }
    public struct LeftMarker : IMarker
    {
        public InputState State;
        public MovementAxis Axis;
    }
    public struct RightMarker : IMarker
    {
        public InputState State;
        public MovementAxis Axis;
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
}