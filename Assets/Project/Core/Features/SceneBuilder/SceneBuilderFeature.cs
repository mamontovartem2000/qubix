using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.DataConfigs;
using ME.ECS.Transform;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Core.Features.SceneBuilder
{
#if ECS_COMPILE_IL2CPP_OPTIONS
     [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SceneBuilderFeature : Feature
    {
        [Header("General")]
        [SerializeField] private TextAsset _testFloor;
        [SerializeField] private TextAsset _testObjects;
        [Header("Reworked Links")]
        public ParticleViewSourceBase[] TileViewSources;
        public DataConfig[] PropsConfigs;
        private ViewId[] _tileViewIds, _propsViewIds;
        protected override void OnConstruct() => RegisterViews();
        protected override void OnConstructLate() => PrepareMaps();
        protected override void OnDeconstruct() { }

        private void RegisterViews()
        {
            _tileViewIds = new ViewId[TileViewSources.Length];
            _propsViewIds = new ViewId[PropsConfigs.Length];

            for (var i = 2; i < TileViewSources.Length; i++)
            {
                _tileViewIds[i] = world.RegisterViewSource(TileViewSources[i]);
            }

            for (int i = 1; i < PropsConfigs.Length; i++)
            {
                if (PropsConfigs[i] != null) 
                    _propsViewIds[i] = world.RegisterViewSource(PropsConfigs[i].Read<TileView>().Value);
            }
        }
        private void PrepareMaps()
        {
            GameMapRemoteData floorMap = null;
            GameMapRemoteData objectsMap = null;

            if (NetworkData.FloorMap != null)
            {
                floorMap = new GameMapRemoteData(NetworkData.FloorMap);
                objectsMap = new GameMapRemoteData(NetworkData.ObjectsMap);
            }
            else
            {
                floorMap = new GameMapRemoteData(_testFloor);
                objectsMap = new GameMapRemoteData(_testObjects);
                Debug.Log("Used Local Maps!!!");
            }    

            var height = floorMap.bytes.Length / floorMap.offset;
            var width = floorMap.offset;
            SceneUtils.SetWidthAndHeight(width, height);
            
            DrawMap(floorMap.bytes);
            DrawMapObjects(objectsMap.bytes);

            world.SetSharedData(new MapInitialized());
        }

        private void DrawMap(byte[] tiles)
        {
            var freeMap = PoolArray<byte>.Spawn(tiles.Length);
            var walkableMap = PoolArray<byte>.Spawn(tiles.Length);

            ListCopyable<int> portalMap = new ListCopyable<int>();

            for (int i = 0; i < tiles.Length; i++)
            {
                Entity entity = Entity.Empty;
                
                switch (tiles[i])
                {
                    case 0:
                    {
                        freeMap[i] = 1;
                        walkableMap[i] = 0;
                        break;
                    }
                    case 1:
                    {
                        freeMap[i] = 1;
                        walkableMap[i] = 1;
                        break;
                    }
                    case 2:
                    {
                        entity = new Entity("Platform-Tile");
                        freeMap[i] = 0;
                        walkableMap[i] = 1;
                        break;
                    }
                    case 8:
                    {
                        entity = new Entity("Dispencer-Tile");
                        entity.Set(new DispenserTag {TimerDefault = 8, Timer = 8});
                        freeMap[i] = 1;
                        walkableMap[i] = 1;
                        break;
                    }
                    case 9:
                    {
                        entity = new Entity("Portal-Tile");
                        entity.Set(new PortalDispenserTag { TimerDefault = 0.5f, Timer = 0.5f});
                        freeMap[i] = 1;
                        walkableMap[i] = 1;
                        portalMap.Add(i);
                        break;
                    }
                    case 10:
                    {
                        entity = new Entity("Bridge-Tile");
                        entity.Get<BridgeTile>().Value = true;
                        freeMap[i] = 1;
                        walkableMap[i] = 1;
                        break;
                    }
                    case 11:
                    {
                        entity = new Entity("Bridge-Tile");
                        entity.Get<BridgeTile>().Value = false;
                        freeMap[i] = 1;
                        walkableMap[i] = 1;
                        break;
                    }
                    default:
                    {
                        entity = new Entity("Platform-Tile");
                        freeMap[i] = 0;
                        walkableMap[i] = 1;
                        break;
                    }
                }
                
                if (entity == Entity.Empty) continue;

                entity.InstantiateView(_tileViewIds[tiles[i]]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
            }

            world.GetSharedData<MapComponents>().FreeMap = freeMap;
            world.GetSharedData<MapComponents>().WalkableMap = walkableMap;
            world.GetSharedData<MapComponents>().PortalsMap = portalMap.innerArray;
        }

        private void DrawMapObjects(byte[] mapInBytes)
        {
            ListCopyable<int> redPool = new ListCopyable<int>();
            ListCopyable<int> bluePool = new ListCopyable<int>();

            for (int i = 0; i < mapInBytes.Length; i++)
            {
                var mapElement = mapInBytes[i];

                if (mapElement == 101)
                {
                    redPool.Add(i);
                    continue;
                }

                if (mapElement == 102)
                {
                    bluePool.Add(i);
                    continue;
                }

                if (mapElement == 0) continue;
                if (PropsConfigs[mapElement] == null) continue;

                var entity = new Entity("Prop");

                PropsConfigs[mapElement].Apply(entity);
                entity.InstantiateView(_propsViewIds[mapElement]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
                entity.SetRotation(PropsConfigs[mapElement].Read<Rotation>().value);
                entity.Get<Owner>().Value = entity;
                
                if (entity.Has<DestructibleView>())
                {
                    entity.Get<PlayerHealth>().Value = 100;
                    entity.Set(new DestructibleTag());
                }

                world.GetSharedData<MapComponents>().BlueTeamSpawnPoints = bluePool.innerArray;
                world.GetSharedData<MapComponents>().RedTeamSpawnPoints = redPool.innerArray;

                SceneUtils.ModifyWalkable(SceneUtils.IndexToPosition(i), false);
            }
        }
    }
}