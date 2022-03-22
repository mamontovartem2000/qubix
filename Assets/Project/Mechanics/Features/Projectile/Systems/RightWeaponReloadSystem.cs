using ME.ECS;
using Project.Core.Features.Events;
using Project.Core.Features.Player.Components;

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
    public sealed class RightWeaponReloadSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private ProjectileFeature _feature;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-RightWeaponReloadSystem")
                .With<RightWeapon>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Read<RightWeapon>().Count > 0) return;

            world.GetFeature<EventsFeature>().RightWeaponDepleted.Execute(entity);
            entity.Remove<RightWeapon>();
        }
    }
}