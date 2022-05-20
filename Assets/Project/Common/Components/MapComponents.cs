using ME.ECS;
using ME.ECS.Collections;
using UnityEngine;

namespace Project.Common.Components
{
    public struct MapComponents : IStructCopyable<MapComponents>
    {
        public BufferArray<byte> WalkableMap;
        public BufferArray<fp3> PortalsMap;

        void IStructCopyable<MapComponents>.CopyFrom(in MapComponents other)
        {
            ArrayUtils.Copy(other.WalkableMap, ref WalkableMap);
            ArrayUtils.Copy(other.PortalsMap, ref PortalsMap);
        }

        void IStructCopyable<MapComponents>.OnRecycle()
        {
            PoolArray<byte>.Recycle(ref WalkableMap);
            PoolArray<fp3>.Recycle(ref PortalsMap);
        }
    }  
}