using ME.ECS;
using UnityEngine;

namespace Project.Features.Player.Components 
{
    public struct PlayerTag : IComponent
    {
        public int PlayerID;
        public Vector3 FaceDirection;
        public Material Material;
    }

    public struct PlayerScore : IComponent
    {
        public int Value;
    }
    
    public struct PlayerIsRotating : IComponent
    {
        public bool Clockwise;
    }

    public struct LeftWeapon : IComponent
    {
        public AmmoType Type;
        public int MaxAmmo;
        public int Ammo;
        public float Cooldown;
        public float ReloadTime;
    }

    public struct LeftWeaponReload : IComponent
    {
        public float Time;
    }

    // public struct RightWeaponReload : IComponent
    // {
    //     public float Time;
    // }

    public struct RightWeapon : IComponent
    {
        public AmmoType Type;
        public int MaxAmmo;
        public int Ammo;
        public float Cooldown;
        public float ReloadTime;
    }
    
    public struct LeftWeaponShot : IComponent {}
    
    public struct RightWeaponShot : IComponent {}

    public struct ScoreHolder : IComponent
    {
        public int ActorID;
        public int ScoreAmount;
    }
    
    public struct RegainScore : IComponent {}
}

public enum AmmoType
{
    Bullet,
    Rocket,
    Rifle,
    Shotgun
}