using ME.ECS;
using ME.ECS.Collections;
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
        public int PlayerCount;
        
        public TileView TileView;
        public PortalView PortalView;
        
        public MineMono MineView;
        public HealthMono HealthView;
        
        private int Width, Height, PortalCount;

        private ViewId _tileID, _portalID;
        private const int EMPTY_TILE = 0, SIMPLE_TILE = 1, PORTAL_TILE = 2;

        private Entity _init;

        protected override void OnConstruct()
        {
            AddSystem<SpawnHealthSystem>();
            AddSystem<SpawnMineSystem>();
            
            PrepareMap();
        }

        protected override void OnDeconstruct() {}

        private void PrepareMap()
        {
            _init = new Entity("init");
            
            _tileID = world.RegisterViewSource(TileView);
            _portalID = world.RegisterViewSource(PortalView);

            GetDimensions();

            var size = Width * Height;
            var map = PoolArray<byte>.Spawn(size);
            var portals = PoolArray<Vector3>.Spawn(PortalCount);

            ConvertMap(Map, map, portals);

            world.SetSharedData(new MapComponents {WalkableMap = map, PortalsMap = portals});
            
            DrawMap(map);
        }

        private void DrawMap(BufferArray<byte> source)
        {
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

            foreach (var line in omg)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '2')
                    {
                        PortalCount++;
                    }
                }
            }
        }

        private void ConvertMap(TextAsset source, BufferArray<byte> walkable, BufferArray<Vector3> portals)
        {
            var walkableResult = new byte[Width * Height];
            var portalsResult = new Vector3[PortalCount];
            
            var bytes = source.text;
            var byteIndex = 0;
            var portalIndex = 0;
            
            var lines = bytes.Split('\n');

            foreach (var line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    switch (line[i])
                    {
                        case '0':
                            walkableResult[byteIndex] = 0;
                            byteIndex++;
                            break;
                        case '1':
                            walkableResult[byteIndex] = 1;
                            byteIndex++;
                            break;
                        case '2':
                            walkableResult[byteIndex] = 2;
                            portalsResult[portalIndex] = IndexToPosition(byteIndex);
                            byteIndex++;
                            portalIndex++;
                            break;
                    }
                }
            }
            
            for (int j = 0; j < walkableResult.Length; j++)
            {
                walkable[j] = walkableResult[j];
            }

            for (int j = 0; j < portalsResult.Length; j++)
            {
                portals[j] = portalsResult[j];
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
        private Vector3 IndexToPosition(int index)
        {
            var x = index % Width;
            var y = Mathf.FloorToInt(index / (float) Width);

            return new Vector3(x, 0f, y);
        }

        public bool IsFree(Vector3 position, Vector3 direction)
        {
            var toIndex = PositionToIndex(position + direction);

            if (toIndex < 0 || toIndex >= Width * Height) return false;

            var result = world.ReadSharedData<MapComponents>().WalkableMap[toIndex] == SIMPLE_TILE ||
                         world.ReadSharedData<MapComponents>().WalkableMap[toIndex] == PORTAL_TILE;
            return result;
        }

        public Vector3 GetRandomSpawnPosition()
        {
            var position = Vector3.zero;

            while (!IsFree(position, Vector3.zero))
            {
                var rnd = world.GetRandomRange(0, Width * Height);
                position = IndexToPosition(rnd);
            }

            return position;
        }
        
        public Vector3 GetRandomPortalPosition(Vector3 vec)
        {
            var pos = vec;
            
            while (pos == vec)
            {
                pos = world.GetSharedData<MapComponents>()
                    .PortalsMap[world.GetRandomRange(0, world.GetSharedData<MapComponents>().PortalsMap.Count)];
            }

            return pos;
        }
    }
}