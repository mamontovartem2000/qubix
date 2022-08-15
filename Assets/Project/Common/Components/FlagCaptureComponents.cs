using ME.ECS;
using ME.ECS.Collections;

namespace Project.Common.Components
{
    public struct FlagTag : IComponent { }

    public struct FlagSpawnerTag : IComponent { }

    public struct CarriesTheFlag : IComponent
    {
        public int Team;
        public Entity Flag;
    }

    public struct FlagNeedRespawn : IComponent
    {
        public float SpawnDelay;
    }

    public struct FlagOnSpawn : IComponent { }

    public struct FlagCaptured : IComponent
    {
        public int Team;
    }

    public struct DroppedFlag : IComponent
    {
        public float WaitingTime;
    }
    
    public struct CapturedFlagsScore : IStructCopyable<CapturedFlagsScore>
    {
        public DictionaryCopyable<int, int> Score;

        void IStructCopyable<CapturedFlagsScore>.CopyFrom(in CapturedFlagsScore other)
        {
            ArrayUtils.Copy(other.Score, ref Score);
        }

        void IStructCopyable<CapturedFlagsScore>.OnRecycle()
        {
            PoolDictionaryCopyable<int, int>.Recycle(ref Score);
        }
    }
}