using ME.ECS;
using ME.ECS.Collections;

namespace Project.Common.Components
{
    public struct MapComponents : IStructCopyable<MapComponents>
    {
        public BufferArray<byte> WalkableMap;
        public BufferArray<byte> PortalsMap;
        public BufferArray<byte> MineMap;
        public BufferArray<int> SpawnPoints;

        void IStructCopyable<MapComponents>.CopyFrom(in MapComponents other)
        {
            ArrayUtils.Copy(other.WalkableMap, ref WalkableMap);
            ArrayUtils.Copy(other.PortalsMap, ref PortalsMap);
            ArrayUtils.Copy(other.MineMap, ref MineMap);
            ArrayUtils.Copy(other.SpawnPoints, ref SpawnPoints);
        }

        void IStructCopyable<MapComponents>.OnRecycle()
        {
            PoolArray<byte>.Recycle(ref WalkableMap);
            PoolArray<byte>.Recycle(ref PortalsMap);
            PoolArray<byte>.Recycle(ref MineMap);
            PoolArray<int>.Recycle(ref SpawnPoints);
        }
    }  
}