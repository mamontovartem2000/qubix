using ME.ECS;
using Project.Features;
using Project.Features.SceneBuilder.Components;
using UnityEngine;

namespace Project.Utilities
{
    public static class SceneUtils
    {


        //public static bool CheckLocalPlayer(in Entity entity)
        //{
        //    var result = entity == Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer();
        //    return result;
        //}

        //public static bool IsFree(Vector3 position)
        //{
        //    return Worlds.current.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(position)] == 1;
        //}

        //private static Vector3 IndexToPosition(int index)
        //{
        //    var x = index % _width;
        //    var y = Mathf.FloorToInt(index / (float)_width);

        //    return new Vector3(x, 0f, y);
        //}

        //public static bool IsWalkable(Vector3 position, Vector3 direction)
        //{
        //    return Worlds.current.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(position + direction)] != 0;
        //}

        //public static int PositionToIndex(Vector3 vec)
        //{
        //    var x = Mathf.RoundToInt(vec.x);
        //    var y = Mathf.RoundToInt(vec.z);

        //    return y * _width + x;
        //}

        //public static Vector3 GetRandomSpawnPosition()
        //{
        //    var position = Vector3.zero;

        //    while (!IsFree(position))
        //    {
        //        var rnd = Worlds.current.GetRandomRange(0, _width * _height);
        //        position = IndexToPosition(rnd);
        //    }

        //    return position;
        //}

        //public static Vector3 GetRandomPortalPosition(Vector3 vec)
        //{
        //    World world = Worlds.current;
        //    var pos = vec;

        //    while (pos == vec)
        //    {
        //        pos = world.GetSharedData<MapComponents>()
        //            .PortalsMap[world.GetRandomRange(0, world.GetSharedData<MapComponents>().PortalsMap.Count)];
        //    }

        //    return pos;
        //}
    }
}
