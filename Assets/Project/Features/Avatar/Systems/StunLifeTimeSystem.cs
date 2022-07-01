using ME.ECS;
using Project.Common.Components;

namespace Project.Features.Avatar.Systems
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

    public sealed class StunLifeTimeSystem : ISystemFilter
    {
        public World world { get; set; }
        void ISystemBase.OnConstruct() { }
        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-StunLifeTimeSystem")
            .With<Stun>()
            .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            entity.Get<Stun>().Value -= deltaTime;
            entity.Read<WeaponEntities>().LeftWeapon.Remove<LeftWeaponShot>();
            entity.Read<WeaponEntities>().LeftWeapon.Remove<LinearActive>();
            entity.Read<WeaponEntities>().RightWeapon.Remove<RightWeaponShot>();
            if (entity.Read<Stun>().Value > 0f) return;

            entity.Remove<Stun>();
        }
    }
}