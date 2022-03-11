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
    
    public struct PlayerDisplay : IComponent {}

    public struct PlayerScore : IComponent
    {
        public int Kills;
        public int Deaths;
    }
    
    public struct PlayerIsRotating : IComponent
    {
        public bool Clockwise;
    }

    public struct LeftWeapon : IComponent
    {
        public WeaponType Type;
        public int MaxAmmo;
        public int Ammo;
        public float Cooldown;
        public float ReloadTime;
    }

    public struct LeftWeaponReload : IComponent
    {
        public float Time;
    }

    public struct RightWeapon : IComponent
    {
        public WeaponType Type;
        public int MaxCount;
        public int Count;
        public float Cooldown;
    }
    
    public struct LeftWeaponShot : IComponent {}
    
    public struct RightWeaponShot : IComponent {}
}

public enum WeaponType
{
    Gun,
    Rocket,
    Rifle,
    Shotgun
}