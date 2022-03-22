using ME.ECS;
using ME.ECS.Collections;
using UnityEngine;

namespace Project.Core.Features.SceneBuilder.Components 
{
    public struct MapComponents : IStructCopyable<MapComponents>
    {
        public BufferArray<byte> WalkableMap;
        public BufferArray<Vector3> PortalsMap;
        public BufferArray<Vector3> AmmoMap;
        public BufferArray<float> HeightMap;
        public BufferArray<bool> PlayerStatus;

        void IStructCopyable<MapComponents>.CopyFrom(in MapComponents other) 
        {
            ArrayUtils.Copy(other.WalkableMap, ref WalkableMap);
            ArrayUtils.Copy(other.PortalsMap, ref PortalsMap);
            ArrayUtils.Copy(other.HeightMap,ref HeightMap);
            ArrayUtils.Copy(other.AmmoMap,ref AmmoMap);
            ArrayUtils.Copy(other.PlayerStatus, ref PlayerStatus);
        }
        
        void IStructCopyable<MapComponents>.OnRecycle() 
        {
            PoolArray<byte>.Recycle(ref WalkableMap);
            PoolArray<Vector3>.Recycle(ref PortalsMap);
            PoolArray<float>.Recycle(ref HeightMap);
            PoolArray<Vector3>.Recycle(ref AmmoMap);
            PoolArray<bool>.Recycle(ref PlayerStatus);
        }
    }
}