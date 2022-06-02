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
            GameMapRemoteData floorMap = null;
            GameMapRemoteData objectsMap = null;
            TextAsset objData = null;

            if (NetworkData.Info.map_id == 1)
            {
                floorMap = new GameMapRemoteData(_colizei);
                objData = _colizei_obj;
                MapChanger.Changer.ChangeMap(Maps.Coliseum);
            }
            else if (NetworkData.Info.map_id == 2)
            {
                floorMap = new GameMapRemoteData(_neon);
                objData = _neon_obj;
                MapChanger.Changer.ChangeMap(Maps.Neon);
            }

            var height = floorMap.bytes.Length / floorMap.offset;
            var width = floorMap.offset;
            SceneUtils.SetWidthAndHeight(width, height);
            
            BufferArray<int> redPoints = new BufferArray<int>();
            BufferArray<int> bluePoints = new BufferArray<int>();

            if (objData != null)
            {
                objectsMap = new GameMapRemoteData(objData);
                GetSpawnPointsPositions(objectsMap.bytes, out redPoints, out bluePoints);
            }

            world.SetSharedData(new MapComponents
            {
                WalkableMap = CreateSharedMap(floorMap.bytes, MapType.Walkable),
                MineMap = CreateSharedMap(floorMap.bytes, MapType.Mine),
                PortalsMap = CreateSharedMap(floorMap.bytes, MapType.Portal),
                RedTeamSpawnPoints = redPoints,
                BlueTeamSpawnPoints = bluePoints
            });

            DrawMap(floorMap.bytes);

            if (objectsMap != null)
                DrawMapObjects(objectsMap.bytes);

            var sdfg = world.GetSharedData<MapComponents>();
        }

        private void DrawMap(byte[] tiles)
        {
            var i = -1;
            foreach (var tile in tiles)
            {
                Entity entity = Entity.Empty;
                i++;
                
                switch (tile)
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

                entity.InstantiateView(_tileViewIds[tile]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
            }
        }
        private void DrawMapObjects(byte[] mapInBytes)
        {
            var i = -1;
            
            foreach (var mapElement in mapInBytes)
            {
                i++;

                if (mapElement == 0 || mapElement == 101 || mapElement == 102) continue;
                if (PropsConfigs[mapElement] == null) continue;
                
                var entity = new Entity("Prop");

                PropsConfigs[mapElement].Apply(entity);
                entity.InstantiateView(_propsViewIds[mapElement]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
                if (entity.Has<Pallette>())
                {
                    entity.SetPosition(entity.GetPosition() + new fp3(-0.15,0.2,0.15));
                }
                entity.SetRotation(PropsConfigs[mapElement].Read<Rotation>().value);
                entity.Get<Owner>().Value = entity;
                
                if (entity.Has<DestructibleView>())
                {
                    entity.Get<PlayerHealth>().Value = 300;
                    entity.Set(new DestructibleTag());
                }

                SceneUtils.TakeTheCell(i);
            }
        }
        private BufferArray<byte> CreateSharedMap(byte[] m, MapType t)
        {
            var a = PoolArray<byte>.Spawn(m.Length);

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
                        for (var i = 0; i < m.Length; i++)
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
            }
            
            return a;
        }

        private void GetSpawnPointsPositions(byte[] m, out BufferArray<int> redTeam, out BufferArray<int> blueTeam)
        {
            ListCopyable<int> bufferRed = new ListCopyable<int>();
            ListCopyable<int> bufferBlue = new ListCopyable<int>();

            for (var i = 0; i < m.Length; i++)
            {
                if (m[i] == 101)
                {
                    bufferRed.Add(i);
                }
                else if (m[i] == 102)
                {
                    bufferBlue.Add(i);
                }
            }

            var redPool = PoolArray<int>.Spawn(bufferRed.Count);
            var bluePool = PoolArray<int>.Spawn(bufferBlue.Count);

            for (int i = 0; i < bufferRed.Count; i++)
            {
                redPool[i] = bufferRed[i];
            }

            for (int i = 0; i < bufferBlue.Count; i++)
            {
                bluePool[i] = bufferBlue[i];
            }

            blueTeam = bluePool;
            redTeam = redPool;
        }
        public enum MapType {Walkable, Mine, Portal}
    }
}