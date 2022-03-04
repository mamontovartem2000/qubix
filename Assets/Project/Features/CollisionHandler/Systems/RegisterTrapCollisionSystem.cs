using ME.ECS;
using Project.Features.Player.Components;
using Project.Features.SceneBuilder.Components;

namespace Project.Features.CollisionHandler.Systems {
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

    public sealed class RegisterTrapCollisionSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private CollisionHandlerFeature feature;

        private Filter _trapFilter;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this.feature);
            Filter.Create("trap-filter")
                .With<TrapTag>()
                .Push(ref _trapFilter);
        }
        
        void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-RegisterTrapCollisionSystem")
                .With<PlayerTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var collectible in _trapFilter)
            {
                if (entity.GetPosition() == collectible.GetPosition())
                {
                    if (entity.Has<LastHit>()) entity.Remove<LastHit>();
                    
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {ApplyTo = entity, Damage = 10f}, ComponentLifetime.NotifyAllSystems);
                    
                    collectible.Destroy();
                }
            }   
        }
    
    }
}