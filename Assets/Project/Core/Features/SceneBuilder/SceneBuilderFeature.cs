using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.Views.Providers;
using ME.ECS.DataConfigs;
using ME.ECS.Transform;
using Project.Common.Components;
using Project.Core.Features.GameState.Components;
using Project.Core.Features.SceneBuilder.Systems;
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
        private const int HEALTH = 0;
        private const int MINE = 1;
        
        [Header("General")]
        [SerializeField] private TextAsset _sourceMap;
        [SerializeField] private TextAsset _objectsMap;

        private ViewId _portal;
        public MonoBehaviourViewBase Portal;
        
        // [HideInInspector] public ViewId _inPortal, _outPortal, _mineId, _healthId;
        // public MonoBehaviourViewBase In_PortalEffect, Out_PortalEffect, MineMono, HealthMono;

        [Header("Reworked Links")]
        // public MonoBehaviourViewBase[] TileViewSources;
        public ParticleViewSourceBase[] TileViewSources;
        public DataConfig[] PropsConfigs;
        public ParticleViewSourceBase[] ObjectViewSources;

        private ViewId[] _tileViewIds, _propsViewIds, _objectViewIds;
        
        protected override void OnConstruct()
        {
            AddSystem<NewHealthDispenserSystem>();
            AddSystem<SpawnMineSystem>();
            AddSystem<PortalsSystem>();

            RegisterViews();
            world.SetSharedData(new MapInitialized());
        }

        protected override void OnConstructLate()
        {
            PrepareMaps();
        }

        protected override void OnDeconstruct() { }

        private ViewId _vfx;
        private void RegisterViews()
        {
            _tileViewIds = new ViewId[TileViewSources.Length];
            _propsViewIds = new ViewId[PropsConfigs.Length];
            _objectViewIds = new ViewId[ObjectViewSources.Length];
            _vfx = world.RegisterViewSource(VFXView);
            _portal = world.RegisterViewSource(Portal);

            for (var i = 2; i < TileViewSources.Length; i++)
            {
                _tileViewIds[i] = world.RegisterViewSource(TileViewSources[i]);
            }

            for (int i = 1; i < PropsConfigs.Length; i++)
            {
                _propsViewIds[i] = world.RegisterViewSource(PropsConfigs[i].Read<TileView>().Value);
            }

            for (int i = 0; i < ObjectViewSources.Length; i++)
            {
                _objectViewIds[i] = world.RegisterViewSource(ObjectViewSources[i]);
            }
            
            // _inPortal = world.RegisterViewSource(In_PortalEffect);
            // _outPortal = world.RegisterViewSource(Out_PortalEffect);
            // _mineId = world.RegisterViewSource(MineMono);
            // _healthId = world.RegisterViewSource(HealthMono);
        }

        private void PrepareMaps()
        {
            //GameMapRemoteData mapData = ParceUtils.CreateFromJSON<UniversalData<GameMapRemoteData>>(data).data;
            var mapData = new GameMapRemoteData(_sourceMap);

            var height = mapData.bytes.Length / mapData.offset;
            var width = mapData.offset;
            
            SceneUtils.SetWidthAndHeight(width, height);
            world.SetSharedData(new MapComponents { WalkableMap = CreateWalkableMap(height * width, mapData.bytes) });

            DrawMap(mapData.bytes);
            
            if (_objectsMap != null)
            {
                var objects = new GameMapRemoteData(_objectsMap);
                DrawMapObjects(objects.bytes);
            }
        }

        public MonoBehaviourViewBase VFXView;

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
                        entity.Set(new PortalTag());
                        var vfx = new Entity("tpvfx");
                        vfx.InstantiateView(_vfx);
                        vfx.SetPosition(SceneUtils.IndexToPosition(i));
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

        private void DrawMapObjects(byte[] s)
        {
            var i = -1;
            
            foreach (var b in s)
            {
                i++;
                if(b == 0) continue;
                
                var entity = new Entity("Prop");
                PropsConfigs[b].Apply(entity);
                entity.InstantiateView(_propsViewIds[b]);
                entity.SetPosition(SceneUtils.IndexToPosition(i));
                if(entity.Has<Pallette>())
                    entity.SetPosition(entity.GetPosition() + new fp3(-0.15,0.2,0.15));
                entity.SetRotation(PropsConfigs[b].Read<Rotation>().value);
                
                entity.Get<Owner>().Value = entity;

                SceneUtils.TakeTheCell(i);
            }
        }

        private BufferArray<byte> CreateWalkableMap(int size, byte[] mapInByte)
        {
            var a = PoolArray<byte>.Spawn(size);

            for (var i = 0; i < mapInByte.Length; i++)
            {
                var number = mapInByte[i];

                if (number == 0 || number == 1)
                    a[i] = number;
                else
                    a[i] = 1;

                //  Anything not equal to 0 will be equal to 1
            }

            return a;
        }

        public void SpawnMine()
        {
            var entity = new Entity("Mine");

            entity.Set(new MineTag());
            entity.SetPosition(SceneUtils.GetRandomSpawnPosition());
            entity.InstantiateView(_objectViewIds[MINE]);
            entity.Set(new CollisionDynamic());
            entity.Get<Owner>().Value = new Entity("ii");
        }

        public Entity SpawnHealth(Entity owner)
        {
            var entity = new Entity("Health");
            entity.Set(new HealthTag());
            entity.InstantiateView(_objectViewIds[HEALTH]);
            entity.Set(new CollisionDynamic());
            entity.Get<ProjectileDirection>().Value = fp3.zero;
            entity.Get<Owner>().Value = owner;
            return entity;
        }

        public void CreatePortal(fp3 invec, fp3 outvec)
        {
            var portIn = new Entity("in");
            portIn.SetPosition(invec);
            portIn.InstantiateView(_portal);
            portIn.Get<LifeTimeLeft>().Value = 3;
            
            var portOut = new Entity("out");
            portOut.SetPosition(outvec);
            portOut.InstantiateView(_portal);
            portOut.Get<LifeTimeLeft>().Value = 3;
        }
    }
}