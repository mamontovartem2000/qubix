using ME.ECS;

namespace Project.Features.Player.Systems {
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
    public sealed class RegainScoreSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private PlayerFeature _feature;
        private EventsFeature _events;
        
        private Filter _holderFilter;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _events);
            
            Filter.Create("holder-filter")
                .With<ScoreHolder>()
                .Push(ref _holderFilter);
        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-RegainScoreSystem")
                .With<RegainScore>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if(_holderFilter.Count < 1) return;
            
            foreach (var holder in _holderFilter)
            {
                if (holder.Read<ScoreHolder>().ActorID == entity.Read<PlayerTag>().PlayerID)
                {
                    entity.Set(new PlayerScore {Value = holder.Read<ScoreHolder>().ScoreAmount});
                    holder.Destroy();
                    _events.ScoreChanged.Execute(entity);
                    return;
                }
            }
        }
    }
}