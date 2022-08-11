using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;

namespace Project.Features.CollisionHandler.Systems 
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SpawnMineSystem : ISystem, IAdvanceTick 
    {        
        public World world { get; set; }

        private CollisionHandlerFeature _feature;
        private Filter _mineFilter;
        private float _spawnDelay;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);

            Filter.Create("mine-filter")
                .With<MineTag>()
                .Push(ref _mineFilter);
        }

        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (_mineFilter.Count >= GameConsts.Scene.Mines.COUNT) return;
            
            _spawnDelay -= deltaTime;
            
            if (_spawnDelay > 0) return;
            
            _feature.SpawnMine();
            _spawnDelay = GameConsts.Scene.Mines.SPAWN_DELAY_DEFAULT;
        }
    }
}