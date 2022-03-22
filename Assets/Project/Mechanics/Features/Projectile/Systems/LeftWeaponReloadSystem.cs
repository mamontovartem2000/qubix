using ME.ECS;
using Project.Core.Features.Events;
using Project.Core.Features.Player.Components;

namespace Project.Mechanics.Features.Projectile.Systems 
{
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class LeftWeaponReloadSystem : ISystemFilter 
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
            return Filter.Create("Filter-LeftWeaponReloadSystem")
                .With<LeftWeapon>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Read<LeftWeapon>().Ammo > 0) return;

            if (entity.Has<LeftWeaponReload>())
            {
                if (entity.Read<LeftWeaponReload>().Time - deltaTime > 0)
                {
                    entity.Get<LeftWeaponReload>().Time -= deltaTime;
                }
                else
                {
                    entity.Get<LeftWeapon>().Ammo = entity.Get<LeftWeapon>().MaxAmmo;
                    entity.Remove<LeftWeaponReload>();
                    world.GetFeature<EventsFeature>().leftWeaponFired.Execute(entity);
                }
            }
            else
            {
                entity.Get<LeftWeaponReload>().Time = entity.Read<LeftWeapon>().ReloadTime;
                world.GetFeature<EventsFeature>().LeftweaponDepleted.Execute(entity);
            }
        }
    }
}