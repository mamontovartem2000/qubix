using ME.ECS;
using ME.ECS.Collections;
using Project.Features.CollisionHandler.Components;
using Project.Features.SceneBuilder.Views;
using UnityEngine;

namespace Project.Features
{
    #region usage



    using Components;
    using Modules;
    using Systems;
    using Features;
    using Markers;
    using SceneBuilder.Components;
    using SceneBuilder.Modules;
    using SceneBuilder.Systems;
    using SceneBuilder.Markers;

    namespace SceneBuilder.Components
    {
    }

    namespace SceneBuilder.Modules
    {
    }

    namespace SceneBuilder.Systems
    {
    }

    namespace SceneBuilder.Markers
    {
    }

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

    public sealed class SceneBuilderFeature : Feature
    {
        public TextAsset Map;
        public TileView TileView;
        public PortalView PortalView;
        private int Width, Height;

        private ViewId _tileID, _portalID;
        private const int EMPTY_TILE = 0, SIMPLE_TILE = 1, PORTAL_TILE = 2;

        private Entity _init;

        protected override void OnConstruct()
        {
            PrepareMap();
        }

        protected override void OnDeconstruct()
        {
        }

        private void PrepareMap()
        {
            _init = new Entity("init");
            _tileID = world.RegisterViewSource(TileView);
            _portalID = world.RegisterViewSource(PortalView);

            GetDimensions();

            Debug.Log($"w: {Width}, h: {Height}");

            var size = Width * Height;
            var map = PoolArray<byte>.Spawn(size);

            ConvertToByte(Map, map);
            world.SetSharedData(new MapComponents {WalkableMap = map});

            DrawMap(map);
        }

        private void DrawMap(BufferArray<byte> source)
        {
            var portalIndex = 0;
            for (var i = 0; i < source.Count; i++)
            {
                switch (source[i])
                {
                    case 1:
                    {
                        var entity = new Entity("tile");
                        entity.InstantiateView(_tileID);
                        entity.SetPosition(IndexToPosition(i));
                        break;
                    }
                    case 2:
                    {
                        var entity = new Entity("tile");
                        entity.InstantiateView(_portalID);
                        entity.SetPosition(IndexToPosition(i));
                        entity.Set(new PortalTag {PortalID = portalIndex});
                        portalIndex++;
                        break;
                    }
                }
            }
        }

        private void GetDimensions()
        {
            var omg = Map.text.Split('\n');

            Height = omg.Length;
            Width = omg[0].Length - 1;
        }

        private void ConvertToByte(TextAsset source, BufferArray<byte> target)
        {
            var result = new byte[Width * Height];
            var bytes = source.text;
            var byteIndex = 0;

            var lines = bytes.Split('\n');

            foreach (var l in lines)
            {
                for (int i = 0; i < l.Length; i++)
                {
                    switch (l[i])
                    {
                        case '0':
                            result[byteIndex] = 0;
                            byteIndex++;
                            break;
                        case '1':
                            result[byteIndex] = 1;
                            byteIndex++;
                            break;
                        case '2':
                            result[byteIndex] = 2;
                            byteIndex++;
                            break;
                    }
                }
            }

            for (int j = 0; j < result.Length; j++)
            {
                target[j] = result[j];
            }
        }

        public void MoveTo(int moveFrom, int moveTo)
        {
            world.GetSharedData<MapComponents>().WalkableMap[moveFrom] = SIMPLE_TILE;
            world.GetSharedData<MapComponents>().WalkableMap[moveTo] = EMPTY_TILE;
        }

        public int PositionToIndex(Vector3 vec)
        {
            var x = Mathf.RoundToInt(vec.x);
            var y = Mathf.RoundToInt(vec.z);

            return y * Width + x;
        }

        public Vector3 IndexToPosition(int index)
        {
            var x = index % Width;
            var y = Mathf.FloorToInt(index / (float) Width);

            return new Vector3(x, 0, y);
        }

        public bool IsFree(Vector3 position, Vector3 direction)
        {
            var toIndex = PositionToIndex(position + direction);

            if (toIndex < 0 || toIndex >= Width * Height) return false;

            return world.ReadSharedData<MapComponents>().WalkableMap[toIndex] == 1;
        }

        public Vector3 GetRandomSpawnPosition()
        {
            var position = Vector3.zero;
            var rnd = 0;

            while (!IsFree(position, Vector3.zero))
            {
                rnd = world.GetRandomRange(0, Width * Height);
                position = IndexToPosition(rnd);
            }

            return position;
        }

        public Vector3 GetRandomPortalPosition(Vector3 pos)
        {
            return Vector3.zero;
        }
}
}