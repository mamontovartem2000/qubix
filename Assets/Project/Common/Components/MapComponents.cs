using ME.ECS;
using ME.ECS.Collections;

namespace Project.Common.Components
{
    public struct MapComponents : IStructCopyable<MapComponents>
    {
        public BufferArray<byte> WalkableMap;
        public BufferArray<byte> FreeMap;

        public BufferArray<int> PortalsMap;
        public BufferArray<int> RedTeamSpawnPoints;
        public BufferArray<int> BlueTeamSpawnPoints;

        void IStructCopyable<MapComponents>.CopyFrom(in MapComponents other)
        {
            ArrayUtils.Copy(other.WalkableMap, ref WalkableMap);
            ArrayUtils.Copy(other.FreeMap, ref FreeMap);
            
            ArrayUtils.Copy(other.PortalsMap, ref PortalsMap);
            ArrayUtils.Copy(other.RedTeamSpawnPoints, ref RedTeamSpawnPoints);
            ArrayUtils.Copy(other.BlueTeamSpawnPoints, ref BlueTeamSpawnPoints);
        }

        void IStructCopyable<MapComponents>.OnRecycle()
        {
            PoolArray<byte>.Recycle(ref WalkableMap);
            PoolArray<byte>.Recycle(ref FreeMap);
            
            PoolArray<int>.Recycle(ref PortalsMap);
            PoolArray<int>.Recycle(ref RedTeamSpawnPoints);
            PoolArray<int>.Recycle(ref BlueTeamSpawnPoints);
        }
    }  
}