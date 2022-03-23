using ME.ECS;

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

public struct LeftWeaponShot : IComponent { }

public struct RightWeaponShot : IComponent { }


public enum WeaponType
{
    Gun,
    Rocket,
    Rifle,
    Shotgun
}