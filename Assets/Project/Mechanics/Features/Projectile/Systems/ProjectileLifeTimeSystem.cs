using ME.ECS;
using Project.Mechanics.Features.Projectile.Components;

namespace Project.Mechanics.Features.Projectile.Systems {
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class ProjectileLifeTimeSystem : ISystemFilter 
    {
        private ProjectileFeature _feature;
        public World world { get; set; }
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this._feature);
        }
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-ProjectileLifeTimeSystem")
                .With<ProjectileTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var lifeTime = entity.ReadTimer(0);

            if (lifeTime <= 0)
            {
                entity.Set(new ProjectileShouldDie(), ComponentLifetime.NotifyAllSystems);
            }
        }
    }
}