using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.Mathematics;
using Project.Common.Components;

namespace Project.Features
{
    public static class SceneUtils
    {
        private static int _width;
        private static int _height;
        private static int PositionToIndex(float3 vec)
        {
            var x = (int)math.round(vec.x);
            var y = (int)math.round(vec.z);

            return y * _width + x;
        }
        
        public static int Width => _width;

        public static void SetWidthAndHeight(int width, int height)
        {
            _width = width;
            _height = height;
        }
        
        public static bool IsWalkable(float3 pos)
        {
            if ((int)pos.x >= _width || (int)pos.x <= 0 || (int)pos.z >= _height || (int)pos.z <= 0) return false;
            return Worlds.current.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(pos)] == 1;
        }

        public static bool IsFree(float3 pos)
        {
            return Worlds.current.ReadSharedData<MapComponents>().FreeMap[PositionToIndex(pos)] == 0;
        }
        
        public static float3 SafeCheckPosition(float3 vec)
        {
            return IndexToPosition(PositionToIndex(vec));
        }
        
        public static void ModifyWalkable(float3 position, bool release)
        {
            var i = PositionToIndex(position);
            Worlds.current.GetSharedData<MapComponents>().WalkableMap[i] = release ? (byte)1 : (byte)0;
        }

        public static void ModifyFree(float3 position, bool release)
        {
            var i = PositionToIndex(position);
            Worlds.current.GetSharedData<MapComponents>().FreeMap[i] = release ? (byte)0 : (byte)1;
        }

        public static float3 IndexToPosition(int index)
        {
            var x = index % _width;
            var y = math.floor(index / _width);

            return new float3(x, 0f, y);
        }

        public static float3 GetRandomPosition()
        {
            while (true)
            {
                var rnd = Worlds.current.GetRandomRange(0, _width * _height);
                var pos = IndexToPosition(rnd);

                if (pos.Equals(float3.zero)) //TODO: Can delete
                    continue;

                if (IsWalkable(pos) && IsFree(pos))
                    return pos;

                //TODO: � ���� ������ ��� ��������� �����?
            }
        }

        public static float3 GetRandomPortal(float3 vec)
        {
            var pos = vec;
            var portals = Worlds.current.ReadSharedData<MapComponents>().PortalsMap;
            
            while (pos.Equals(vec))
            {
                var rnd = Worlds.current.GetRandomRange(0, portals.Length);
                pos = IndexToPosition(portals[rnd]);

                if (!IsWalkable(pos))
                {
                    pos = vec;
                }
            }
            
            return pos;
        }

        public static float3 GetTeamSpawnPosition(BufferArray<int> spawnPoints)
        {
            //TODO: NEVER EQUAL LENGHT 0
            if (spawnPoints.Length == 0)
                return GetRandomPosition();

            float3 pos = float3.zero;
            ListCopyable<int> pool = new ListCopyable<int>();
            pool.AddRange(spawnPoints);

            while (pool.Count > 0)
            {
                var rnd = Worlds.current.GetRandomRange(0, pool.Count);
                pos = IndexToPosition(pool[rnd]);

                if (IsWalkable(pos))
                    return pos;
                else
                    pool.RemoveAt(rnd);
            }

            return pos;
        }

        public static int BurstConvert(float3 vec, int width)
        {
            var x = (int)math.round(vec.x);
            var y = (int)math.round(vec.z);

            return y * width + x;
        }
    }
}
