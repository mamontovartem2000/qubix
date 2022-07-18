using ME.ECS;

namespace Project.Common.Components 
{
    public struct FlagTag : IComponent { }

    public struct FlagSpawnerTag : IComponent { }

    public struct CarriesTheFlag : IComponent
    {
        public int Team;
    }

    public struct FlagNeedRespawn : IComponent { }

    public struct FlagOnSpawn : IComponent { }

    public struct DroppedFlag : IComponent
    {
        public float WatingTime;
    }
}