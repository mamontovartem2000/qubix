using ME.ECS;
using Project.Common.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Core
{
    public static class SceneUtils
    {
        public const float ItemRadius = 0.2f;

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

        //positioning index methods
        public static int PositionToIndex(fp3 vec)
        {
            var x = (int)fpmath.round(vec.x);
            var y = (int)fpmath.round(vec.z);

            return y * Width + x;
        }

        public static fp3 IndexToPosition(int index)
        {
            var x = index % Width;
            var y = fpmath.floor(index / (fp) Width);

            return new fp3(x, 0f, y);
        }

        //burst compile compatible overloads
        public static int PositionToIndex(fp3 vec, int width)
        {
            var x = (int)fpmath.round(vec.x);
            var y = (int)fpmath.round(vec.z);

            return y * width + x;
        }

        //position check methods
        public static fp3 GetRandomFreePosition()
        {
            var pos = fp3.zero;

            while (!IsWalkable(pos))
            {
                var rnd = Worlds.current.GetRandomRange(0, Width * Height);
                pos = IndexToPosition(rnd);
                
                if (!IsFree(pos)) pos = fp3.zero;
            }
            
            return pos;
        }

        public static bool IsWalkable(fp3 pos)
        {
            if (PositionToIndex(pos) < 0 || PositionToIndex(pos) > Worlds.current.ReadSharedData<MapComponents>().WalkableMap.Count - 1) return false;

            if (Worlds.current.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(pos)] == 1) return true;
            
            return false;
        }

        public static bool IsFree(fp3 pos)
        {
            return Worlds.current.ReadSharedData<MapComponents>().MineMap[PositionToIndex(pos)] == 0;
        }

        //walkable map index changing methods
        public static void Move(Vector3 currentPos, Vector3 targetPos)
        {
            int moveFrom = PositionToIndex(currentPos);
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

        public static void PlantMine(fp3 pos)
        {
            var i = PositionToIndex(pos);
            Worlds.current.GetSharedData<MapComponents>().MineMap[i] = 1;
        }

        public static void ReleaseMine(fp3 pos)
        {
            var i = PositionToIndex(pos);
            Worlds.current.GetSharedData<MapComponents>().MineMap[i] = 0;
        }

        public static void TakePortal(fp3 pos)
        {
            var i = PositionToIndex(pos);
            Worlds.current.GetSharedData<MapComponents>().PortalsMap[i] = 0;
        }

        public static void ReleasePortal(fp3 pos)
        {
            var i = PositionToIndex(pos);
            Worlds.current.GetSharedData<MapComponents>().PortalsMap[i] = 1;
        }

        public static fp3[] GetAvailablePortalPositions()
        {
            var tmp = 0;

            foreach (var b in Worlds.current.ReadSharedData<MapComponents>().PortalsMap)
            {
                if (b == 1)
                    tmp++;
            }

            var ar = new fp3[tmp];
            var idx = 0;
            
            for (int i = 0; i < Worlds.current.ReadSharedData<MapComponents>().PortalsMap.Length; i++)
            {
                if (Worlds.current.ReadSharedData<MapComponents>().PortalsMap[i] == 1)
                {
                    ar[idx] = IndexToPosition(i);
                    idx++;
                }
            }

            return ar;
        }
        public static fp3 GetRandomPortalPosition(fp3[] vec)
        {
            var pos = fp3.zero;

            while (!IsWalkable(pos))
            {
                var rnd = Worlds.current.GetRandomRange(0, vec.Length);
                pos = vec[rnd];
            }
            
            return pos;
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
            ParseMap(omg);
        }

        public GameMapRemoteData(string sourceMap)
        {
            var omg = sourceMap.Split('\n');
            ParseMap(omg);
        }

        private void ParseMap(string[] lines)
        {
            List<byte> byteList = new List<byte>();

            var arr = lines[0].Split(' ');
            offset = arr.Length + 2;
            var wall = CreateWalls();
            byteList.AddRange(wall);

            foreach (var line in lines)
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
