using ME.ECS;
using ME.ECS.Collections;
using UnityEngine;

namespace Project.Common.Components
{
    public struct MapComponents : IStructCopyable<MapComponents>
    {
        public BufferArray<byte> WalkableMap;
        public BufferArray<byte> PortalsMap;
        public BufferArray<byte> MineMap;

        void IStructCopyable<MapComponents>.CopyFrom(in MapComponents other)
        {
            ArrayUtils.Copy(other.WalkableMap, ref WalkableMap);
            ArrayUtils.Copy(other.PortalsMap, ref PortalsMap);
            ArrayUtils.Copy(other.MineMap, ref MineMap);
        }

        void IStructCopyable<MapComponents>.OnRecycle()
        {
            PoolArray<byte>.Recycle(ref WalkableMap);
            PoolArray<byte>.Recycle(ref PortalsMap);
            PoolArray<byte>.Recycle(ref MineMap);
        }
    }  
}