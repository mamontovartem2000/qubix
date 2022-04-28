using ME.ECS;
using ME.ECS.Transform;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.SceneBuilder.Components;
using Project.Mechanics.Features.VFX;

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
        private VFXFeature _vfx;
        
        private Filter _powerUpFilter;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _vfx);

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
                .With<AvatarTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var collectible in _powerUpFilter)
            {
                if ((entity.GetPosition() - collectible.GetPosition()).sqrMagnitude <= SceneUtils.ItemRadiusSQR)
                {
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {ApplyTo = entity, Damage = -10f}, ComponentLifetime.NotifyAllSystems);

                    if (collectible.GetParent().Has<Spawned>())
                        collectible.GetParent().Remove<Spawned>();
                    
                    _vfx.SpawnVFX(VFXFeature.VFXType.Heal, collectible.GetPosition());
                    collectible.Destroy();
                }
            }   
        }
    }
}