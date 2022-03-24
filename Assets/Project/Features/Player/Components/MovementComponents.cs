using ME.ECS;
using UnityEngine;

namespace Project.Features.Projectile.Components 
{   
    public struct PlayerMoveTarget : IComponent 
    {
        public Vector3 Value;
    }

    public struct PlayerMovementSpeed : IComponent
    {
        public float Value;
    }

    public struct PlayerIsMoving : IComponent
    {
        public bool Forward;
    }
    
    public struct PlayerHasStopped : IComponent {}

    public struct TeleportPlayer : IComponent {}
}