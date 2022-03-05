using ME.ECS;

namespace Project.Features.SceneBuilder.Systems 
{
    #region usage

    

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion

    public sealed class SpawnRocketSystem : ISystem, IAdvanceTick, IUpdate 
    {
        public World world { get; set; }
        
        private SceneBuilderFeature _feature;
        private Filter _rocketFilter;
        private ViewId _rocketID;
        
        private float _spawnCooldown;
        private float _spawnCooldownDefault = 10f;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);

            Filter.Create("rocket-filter")
                .With<RocketAmmoTag>()
                .Push(ref _rocketFilter);

            _rocketID = world.RegisterViewSource(_feature.RocketAmmoView);
        }
        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if(_rocketFilter.Count > 2) return;

            if (_spawnCooldown + deltaTime < _spawnCooldownDefault)
            {
                _spawnCooldown += deltaTime;
            }
            else
            {
                var entity = new Entity("rocket");
                entity.Set(new RocketAmmoTag());
                
                entity.SetPosition(_feature.GetRandomSpawnPosition());
                entity.InstantiateView(_rocketID);
                
                _spawnCooldown = 0;
            }
            
        }
        
        void IUpdate.Update(in float deltaTime) {}
    }
}