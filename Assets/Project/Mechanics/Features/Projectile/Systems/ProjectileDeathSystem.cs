using ME.ECS;
using Project.Mechanics.Features.CollisionHandler;
using Project.Mechanics.Features.Projectile.Components;

namespace Project.Mechanics.Features.Projectile.Systems 
{
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class ProjectileDeathSystem : ISystemFilter 
    {
        public World world { get; set; }

        private ProjectileFeature _feature;
        private CollisionHandlerFeature _coll;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _coll);
        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-ProjectileDeathSystem")
                .With<ProjectileShouldDie>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Has<ProjectileType>())
            {
                if(entity.Read<ProjectileType>().Type == ProjectileBase.Rocket)
                {
                    _coll.SpawnVFX(entity.GetPosition(), _coll._rocketId, _coll._deathTimer);
                }
            }
            
            entity.Destroy();
        }
    }
}