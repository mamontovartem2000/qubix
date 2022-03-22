using ME.ECS;
using Project.Mechanics.Features.Projectile.Components;

namespace Project.Mechanics.Features.Projectile.Systems 
{
    #region usage

    

    #pragma warning disable
#pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class ProjectileMovementSystem : ISystemFilter 
    {
        public World world { get; set; }

        private ProjectileFeature _feature;
        
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
            return Filter.Create("Filter-ProjectileMovementSystem")
                .With<ProjectileTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var direction = entity.Read<ProjectileDirection>().Value;
            var speed = entity.Read<ProjectileSpeed>().Value;

            var newPosition = entity.GetPosition() + direction * speed * deltaTime;
            entity.SetPosition(newPosition);
        }
    }
}