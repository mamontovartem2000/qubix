using ME.ECS;
using UnityEngine;

namespace Project.Common.Components 
{   
    public enum MovementAxis
    {
        Horizontal, Vertical
    }
    
    public struct PlayerMoveTarget : IComponent
    {
        public Vector3 Value;
    }

    public struct MoveInput : IComponent
    {
        public int Value;
        public MovementAxis Axis;
    }

    public struct PlayerMovementSpeed : IComponent
    {
        public float Value;
    }
    
    public struct TeleportPlayer : IComponent 
    {
        public bool NeedDelete;
    }

    public struct LockTarget : IComponent { }
}