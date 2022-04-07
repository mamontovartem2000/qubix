using ME.ECS;
using Project.Common.Components;
using Project.Core.Features;
using Project.Core.Features.Player.Components;
using Project.Core.Features.SceneBuilder.Components;

namespace Project.Mechanics.Features.CollisionHandler.Systems 
{
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif

    #endregion
    public sealed class HealthCollisionSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private CollisionHandlerFeature _feature;
        
        private Filter _powerUpFilter;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);

            Filter.Create("powerup-filter")
                .With<HealthTag>()
                .Push(ref _powerUpFilter);
        }
        
        void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-RegisterPointCollisionSystem")
                .With<PlayerTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var collectible in _powerUpFilter)
            {
                if ((entity.GetPosition() - collectible.GetPosition()).sqrMagnitude <= SceneUtils.ItemRadius)
                {
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {ApplyTo = entity, Damage = -10f}, ComponentLifetime.NotifyAllSystems);

                    if (collectible.GetParent().Has<Spawned>())
                        collectible.GetParent().Remove<Spawned>();
                    
                    _feature.SpawnVFX(entity.GetPosition(), _feature.HealID, _feature.DefaultTimer);                    
                    collectible.Destroy();
                }
            }   
        }
    }
}