using ME.ECS;
using Unity.Mathematics;

namespace Project.Common.Components 
{   
    public enum MovementAxis
    {
        Horizontal, Vertical
    }
    
    public struct PlayerMoveTarget : IComponent
    {
        public float3 Value;
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