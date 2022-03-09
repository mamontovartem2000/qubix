using ME.ECS;
using Project.Features.Projectile.Components;

namespace Project.Features.Projectile.Systems 
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
    public sealed class LeftWeaponCooldownSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private PlayerFeature _feature;
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
            return Filter.Create("Filter-FiringCooldownSystem")
                .With<LeftWeaponCooldown>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Read<LeftWeaponCooldown>().Value - deltaTime > 0)
            {
                entity.Get<LeftWeaponCooldown>().Value -= deltaTime;
            }
            else
            {
                entity.Remove<LeftWeaponCooldown>();
            }
        }
    }
}