using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;

namespace Project.Features.Weapon.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class NewFiringSystem : ISystemFilter
    {
        public World world { get; set; }
		
        private WeaponFeature _feature;

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
            return Filter.Create("Filter-NewFiringSystem")
                .With<AvatarTag>()
                .With<WeaponEntities>()
                .Without<Stun>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var weapon = ref entity.Get<WeaponEntities>();
			
            if (entity.Owner().Has<LeftWeaponShot>() && !entity.Has<DisarmModifier>())
            {
                weapon.LeftWeapon.Set(new LeftWeaponShot());
                weapon.LeftWeapon.Set(new MeleeActive());
            }
            else
            {
                weapon.LeftWeapon.Remove<LeftWeaponShot>();
                weapon.LeftWeapon.Remove<LinearActive>();
               
            }

            if (entity.Owner().Has<RightWeaponShot>() && !entity.Has<DisarmModifier>())
            {
                weapon.RightWeapon.Set(new RightWeaponShot());
                weapon.RightWeapon.Set(new MeleeActive());
            }
            else
            {
                weapon.RightWeapon.Remove<RightWeaponShot>();
            }
        }
    }
}