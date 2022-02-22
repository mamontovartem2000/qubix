using System.Collections.Generic;
using ME.ECS;
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
        public TextAsset Map;
        public TileView TileView;
        public MineMono MineView;
        public HealthMono HealthView;

        public GlobalEvent HealthCollision;
        public GlobalEvent MineCollision;
        
        private List<Vector3> _walkable = new List<Vector3>();
        //new shit
        public int width;
        private ViewId _tileID, _mineID, _healthID;
        
        protected override void OnConstruct()
        {
            _tileID = world.RegisterViewSource(TileView);
            _mineID = world.RegisterViewSource(MineView);
            _healthID = world.RegisterViewSource(HealthView);

            this.AddSystem<SpawnMineSystem>();
            
            var w = 26;
            var h = 18;

            width = w;
            
            var strings = Map.text.Split(System.Environment.NewLine.ToCharArray());
            var map = PoolArray<byte>.Spawn(w * h);
            for (var i = 0; i < strings.Length; i++)
            {
                if (strings[i] != string.Empty)
                {
                    var pos = JsonUtility.FromJson<TilePosition>(strings[i]).Position;
                    _walkable.Add(pos);
                }
            }
            
            world.SetSharedData(new WalkableCheck{Buffer = map});
            foreach (var pos in _walkable)
            {
                var tile = new Entity("tile");
                tile.InstantiateView(_tileID);
                tile.SetPosition(pos);
            }
        }

        protected override void OnDeconstruct() { }

        public void Move(Entity entity, int from, int to)
        {
            world.GetSharedData<WalkableCheck>().Buffer[from] = 1;
            world.GetSharedData<WalkableCheck>().Buffer[to] = 0;
        }

        public int PosToIndex(Vector3 vec)
        {
            var x = Mathf.RoundToInt(vec.x);
            var y = Mathf.RoundToInt(vec.z);

            return y * width + x;
        }
        
        public bool CheckWalkable(Vector3 pos, Vector3 dir)
        {
            // var fromIndex = PosToIndex(pos);
            var toIndex = PosToIndex(pos + dir);

            return world.ReadSharedData<WalkableCheck>().Buffer[toIndex] == 1;
        }

        public void SpawnMine()
        {
            var entity = new Entity("mine");
            entity.InstantiateView(_mineID);
            entity.SetPosition(_walkable[world.GetRandomRange(0, _walkable.Count)]);
            entity.Set(new MineTag());
        }

        public void SpawnHealth()
        {
            var entity = new Entity("health");
            entity.InstantiateView(_healthID);
            entity.SetPosition(_walkable[world.GetRandomRange(0, _walkable.Count)]);
            entity.Set(new HealthTag());
        }
    }
    
    public class TilePosition
    {
        public Vector3 Position;

        public TilePosition(Vector3 p)
        {
            Position = p;
        }
    }
}