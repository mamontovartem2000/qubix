using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.DataConfigs;
using ME.ECS.Transform;
using Project.Common.Components;
using Project.Modules.Network;
using System;
using Project.Common.Utilities;
using UnityEngine;

namespace Project.Features.SceneBuilder
{
#if ECS_COMPILE_IL2CPP_OPTIONS
     [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SceneBuilderFeature : Feature
    {
        [Header("General")] 
        [SerializeField] private TextAsset _colizei;
        [SerializeField] private TextAsset _colizei_obj;
        [SerializeField] private TextAsset _neon;
        [SerializeField] private TextAsset _neon_obj;
        [SerializeField] private TextAsset _neon_flag;
        [SerializeField] private TextAsset _neon_flag_obj;
        [Header("Reworked Links")]
        
        public DataConfigWithId[] TilesConfigs;
        public DataConfigWithId[] PropsConfigs;
        private DictionaryCopyable<int, DataConfig> _tileConfigsDictionary;
        private DictionaryCopyable<int, DataConfig> _propsConfigsDictionary;

        protected override void OnConstruct() => ConfigsToDictionary();
        protected override void OnConstructLate() => PrepareMaps();
        protected override void OnDeconstruct() { }

        private void ConfigsToDictionary()
        {
            _tileConfigsDictionary = new DictionaryCopyable<int, DataConfig>();
            _propsConfigsDictionary = new DictionaryCopyable<int, DataConfig>();

            foreach (var tile in TilesConfigs)
            {
                _tileConfigsDictionary.Add(tile.Id, tile.Config);
            }
            
            foreach (var prop in PropsConfigs)
            {
                _propsConfigsDictionary.Add(prop.Id, prop.Config);
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
                if (NetworkData.Info.map_id == 1)
                {
                    floorMap = new GameMapRemoteData(_colizei);
                    objectsMap = new GameMapRemoteData(_colizei_obj);
                }
                else if (NetworkData.Info.map_id == 2)
                {
                    if (NetworkData.GameMode == GameModes.flagCapture)
                    {
                        floorMap = new GameMapRemoteData(_neon_flag);
                        objectsMap = new GameMapRemoteData(_neon_flag_obj);
                    }
                    else
                    {
                        floorMap = new GameMapRemoteData(_neon);
                        objectsMap = new GameMapRemoteData(_neon_obj);
                    }
                }
                
                //Debug.Log("Used Local Maps!!!");
            }    

            var height = floorMap.bytes.Length / floorMap.offset;
            var width = floorMap.offset;
            SceneUtils.SetWidthAndHeight(width, height);
            
            DrawMap(floorMap.bytes);
            DrawMapObjects(objectsMap.bytes);

            MapChanger.Changer.ChangeMap();
            world.SetSharedData(new MapInitialized());
        }

         private void DrawMap(byte[] tiles)
        {
            var freeMap = PoolArray<byte>.Spawn(tiles.Length);
            var walkableMap = PoolArray<byte>.Spawn(tiles.Length);
            ListCopyable<int> portalMap = new ListCopyable<int>();

            for (int i = 0; i < tiles.Length; i++)
            {
                var tileIndex = tiles[i];
                var config = _tileConfigsDictionary[tileIndex];
                
                var mapsValues = config.Read<FreeAndWalkableMap>();
                freeMap[i] = mapsValues.FreeMapValue;
                walkableMap[i] = mapsValues.WalkableMapValue;
                
                if (tileIndex == 0 || tileIndex == 1) continue;

                if (tileIndex == 9)
                {
                    portalMap.Add(i);
                }
                
                Entity entity = new Entity(config.Read<TileName>().Value);
                config.Apply(entity);
                
                if (entity.Has<GlowTile>())
                {
                    ref var glow = ref entity.Get<GlowTile>();
                    glow.Amount = world.GetRandomRange(glow.AmountRange.x, glow.AmountRange.y);
                }

                var viewId = world.RegisterViewSource(config.Read<TileAlternativeView>().Value);
                entity.InstantiateView(viewId);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
            }
            
            world.GetSharedData<MapComponents>().FreeMap = freeMap;
            world.GetSharedData<MapComponents>().WalkableMap = walkableMap;
            world.GetSharedData<MapComponents>().PortalsMap = portalMap.innerArray;
        }
         
         private void DrawMapObjects(byte[] mapInBytes)
         {
             var redPool = new ListCopyable<int>(); //TODO: if empty, anyway has empty elements and count > 0
             var bluePool = new ListCopyable<int>();

             for (int i = 0; i < mapInBytes.Length; i++)
             {
                 var mapElement = mapInBytes[i];

                 if (mapElement == 0)
                     continue;
                 
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
                 
                 var config = _propsConfigsDictionary[mapElement];
                 var viewId = world.RegisterViewSource(config.Read<TileAlternativeView>().Value);
                 var entity = new Entity("Prop");

                 config.Apply(entity);
                 entity.InstantiateView(viewId);
                 entity.SetPosition(SceneUtils.IndexToPosition(i));
                 entity.SetRotation(config.Read<Rotation>().value);
                 entity.Get<Owner>().Value = entity;
                
                 if (entity.Has<DestructibleView>())
                 {
                     entity.Get<PlayerHealth>().Value = GameConsts.Scene.DESTRUCTUBLE_OBJ_HEALTH;
                     entity.Set(new DestructibleTag());
                 }
                
                 entity.Set(new GlowTile {Direction = false, Amount = world.GetRandomRange(2f, 4f)});

                 world.GetSharedData<MapComponents>().BlueTeamSpawnPoints = bluePool.innerArray;
                 world.GetSharedData<MapComponents>().RedTeamSpawnPoints = redPool.innerArray;

                 SceneUtils.ModifyWalkable(SceneUtils.IndexToPosition(i), false);
             }
         }
    }

    [Serializable]
    public struct DataConfigWithId
    {
        public int Id;
        public DataConfig Config;
    }
}