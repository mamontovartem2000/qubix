using ME.ECS;
using Project.Common.Components;
using Project.Features.Events;

namespace Project.Features.Weapon.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class RefreshLinearUISystem : ISystemFilter
    {
        public World world { get; set; }
        
        private WeaponFeature _feature;
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
            return Filter.Create("Filter-NewRefreshAmmoUISystem")
                .With<LinearWeapon>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var ammo = entity.Read<AmmoCapacity>().Value;
            var max = entity.Read<AmmoCapacityDefault>().Value;

            if (ammo != max)
            {
                world.GetFeature<EventsFeature>().leftWeaponFired.Execute(entity.Get<Owner>().Value);
            }
            else
            {
                entity.Set(new LinearFull());
                world.GetFeature<EventsFeature>().leftWeaponFired.Execute(entity.Get<Owner>().Value);
            }
        }
    }
}