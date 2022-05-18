using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player;
using Project.Core.Features.SceneBuilder.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Core
{
    public static class SceneUtils
    {
        public const float ItemRadius = 0.2f;
        public const float PlayerRadius = 0.6f;
        public const float PlayerRadiusSQR = PlayerRadius * PlayerRadius;
        public const float ItemRadiusSQR = ItemRadius * ItemRadius;

        public static int Width, Height;

        public static void SetWidthAndHeight(int width, int height)
        {
            Width = width;
            Height = height;
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

            return y * Width + x;
        }

        public static Vector3 IndexToPosition(int index)
        {
            var x = index % Width;
            var y = Mathf.FloorToInt(index / (float) Width);

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
                var rnd = Worlds.current.GetRandomRange(0, Width * Height);
                position = IndexToPosition(rnd);
            }

            return position;
        }

        public static bool CheckLocalPlayer(Entity player)
        {
            // Debug.Log(player == Worlds.current.GetFeature<PlayerFeature>().GetPlayer(player.Read<PlayerTag>().PlayerID));
            return player == Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(player.Read<PlayerTag>().PlayerLocalID);
        }

        public static void Move(Vector3 currentPos, Vector3 targetPos)
        {
            int moveFrom = SceneUtils.PositionToIndex(currentPos);
            Worlds.current.GetSharedData<MapComponents>().WalkableMap[moveFrom] = 1;
            TakeTheCell(targetPos);
        }

        public static void TakeTheCell(Vector3 targetPos)
        {
            int moveTo = PositionToIndex(targetPos);
            Worlds.current.GetSharedData<MapComponents>().WalkableMap[moveTo] = 0;
        }

        public static void TakeTheCell(int index)
        {
            Worlds.current.GetSharedData<MapComponents>().WalkableMap[index] = 0;
        }

        public static void ReleaseTheCell(Vector3 currentPos)
        {
            int moveFrom = PositionToIndex(currentPos);
            Worlds.current.GetSharedData<MapComponents>().WalkableMap[moveFrom] = 1;
        }
    }

    [Serializable]
    public class GameMapRemoteData
    {
        public byte[] bytes;
        public int offset;

        public GameMapRemoteData(TextAsset sourceMap)
        {
            var omg = sourceMap.text.Split('\n');
            List<byte> byteList = new List<byte>();

            var arr = omg[0].Split(' ');
            offset = arr.Length + 2;
            var wall = CreateWalls();
            byteList.AddRange(wall);

            foreach (var line in omg)
            {
                byteList.Add(0);
                var stringArray = line.Split(' ');
                byte[] byteArray = Array.ConvertAll(stringArray, s => byte.Parse(s));
                byteList.AddRange(byteArray);
                byteList.Add(0);
            }

            byteList.AddRange(wall);
            bytes = byteList.ToArray();

            //DebugMapMatrix();
        }

        private List<byte> CreateWalls()
        {
            List<byte> wall = new List<byte>(offset);

            for (int i = 0; i < offset; i++)
            {
                wall.Add(0);
            }
            return wall;
        }

        private void DebugMapMatrix()
        {
            int count = 0;
            string df = string.Empty;

            for (int i = 0; i < bytes.Length; i++)
            {
                count++;
                df += bytes[i].ToString();

                if (count == offset)
                {
                    count = 0;
                    df += "\n";
                }
            }
            Debug.Log(df);
        }
    }
}
