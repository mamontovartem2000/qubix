using ME.ECS;
using UnityEngine;

namespace Project.Features.Player.Components 
{
    public struct PlayerTag : IComponent
    {
        public int PlayerID;
        public Vector3 FaceDirection;
    }

    public struct PlayerScore : IComponent
    {
        public int Value;
    }
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
    public struct PlayerIsRotating : IComponent
    {
        public bool Clockwise;
    }
    public struct PlayerHealth : IComponent
    {
        public float Value;
    }
    public struct PlayerHasStopped : IComponent {}
    public struct PlayerShouldRotate : IComponent{}

    public struct PlayerCollided : IComponent
    {
        public int Value;
    }

    public struct PlayerShot : IComponent
    {
        public AmmoType Ammo;
        public Vector3 SpawnPoint;
    }
}

public enum AmmoType
{
    Bullet,
    Rocket
}