using ME.ECS;

namespace Project.Features.SceneBuilder.Systems {
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
    public sealed class SpawnHealthSystem : ISystem, IAdvanceTick, IUpdate 
    {
        public World world { get; set; }
        
        private SceneBuilderFeature _feature;

        private Filter _healthFilter;
        private ViewId _healthID;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);

            Filter.Create("health-filter")
                .With<HealPoweUpTag>()
                .Push(ref _healthFilter);

            _healthID = world.RegisterViewSource(_feature.HealthView);
        }
        
        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (_healthFilter.Count < 4)
            {
                var entity = new Entity("Healing");

                entity.Set(new HealPoweUpTag());      
                
                entity.SetPosition(_feature.GetRandomSpawnPosition());
                entity.InstantiateView(_healthID);
            }
        }
        
        void IUpdate.Update(in float deltaTime) {}
    }
}