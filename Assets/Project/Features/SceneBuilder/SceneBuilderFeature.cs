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
    using Project.Utilities;

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
        [Header("General")]
        [SerializeField] private TextAsset _sourceMap;
        [SerializeField] private int _playerCount;

        [Header("Tiles")]
        [SerializeField] private TileView _tileView;
        [SerializeField] private PortalMono _portalView;
        [SerializeField] private AmmoTileMono _ammoTileView;
        public MineMono MineView;
        public HealthMono HealthView;
        public RocketAmmoMono RocketAmmoView;
        public RifleAmmoMono RifleAmmoView;

        public int PlayerCount => _playerCount;

        public int Width { get; private set; }
        public int Height { get; private set; }


        private ViewId _tileID, _portalTileID, _ammoTileID;

        private const int EMPTY_TILE = 0, SIMPLE_TILE = 1, PORTAL_TILE = 2, AMMO_TILE = 3;

        public Entity TimerEntity { get; private set; }

        protected override void OnConstruct()
        {
            AddSystem<SpawnHealthSystem>();
            AddSystem<SpawnMineSystem>();
            AddSystem<SpawnAmmoSystem>();

            PrepareMap();
        }      

        protected override void OnDeconstruct() { }

        private void PrepareMap()
        {
            TimerEntity = new Entity("Init");

            _tileID = world.RegisterViewSource(_tileView);
            _portalTileID = world.RegisterViewSource(_portalView);
            _ammoTileID = world.RegisterViewSource(_ammoTileView);

            //GetDimensions_new(out int portalCount, out int ammoCount);
            GetDimensions(out int portalCount, out int ammoCount);


            var size = Width * Height;
            var map = PoolArray<byte>.Spawn(size);
            var portals = PoolArray<Vector3>.Spawn(portalCount);
            var ammos = PoolArray<Vector3>.Spawn(ammoCount);
            var players = PoolArray<bool>.Spawn(_playerCount);

            ConvertMap(_sourceMap, map, portals, ammos);

            world.SetSharedData(new MapComponents { WalkableMap = map, PortalsMap = portals, AmmoMap = ammos, PlayerStatus = players });

            DrawMap(map);
        }

        private void ParseMap()
        {
            string data = string.Empty;
            GameMapRemoteData map = ParceUtils.CreateFromJSON<UniversalData<GameMapRemoteData>>(data).data;

            //GetDimensions_new(map);

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

        private void GetDimensions_new(out int portalCount, out int ammoCount)
        {
            GameMapRemoteData mapData = new GameMapRemoteData(_sourceMap);
            portalCount = 0;
            ammoCount = 0;

            byte[] mapInByte = mapData.bytes;

            Height = mapData.offset;
            Width = mapInByte.Length / mapData.offset;

            for (int i = 0; i < mapInByte.Length; i++)
            {
                if (mapInByte[i] == '2')
                    portalCount++;
                else if (mapInByte[i] == '3')
                    ammoCount++;
            }
        }

        private void GetDimensions(out int portalCount, out int ammoCount)
        {
            portalCount = 0;
            ammoCount = 0;

            var omg = _sourceMap.text.Split('\n');

            Height = omg.Length;
            Width = omg[0].Length - 1;

            foreach (var line in omg)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '2')
                    {
                        portalCount++;
                    }

                    if (line[i] == '3')
                    {
                        ammoCount++;
                    }
                }
            }
        }

        private void ConvertMap(TextAsset source, BufferArray<byte> walkable, BufferArray<Vector3> portals, BufferArray<Vector3> ammos)
        {
            var walkableResult = new byte[Width * Height];
            var portalsResult = new Vector3[portals.Length];
            var ammoResult = new Vector3[ammos.Length];

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