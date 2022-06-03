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

            if (_testFloor == null)
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
        }

        private void DrawMap(byte[] tiles)
        {
            var i = -1;
            var freeMap = PoolArray<byte>.Spawn(tiles.Length);
            var walkableMap = PoolArray<byte>.Spawn(tiles.Length);
            var portC = 0;

            for (int idx = 0; idx < tiles.Length; idx++)
            {
                if (tiles[idx] == 9) portC++;
            }
            
            var portalMap = PoolArray<int>.Spawn(portC);
            portC = 0;

            foreach (var tile in tiles)
            {
                Entity entity = Entity.Empty;
                i++;
                
                switch (tile)
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
                        portalMap[portC] = i;
                        portC++;
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

                entity.InstantiateView(_tileViewIds[tile]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
            }

            world.GetSharedData<MapComponents>().FreeMap = freeMap;
            world.GetSharedData<MapComponents>().WalkableMap = walkableMap;
            world.GetSharedData<MapComponents>().PortalsMap = portalMap;
        }
        private void DrawMapObjects(byte[] mapInBytes)
        {
            var i = -1;
            
            var redC = 0;
            var blueC = 0;
            
            for (int idx = 0; idx < mapInBytes.Length; idx++)
            {
                if (mapInBytes[idx] == 101) redC++;
                if (mapInBytes[idx] == 102) blueC++;
            }

            var redPool = PoolArray<int>.Spawn(redC);
            var bluePool = PoolArray<int>.Spawn(blueC);
            redC = 0;
            blueC = 0;
            
            foreach (var mapElement in mapInBytes)
            {
                i++;

                //TODO: 35 - xuinya
                if (mapElement == 0 || mapElement == 35) continue;
                if (PropsConfigs[mapElement] == null) continue;
                
                if (mapElement == 101)
                {
                    redPool[redC] = i;
                    redC++;
                    continue;
                }

                if (mapElement == 102)
                {
                    bluePool[blueC] = i;
                    blueC++;
                    continue;
                }
                
                var entity = new Entity("Prop");

                PropsConfigs[mapElement].Apply(entity);
                entity.InstantiateView(_propsViewIds[mapElement]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
                // if (entity.Has<Pallette>())
                // {
                //     entity.SetPosition(entity.GetPosition() + new fp3(-0.15, 0.2, 0.15));
                // }
                entity.SetRotation(PropsConfigs[mapElement].Read<Rotation>().value);
                entity.Get<Owner>().Value = entity;
                
                if (entity.Has<DestructibleView>())
                {
                    entity.Get<PlayerHealth>().Value = 300;
                    entity.Set(new DestructibleTag());
                }

                world.GetSharedData<MapComponents>().BlueTeamSpawnPoints = bluePool;
                world.GetSharedData<MapComponents>().RedTeamSpawnPoints = redPool;

                SceneUtils.ModifyWalkable(SceneUtils.IndexToPosition(i), false);
            }
        }
    }
}