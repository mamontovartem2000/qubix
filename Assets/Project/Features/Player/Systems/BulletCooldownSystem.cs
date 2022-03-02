using ME.ECS;
using Project.Features.Projectile.Components;

namespace Project.Features.Player.Systems 
{
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
    public sealed class BulletCooldownSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private PlayerFeature feature;
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this.feature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-FiringCooldownSystem")
                .With<BulletCooldown>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Read<BulletCooldown>().Cooldown - deltaTime > 0)
            {
                entity.Get<BulletCooldown>().Cooldown -= deltaTime;
            }
            else
            {
                entity.Remove<BulletCooldown>();
            }
        }
    }
}