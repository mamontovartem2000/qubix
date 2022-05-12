using Dima.Scripts;
using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.Views.Providers;
using Project.Core.Features.SceneBuilder.Components;
using Project.Core.Features.SceneBuilder.Systems;
using System.Collections.Generic;
using Project.Core.Features.GameState.Components;
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
        [SerializeField] private TextAsset _objectsMap;

        [HideInInspector] public ViewId _inPortal, _outPortal;
        public MonoBehaviourViewBase In_PortalEffect;
        public MonoBehaviourViewBase Out_PortalEffect;

        [Header("Tiles")]
        public MineMono MineView;
        public HealthMono HealthView;

        [SerializeField] private List<MapViewElement> _tileList = new List<MapViewElement>();
        [SerializeField] private List<MapViewElement> _objectsList = new List<MapViewElement>();


        private Dictionary<int, ViewId> _tilesView = new Dictionary<int, ViewId>();
        private Dictionary<int, ViewId> _objectsView = new Dictionary<int, ViewId>();


        protected override void OnConstruct()
        {
            _inPortal = world.RegisterViewSource(In_PortalEffect);
            _outPortal = world.RegisterViewSource(Out_PortalEffect);

            RegisterViewsToDictionary();
            // AddSystem<NewHealthDispenserSystem>();
            // AddSystem<SpawnMineSystem>();
            // AddSystem<PortalsSystem>();

            PrepareMap();
            
            world.SetSharedData(new MapInitialized());
        }

        protected override void OnDeconstruct() { }

        private void RegisterViewsToDictionary()
        {
            foreach (var tile in _tileList)
            {
                ViewId view = world.RegisterViewSource(tile.View);
                _tilesView.Add(tile.Key, view);
            }

            foreach (var tile in _objectsList)
            {
                ViewId view = world.RegisterViewSource(tile.View);
                _objectsView.Add(tile.Key, view);
            }
        }

        private void PrepareMap()
        {
            //GameMapRemoteData mapData = ParceUtils.CreateFromJSON<UniversalData<GameMapRemoteData>>(data).data;
            GameMapRemoteData mapData = new GameMapRemoteData(_sourceMap);
            GameMapRemoteData objects = new GameMapRemoteData(_objectsMap);


            int height = mapData.bytes.Length / mapData.offset;
            int width = mapData.offset;
            SceneUtils.SetWidthAndHeight(width, height);

            CreateWalkableMap(height * width, mapData.bytes, out BufferArray<byte> walkableMap);
            world.SetSharedData(new MapComponents { WalkableMap = walkableMap });
            DrawMap(mapData.bytes);
            DrawMapObjects(objects.bytes);
        }

        private void DrawMap(byte[] source)
        {
            for (var i = 0; i < source.Length; i++)
            {
                Entity entity = Entity.Empty;

                switch (source[i])
                {
                    case 1: // Walkable tiles
                    case 2:
                    case 3:
                    case 4:
                    case 11:
                    case 12:
                        {
                            entity = new Entity("Platform-Tile");
                            break;
                        }
                    case 5: // Dispencer tile
                        {
                            entity = new Entity("Dispencer-Tile");
                            entity.Set(new DispenserTag { TimerDefault = 8, Timer = 8 });
                            break;
                        }
                    case 6: // Teleport tile
                        {
                            entity = new Entity("Portal-Tile");
                            entity.Set(new PortalTag());
                            break;
                        }
                    case 7: // Bridge tile
                    case 8:
                        {
                            entity = new Entity("Bridge-Tile");
                            break;
                        }

                }

                if (entity != Entity.Empty)
                {
                    entity.InstantiateView(_tilesView[source[i]]);
                    entity.SetPosition(SceneUtils.IndexToPosition(i));
                }
            }
        }

        private void DrawMapObjects(byte[] bytes)
        {
            for (var i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0) continue;

                Entity entity = new Entity("Map-Object");
                entity.InstantiateView(_objectsView[bytes[i]]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
                TakeTheCell(i);
            }
        }

        private void CreateWalkableMap(int size, byte[] mapInByte, out BufferArray<byte> walkableMap)
        {
            walkableMap = PoolArray<byte>.Spawn(size);

            for (var i = 0; i < mapInByte.Length; i++)
            {
                var number = mapInByte[i];

                if (number == 0 || number == 1)
                    walkableMap[i] = number;
                else
                    walkableMap[i] = 1;

                //  Anything not equal to 0 will be equal to 1
            }
        }

        public void Move(Vector3 currentPos, Vector3 targetPos)
        {
            int moveFrom = SceneUtils.PositionToIndex(currentPos);
            world.GetSharedData<MapComponents>().WalkableMap[moveFrom] = 1;
            TakeTheCell(targetPos);
        }

        public void TakeTheCell(Vector3 targetPos)
        {
            int moveTo = SceneUtils.PositionToIndex(targetPos);
            world.GetSharedData<MapComponents>().WalkableMap[moveTo] = 0;
        }

        public void TakeTheCell(int index)
        {
            world.GetSharedData<MapComponents>().WalkableMap[index] = 0;
        }

        public void ReleaseTheCell(Vector3 currentPos)
        {
            int moveFrom = SceneUtils.PositionToIndex(currentPos);
            world.GetSharedData<MapComponents>().WalkableMap[moveFrom] = 1;
        }
    }
}