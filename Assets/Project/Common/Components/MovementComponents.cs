using ME.ECS;
using UnityEngine;

namespace Project.Common.Components 
{   
    public struct PlayerMoveTarget : IComponent
    {
        public Vector3 Value;
    }

    public struct MoveInput : IComponent
    {
        public Vector2Int Value;
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

    public enum MovementAxis
    {
        Horizontal, Vertical
    }
}