using ME.ECS;
using UnityEngine;

namespace Project.Core.Features.Player.Components 
{
    public struct MovementComponents : IComponent {}
    
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
    
    public struct PlayerShouldRotate : IComponent{}
    
    public struct TeleportPlayer : IComponent
    {
        public int CurrentID;
    }
    
}