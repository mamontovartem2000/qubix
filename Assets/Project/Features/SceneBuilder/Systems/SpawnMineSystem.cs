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

    public sealed class SpawnMineSystem : ISystem, IAdvanceTick, IUpdate 
    {        
        public World world { get; set; }

        private SceneBuilderFeature _feature;

        private Filter _mineFilter;
        private ViewId _mineID;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);

            Filter.Create("mine-filter")
                .With<MineTag>()
                .Push(ref _mineFilter);

            _mineID = world.RegisterViewSource(_feature.MineView);
        }
        
        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (_mineFilter.Count < 8)
            {
                var entity = new Entity("Mine");

                entity.Set(new MineTag());
                
                entity.SetPosition(_feature.GetRandomSpawnPosition());
                entity.InstantiateView(_mineID);
            }
        }
        
        void IUpdate.Update(in float deltaTime) {}
    }
}