using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.Weapon;
using UnityEngine;

namespace Project.Mechanics.Features.Weapon.Systems
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
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var weapon = ref entity.Get<WeaponEntities>();
			
            if(entity.Has<Stun>()) return;

            if (entity.Owner().Has<LeftWeaponShot>())
            {
                weapon.LeftWeapon.Set(new LeftWeaponShot());
                weapon.LeftWeapon.Set(new MeleeActive());
            }
            else
            {
                weapon.LeftWeapon.Remove<LeftWeaponShot>();
                weapon.LeftWeapon.Remove<LinearActive>();
            }

            if (entity.Owner().Has<RightWeaponShot>())
            {
                weapon.RightWeapon.Set(new RightWeaponShot());
            }
            else
            {
                weapon.RightWeapon.Remove<RightWeaponShot>();
            }
        }
    }
}