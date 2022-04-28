using Dima.Scripts;
using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Core.Features.GameState.Components;
using Project.Core.Features.SceneBuilder.Components;
using Project.Core.Features.SceneBuilder.Systems;
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
        [SerializeField] private TextAsset _sourceMap;

        public MonoBehaviourViewBase PortIn;
        public MonoBehaviourViewBase PortOut;
        public ViewId _in, _out;

        [Header("Tiles")]
         // public TileParticle TileView;
         // public TeleportParticle PortalView;
         // public DispenserParticle DispenserView;
         // public MineParticle MineView;
         // public HealthParticle HealthView;
         
         public TileMono TileView;
         public TeleportMono PortalView;
         public DispenserMono DispenserView;
         public MineMono MineView;
         public HealthMono HealthView;

        private int Width, Height, PortalCount, AmmoCount;

        private ViewId _tileID, _portalTileID, _ammoTileID;

        private const int EMPTY_TILE = 0, SIMPLE_TILE = 1, PORTAL_TILE = 2, AMMO_TILE = 3;

        protected override void OnConstruct()
        {
            AddSystem<NewHealthDispenserSystem>();
            AddSystem<SpawnMineSystem>();
            AddSystem<PortalsSystem>();

            _tileID = world.RegisterViewSource(TileView);
            _portalTileID = world.RegisterViewSource(PortalView);
            _ammoTileID = world.RegisterViewSource(DispenserView);
            _in = world.RegisterViewSource(PortIn);
            _out = world.RegisterViewSource(PortOut);
            
            PrepareMap();
        }

        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder.WithoutShared<GamePaused>();
        }

        protected override void OnDeconstruct() { }

        private void PrepareMap()
        {
            Entity entity = new Entity("init");
            entity.Set(new TestTag());

            GetDimensions();
             SceneUtils.SetWidthAndHeight(Width, Height);

            var size = Width * Height;
            var map = PoolArray<byte>.Spawn(size);
            var portals = PoolArray<Vector3>.Spawn(PortalCount);

            ConvertMap(_sourceMap, map, portals);

            world.SetSharedData(new MapComponents { WalkableMap = map, PortalsMap = portals});

            DrawMap(map);
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
                             // entity.InstantiateView(_tileID);
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
                             entity.Set(new DispenserTag {TimerDefault = 8, Timer = 8 });
                             break;
                         }
                 }

                if (entity != Entity.Empty)
                    entity.SetPosition(SceneUtils.IndexToPosition(i));
            }

            world.SetSharedData(new MapInitialized());
        }    

        private void GetDimensions()
        {
            var omg = _sourceMap.text.Split('\n');

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

        private void ConvertMap(TextAsset source, BufferArray<byte> walkable, BufferArray<Vector3> portals)
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

        public void Move(Vector3 currentPos, Vector3 targetPos)
        {
            int moveFrom = SceneUtils.PositionToIndex(currentPos);
            world.GetSharedData<MapComponents>().WalkableMap[moveFrom] = SIMPLE_TILE;
            TakeTheCell(targetPos);
            ReleaseTheCell(currentPos);
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