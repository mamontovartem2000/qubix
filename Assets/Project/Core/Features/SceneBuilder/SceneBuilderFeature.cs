using ME.ECS;
using ME.ECS.Collections;
using Project.Core.Features.SceneBuilder.Components;
using Project.Core.Features.SceneBuilder.Systems;
using Project.Core.Features.SceneBuilder.Views;
using Project.Core.Features.SceneBuilder.Views.Health;
using Project.Core.Features.SceneBuilder.Views.Mine;
using Project.Core.Features.SceneBuilder.Views.Portal;
using Project.Core.Features.SceneBuilder.Views.Weapon;
using UnityEngine;

namespace Project.Core.Features.SceneBuilder
{
    #region usage
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
        private int _width, _height;
        private ViewId _tileID, _portalTileID, _ammoTileID;

        private const int EMPTY_TILE = 0, SIMPLE_TILE = 1, PORTAL_TILE = 2, AMMO_TILE = 3;

        public Entity TimerEntity { get; private set; }

        protected override void OnConstruct()
        {
            AddSystem<SpawnHealthSystem>();
            AddSystem<SpawnMineSystem>();
            AddSystem<PortalsSystem>();

            _tileID = world.RegisterViewSource(_tileView);
            _portalTileID = world.RegisterViewSource(_portalView);
            _ammoTileID = world.RegisterViewSource(_ammoTileView);

            PrepareMap();
        }      

        protected override void OnDeconstruct() { }

        private void PrepareMap()
        {
            TimerEntity = new Entity("Init"); //TODO: Перенести на старт игры
        
            //GameMapRemoteData mapData = ParceUtils.CreateFromJSON<UniversalData<GameMapRemoteData>>(data).data;
            GameMapRemoteData mapData = new GameMapRemoteData(_sourceMap);

            _height = mapData.bytes.Length / mapData.offset;
            _width = mapData.offset;
            SceneUtils.SetWidthAndHeight(_width, _height);
                  
            ConvertMap(mapData.bytes, out BufferArray<byte> walkableMap);
            world.SetSharedData(new MapComponents { WalkableMap = walkableMap });
            DrawMap(walkableMap);
        }

        private void DrawMap(BufferArray<byte> source)
        {
            for (var i = 0; i < source.Count; i++)
            {
                Entity entity = Entity.Empty;

                switch (source[i])
                {
                    case SIMPLE_TILE:
                        {
                            entity = new Entity("Platform-Tile");
                            entity.InstantiateView(_tileID);
                            break;
                        }
                    case PORTAL_TILE:
                        {
                            entity = new Entity("Portal-Tile");
                            entity.InstantiateView(_portalTileID);
                            entity.Set(new PortalTag());
                            break;
                        }
                    case AMMO_TILE:
                        {
                            entity = new Entity("Ammo-Tile");
                            entity.InstantiateView(_ammoTileID);
                            entity.Set(new AmmoTileTag { Spawned = false, TimerDefault = 8, Timer = 8 });
                            break;
                        }
                }

                if (entity != Entity.Empty)
                    entity.SetPosition(SceneUtils.IndexToPosition(i));
            }
        }       

        private void ConvertMap(byte[] mapInByte, out BufferArray<byte> walkableMap)
        {
            var size = _width * _height;
            walkableMap = PoolArray<byte>.Spawn(size);

            for (int i = 0; i < mapInByte.Length; i++)
            {
                walkableMap[i] = mapInByte[i];
            }
        }

        public void Move(Vector3 currentPos, Vector3 targetPos)
        {
            int moveFrom = SceneUtils.PositionToIndex(currentPos);
            world.GetSharedData<MapComponents>().WalkableMap[moveFrom] = SIMPLE_TILE;
            TakeTheCell(targetPos);
        }

        public void TakeTheCell(Vector3 targetPos)
        {
            int moveTo = SceneUtils.PositionToIndex(targetPos);
            world.GetSharedData<MapComponents>().WalkableMap[moveTo] = EMPTY_TILE;
        }

        public void ReleaseTheCell(Vector3 currentPos)
        {
            int moveFrom = SceneUtils.PositionToIndex(currentPos);
            world.GetSharedData<MapComponents>().WalkableMap[moveFrom] = SIMPLE_TILE;
        }
    }
}