using ME.ECS;
using Project.Input.InputHandler.Markers;
using UnityEngine;

namespace Project.Core.Features.Player.Components 
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
}