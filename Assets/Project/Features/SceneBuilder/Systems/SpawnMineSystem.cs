using ME.ECS;

namespace Project.Features.SceneBuilder.Systems 
{
    #region usage

    

     #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Project.Utilities;
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

        private SceneBuilderFeature _scene;

        private Filter _mineFilter;
        private ViewId _mineID;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _scene);

            Filter.Create("mine-filter")
                .With<MineTag>()
                .Push(ref _mineFilter);

            _mineID = world.RegisterViewSource(_scene.MineView);
        }
        
        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (_mineFilter.Count < 8)
            {
                var entity = new Entity("Mine");

                entity.Set(new MineTag());
                
                entity.SetPosition(SceneUtils.GetRandomSpawnPosition());
                entity.InstantiateView(_mineID);
            }
        }
        
        void IUpdate.Update(in float deltaTime) {}
    }
}