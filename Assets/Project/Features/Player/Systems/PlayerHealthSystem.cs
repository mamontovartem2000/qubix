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
    public sealed class PlayerHealthSystem : ISystemFilter {
        
        private PlayerFeature feature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-PlayerHealthSystem")
                .With<PlayerHealth>()
                .Push();
            
        }

        private bool _isActive;
        private int _count;
        
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var currentHealth = entity.Read<PlayerHealth>().Value;

            if (currentHealth > 0) return;

            var deadBody = new Entity("deadBody");
            deadBody.Set(new DeadBody {ActorID = entity.Read<PlayerTag>().PlayerID, Time = 2f});
            entity.Destroy();

            _count = _isActive ? 1 : 0;
        }
    }
}