using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;

namespace Project.Features.Projectile.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class ShengbiaoProjectileMovementSystem : ISystemFilter {
        
        private ProjectileFeature feature;
        
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
            
            return Filter.Create("Filter-ShengbiaoProjectileMovementSystem")
                .With<ShengbiaoProjectile>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var shengbiaoDamageSpot = entity.Owner().Avatar().Read<WeaponEntities>().RightWeapon.Read<ShengbiaoDamageSpot>().Value;
            
            entity.SetPosition(shengbiaoDamageSpot.GetPosition());

            if (shengbiaoDamageSpot.Read<ProjectileParent>().Speed < 0f && shengbiaoDamageSpot.GetLocalPosition().z <= 0f)
            {
                entity.Destroy();
            }
        }
    }
}