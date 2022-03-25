using ME.ECS;
using UnityEngine;

namespace Project.Features.Projectile.Components 
{   
    public struct PlayerMoveTarget : IComponent 
    {
        public Vector3 Value;
    }

    public struct Moving : IComponent
    {
        public int Direction;
        public int LastDirection;
        public Vector3 LastMapCell;

        public void ChangeDirection(int dir)
        {
            LastDirection = Direction;
            Direction += dir;
        }
    }

    public struct PlayerMovementSpeed : IComponent
    {
        public float Forward;
        public float Backward;
    }

    public struct PlayerIsMoving : IComponent
    {
        public bool Forward;
    }
    
    public struct PlayerHasStopped : IComponent {}

    public struct TeleportPlayer : IComponent {}
}