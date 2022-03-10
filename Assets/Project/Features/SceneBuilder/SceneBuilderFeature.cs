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
        public TextAsset SourceMap;
        public int PlayerCount;
        public Material _glowMat;
        public TileView TileView;
        public PortalMono PortalView;
        public AmmoTileMono AmmoTileView;

        public MineMono MineView;
        public HealthMono HealthView;
        public RocketAmmoMono RocketAmmoView;
        public RifleAmmoMono RifleAmmoView;

        private int Width, Height, PortalCount, AmmoCount;

        private ViewId _tileID, _portalTileID, _ammoTileID;

        private const int EMPTY_TILE = 0, SIMPLE_TILE = 1, PORTAL_TILE = 2, AMMO_TILE = 3;

        private Entity _init;

        protected override void OnConstruct()
        {
            AddSystem<SpawnHealthSystem>();
            AddSystem<SpawnMineSystem>();
            AddSystem<SpawnAmmoSystem>();

            PrepareMap();
        }
        
        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder.WithoutShared<GamePaused>();
        }

        public void ChangeColorGlowingMaterial()
        {
            var rnd = world.GetRandomRange(1, 6);
            switch (rnd)
            {
                case 1:
                    _glowMat.SetColor("_EmissionColor", Color.blue);
                    break;
                case 2:
                    _glowMat.SetColor("_EmissionColor", Color.cyan);
                    break;
                case 3:
                    _glowMat.SetColor("_EmissionColor", Color.green);
                    break;
                case 4:
                    _glowMat.SetColor("_EmissionColor", Color.magenta);
                    break;
                case 5:
                    _glowMat.SetColor("_EmissionColor", Color.red);
                    break;
                case 6:
                    _glowMat.SetColor("_EmissionColor", Color.yellow);
                    break;
            }
        }

        protected override void OnDeconstruct() { }

        private void PrepareMap()
        {
            _init = new Entity("init");

            _tileID = world.RegisterViewSource(TileView);
            _portalTileID = world.RegisterViewSource(PortalView);
            _ammoTileID = world.RegisterViewSource(AmmoTileView);

            GetDimensions();

            var size = Width * Height;
            var map = PoolArray<byte>.Spawn(size);
            var portals = PoolArray<Vector3>.Spawn(PortalCount);
            var ammos = PoolArray<Vector3>.Spawn(AmmoCount);
            var players = PoolArray<bool>.Spawn(PlayerCount);

            ConvertMap(SourceMap, map, portals, ammos);

            world.SetSharedData(new MapComponents { WalkableMap = map, PortalsMap = portals, AmmoMap = ammos, PlayerStatus = players });

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
                            var entity = new Entity("portal-tile");
                            entity.InstantiateView(_portalTileID);
                            entity.SetPosition(IndexToPosition(i));
                            break;
                        }
                    case 3:
                        {
                            var entity = new Entity("ammo-tile");
                            entity.InstantiateView(_ammoTileID);
                            entity.SetPosition(IndexToPosition(i));

                            entity.Set(new AmmoTileTag { Spawned = false, TimerDefault = 8, Timer = 8 });
                            break;
                        }
                }
            }
        }

        private void GetDimensions()
        {
            var omg = SourceMap.text.Split('\n');

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

                    if (line[i] == '3')
                    {
                        AmmoCount++;
                    }
                }
            }
        }

        private void ConvertMap(TextAsset source, BufferArray<byte> walkable, BufferArray<Vector3> portals, BufferArray<Vector3> ammos)
        {
            var walkableResult = new byte[Width * Height];
            var portalsResult = new Vector3[PortalCount];
            var ammoResult = new Vector3[AmmoCount];

            var bytes = source.text;
            var byteIndex = 0;
            var portalIndex = 0;
            var ammoIndex = 0;

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
                        case '3':
                            walkableResult[byteIndex] = 3;
                            ammoResult[ammoIndex] = IndexToPosition(byteIndex);
                            byteIndex++;
                            ammoIndex++;
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

            for (int j = 0; j < portalsResult.Length; j++)
            {
                ammos[j] = ammoResult[j];
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
            var y = Mathf.FloorToInt(index / (float)Width);

            return new Vector3(x, 0f, y);
        }

        public bool IsWalkable(Vector3 position, Vector3 direction)
        {
            return world.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(position + direction)] != 0;
        }

        public bool IsFree(Vector3 position)
        {
            return world.ReadSharedData<MapComponents>().WalkableMap[PositionToIndex(position)] == 1;
        }

        public Vector3 GetRandomSpawnPosition()
        {
            var position = Vector3.zero;

            while (!IsFree(position))
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