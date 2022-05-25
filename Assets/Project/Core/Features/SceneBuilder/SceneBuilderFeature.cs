using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.Views.Providers;
using ME.ECS.DataConfigs;
using ME.ECS.Transform;
using Project.Common.Components;
using UnityEngine;
using Project.Modules.Network;

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
        [SerializeField] private TextAsset _colizei;
        [SerializeField] private TextAsset _colizei_obj;
        [SerializeField] private TextAsset _neon;
        [SerializeField] private TextAsset _neon_obj;
        [Header("Reworked Links")]
        public ParticleViewSourceBase[] TileViewSources;
        public DataConfig[] PropsConfigs;
        private ViewId[] _tileViewIds, _propsViewIds;
        protected override void OnConstruct()
        {
            RegisterViews();
            world.SetSharedData(new MapInitialized());
        }
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
            //GameMapRemoteData mapData = ParceUtils.CreateFromJSON<UniversalData<GameMapRemoteData>>(data).data;
            GameMapRemoteData mapData = null;
            TextAsset objData = null;

            if (NetworkData.Info.map_id == 1)
            {
                mapData = new GameMapRemoteData(_colizei);
                objData = _colizei_obj;
                MapChanger.Changer.ChangeMap(Maps.Coliseum);
            }
            else if (NetworkData.Info.map_id == 2)
            {
                mapData = new GameMapRemoteData(_neon);
                objData = _neon_obj;
                MapChanger.Changer.ChangeMap(Maps.Neon);
            }

            var height = mapData.bytes.Length / mapData.offset;
            var width = mapData.offset;
            SceneUtils.SetWidthAndHeight(width, height);

            world.SetSharedData(new MapComponents
            {
                WalkableMap = CreateSharedMap(height * width, mapData.bytes, MapType.Walkable),
                MineMap = CreateSharedMap(height * width,mapData.bytes, MapType.Mine),
                PortalsMap = CreateSharedMap(height * width, mapData.bytes, MapType.Portal),
                SpawnPoints = CreateSharedMap(height * width, mapData.bytes, MapType.SpawnPoint)
            });
            
            DrawMap(mapData.bytes);

            if (objData != null)
            {
                var objects = new GameMapRemoteData(objData);
                DrawMapObjects(objects.bytes);
            }
        }
        private void DrawMap(byte[] s)
        {
            var i = -1;
            foreach (var b in s)
            {
                Entity entity = Entity.Empty;
                i++;
                
                switch (b)
                {
                    case 0:
                    case 1:
                    {
                        break;
                    }
                    case 8: // Dispencer tile
                    {
                        entity = new Entity("Dispencer-Tile");
                        entity.Set(new DispenserTag {TimerDefault = 8, Timer = 8});
                        break;
                    }
                    case 9: // Teleport tile
                    {
                        entity = new Entity("Portal-Tile");
                        entity.Set(new PortalDispenserTag { TimerDefault = 0.5f, Timer = 0.5f});
                        break;
                    }
                    case 10: // Bridge tile
                    {
                        entity = new Entity("Bridge-Tile");
                        entity.Get<BridgeTile>().Value = true;
                        break;
                    }
                    case 11:
                    {
                        entity = new Entity("Bridge-Tile");
                        entity.Get<BridgeTile>().Value = false;
                        break;
                    }
                    default:
                    {
                        entity = new Entity("Platform-Tile");
                        break;
                    }
                }
                
                if (entity == Entity.Empty) continue;

                entity.InstantiateView(_tileViewIds[b]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
            }
        }
        private void DrawMapObjects(byte[] mapInBytes)
        {
            var i = -1;
            
            foreach (var mapElement in mapInBytes)
            {
                i++;
                if (mapElement == 35) 
                {
                    Debug.Log(SceneUtils.IndexToPosition(i));
                    continue;
                }
                if (mapElement == 0) continue;
                if (PropsConfigs[mapElement] == null) continue;
                
                var entity = new Entity("Prop");

                PropsConfigs[mapElement].Apply(entity);
                entity.InstantiateView(_propsViewIds[mapElement]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
                if(entity.Has<Pallette>())
                    entity.SetPosition(entity.GetPosition() + new fp3(-0.15,0.2,0.15));
                entity.SetRotation(PropsConfigs[mapElement].Read<Rotation>().value);
                
                entity.Get<Owner>().Value = entity;

                SceneUtils.TakeTheCell(i);
            }
        }
        private BufferArray<byte> CreateSharedMap(int s, byte[] m, MapType t)
        {
            var a = PoolArray<byte>.Spawn(s);

            switch (t)
            {
                case MapType.Walkable:
                    {
                        for (var i = 0; i < m.Length; i++)
                        {
                            a[i] = m[i] == 0 || m[i] == 1 ? m[i] : (byte)1;
                        }

                        break;
                    }
                case MapType.Mine:
                    {
                        for (var i = 0; i < a.Length; i++)
                        {
                            a[i] = 0;
                        }

                        break;
                    }
                case MapType.Portal:
                    {
                        for (var i = 0; i < m.Length; i++)
                        {
                            a[i] = m[i] == 9 ? (byte)1 : (byte)0;
                        }

                        break;
                    }
                case MapType.SpawnPoint:
                    {
                        for (var i = 0; i < m.Length; i++)
                        {
                            a[i] = m[i] == 35 ? (byte)1 : (byte)0;
                        }

                        break;
                    }
            }
            
            return a;
        }
        public enum MapType {Walkable, Mine, Portal, SpawnPoint}
    }
}