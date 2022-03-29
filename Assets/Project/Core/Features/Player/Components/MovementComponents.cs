using ME.ECS;
using UnityEngine;

namespace Project.Core.Features.Player.Components 
{   
    public struct PlayerMoveTarget : IComponent 
    {
        public Vector3 Value;
    }

    public struct MoveInput : IComponent
    {
        public int Value;
    }

    public struct PlayerMovementSpeed : IComponent
    {
        public float Forward;
        public float Backward;
    }

    public struct TeleportPlayer : IComponent 
    {
        public bool NeedDelete;
    }

    public struct LockDirection : IComponent { }
}