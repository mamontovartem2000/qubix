using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player;
using Project.Core.Features.SceneBuilder.Components;
using UnityEngine;

namespace Project.Core
{
    public static class SceneUtils
    {
        public const float ItemRadius = 0.2f;
        public const float PlayerRadius = 0.6f;
        public const float PlayerRadiusSQR = PlayerRadius * PlayerRadius;

        private static int _width, _height;

        public static void SetWidthAndHeight(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public static Entity[] ConvertFilterToEntityArray(Filter filter)
        {
            Entity[] entities = new Entity[filter.Count];
            int index = 0;

            foreach (var entity in filter)
            {
                entities[index] = entity;
                index++;
            }
            return entities;
        }

        public static int PositionToIndex(Vector3 vec)
        {
            var x = Mathf.RoundToInt(vec.x);
            var y = Mathf.RoundToInt(vec.z);

            return y * _width + x;
        }

        public static Vector3 IndexToPosition(int index)
        {
            var x = index % _width;
            var y = Mathf.FloorToInt(index / (float)_width);

            return new Vector3(x, 0f, y);
        }

        public static bool IsWalkable(Vector3 position, Vector3 direction)
        {
            return Worlds.current.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(position + direction)] != 0;
        }

        public static bool IsWalkable(Vector3 target)
        {
            return Worlds.current.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(target)] != 0;
        }

        public static bool IsFree(Vector3 position)
        {
            return Worlds.current.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(position)] == 1;
        }

        public static Vector3 GetRandomSpawnPosition()
        {
            var position = Vector3.zero;

            while (!IsFree(position))
            {
                var rnd = Worlds.current.GetRandomRange(0, _width * _height);
                position = IndexToPosition(rnd);
            }

            return position;
        }

        public static bool CheckLocalPlayer(Entity player)
        {
            // Debug.Log(player == Worlds.current.GetFeature<PlayerFeature>().GetPlayer(player.Read<PlayerTag>().PlayerID));
            return player == Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(player.Read<PlayerTag>().PlayerID);
        }
    }
}
