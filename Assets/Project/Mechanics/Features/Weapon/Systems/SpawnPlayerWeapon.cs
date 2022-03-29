using ME.ECS;

namespace Project.Mechanics.Features.Weapon.Systems
{
    #region usage
#pragma warning disable
    using Components;
    using Markers;
    using Modules;
    using Project.Components;
    using Project.Core.Features.Player;
    using Project.Core.Features.Player.Components;
    using Project.Markers;
    using Project.Modules;
    using Project.Systems;
    using Systems;
    using UnityEngine;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class SpawnPlayerWeapon : ISystemFilter
    {
        public World world { get; set; }
        private WeaponFeature _weapon;
        private PlayerFeature _player;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _weapon);
            this.GetFeature(out _player);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-SpawnPlayerWeapon")
                .With<NeedWeapon>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity == _player.GetActivePlayer())
            {
                var leftWeapon = new Entity("LeftWeapon");
                var rightWeapon = new Entity("RightWeapon");
                float weapon_xPos = 1.5f;

                _weapon.ConstructWeapon(entity, leftWeapon, _weapon.CurrentLeft, -weapon_xPos);
                _weapon.ConstructWeapon(entity, rightWeapon, _weapon.CurrentRight, weapon_xPos);
            }
        }
    }
}