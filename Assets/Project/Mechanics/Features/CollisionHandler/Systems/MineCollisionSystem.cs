using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features;
using Project.Core.Features.Player.Components;
using Project.Core.Features.SceneBuilder.Components;
using Project.Mechanics.Features.CollisionHandler.Components;

namespace Project.Mechanics.Features.CollisionHandler.Systems {
    #region usage

    

    #pragma warning disable
#pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif

    #endregion

    public sealed class MineCollisionSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private CollisionHandlerFeature _feature;
        private Filter _trapFilter;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
            Filter.Create("trap-filter")
                .With<MineTag>()
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
                if ((entity.GetPosition() - collectible.GetPosition()).sqrMagnitude <= SceneUtils.ItemRadius)
                {
                    if (entity.Has<LastHit>()) entity.Remove<LastHit>();
                    
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {ApplyTo = entity, Damage = 25f}, ComponentLifetime.NotifyAllSystems);

                    _feature.SpawnVFX(entity.GetPosition(), _feature.ExplosionID, _feature.DefaultTimer);                    
                    collectible.Destroy();
                }
            }   
        }
    
    }
}