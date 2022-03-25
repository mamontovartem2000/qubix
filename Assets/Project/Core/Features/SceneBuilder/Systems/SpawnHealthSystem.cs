using ME.ECS;
using Project.Core.Features.SceneBuilder.Components;

namespace Project.Core.Features.SceneBuilder.Systems {
    #region usage

    

    #pragma warning disable
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
                
                entity.SetPosition(SceneUtils.GetRandomSpawnPosition());
                entity.InstantiateView(_healthID);
            }
        }
        
        void IUpdate.Update(in float deltaTime) {}
    }
}