using ME.ECS;
using ME.ECS.Collections;
using UnityEngine;

namespace Project.Features.SceneBuilder.Components
{
    public struct MapComponents : IStructCopyable<MapComponents>
    {
        public BufferArray<byte> WalkableMap;
        public BufferArray<float> HeightMap;
        public BufferArray<Vector3> PortalsMap;


        void IStructCopyable<MapComponents>.CopyFrom(in MapComponents other)
        {
            ArrayUtils.Copy(other.WalkableMap, ref WalkableMap);
            ArrayUtils.Copy(other.HeightMap, ref HeightMap);
            ArrayUtils.Copy(other.PortalsMap, ref PortalsMap);
        }

        void IStructCopyable<MapComponents>.OnRecycle()
        {
            PoolArray<byte>.Recycle(ref WalkableMap);
            PoolArray<float>.Recycle(ref HeightMap);
            PoolArray<Vector3>.Recycle(ref PortalsMap);
        }
    }  
}