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
    using Project.Common.Components;
    using Systems;
    using UnityEngine;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class SpawnPlayerWeaponSystem : ISystemFilter
    {
        public World world { get; set; }
        private WeaponFeature _weapon;
        private PlayerFeature _player;
        private float _weapon_xPos = -0.5f;
        private bool _left = true;

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

                ConstructWeapon(entity, leftWeapon, _weapon.CurrentLeft);
                ConstructWeapon(entity, rightWeapon, _weapon.CurrentRight);
            }
        }
        public void ConstructWeapon(Entity parent, Entity weapon, WeaponType weaponType)
        {
            if (_left)
            {
                weapon.Get<WeaponTag>().Hand = WeaponHand.Left;
                _weapon.LeftDestinationPoint = new Entity("destinationPoint");
                _left = false;
                _weapon.LeftDestinationPoint.SetPosition(Vector3.forward + weapon.GetPosition());
                _weapon.LeftDestinationPoint.SetParent(weapon);
            }
            else
            {
                weapon.Get<WeaponTag>().Hand = WeaponHand.Right;
                _weapon.RightDestinationPoint = new Entity("destinationPoint");
                _weapon.RightDestinationPoint.SetPosition(Vector3.forward + weapon.GetPosition());
                _weapon.RightDestinationPoint.SetParent(weapon);
                _weapon_xPos *= -1;
            }

            var viewId = _weapon.AutoRifleView;
            var config = _weapon.AutoRifleConfig;

            weapon.Get<WeaponTag>().Gun = weaponType;

            switch (weaponType)
            {
                case WeaponType.AutoRifle:
                    {
                        viewId = _weapon.AutoRifleView;
                        config = _weapon.AutoRifleConfig;
                        break;
                    }

                case WeaponType.RocketLauncher:
                    {
                        viewId = _weapon.RocketLauncherView;
                        config = _weapon.RocketLauncherConfig;
                        break;
                    }

                case WeaponType.SniperRifle:
                    {
                        viewId = _weapon.SniperRifleView;
                        config = _weapon.SniperRifleConfig;
                        break;
                    }

                case WeaponType.FlameTrower:
                    {
                        viewId = _weapon.FlamethrowerView;
                        config = _weapon.FlamethrowerConfig;
                        break;
                    }

                case WeaponType.LaserSword:
                    {
                        viewId = _weapon.LaserSwordView;
                        config = _weapon.LaserSwordConfig;
                        break;
                    }
            }

            weapon.SetParent(parent);
            var weaponView = world.RegisterViewSource(viewId);
            weapon.InstantiateView(weaponView);
            weapon.SetLocalPosition(new Vector3(_weapon_xPos, 0.5f, 0));
            weapon.SetRotation(Quaternion.LookRotation(Vector3.forward));

            config.Apply(weapon);
            weapon.Get<WeaponAmmo>().Value = weapon.Read<WeaponAmmoDefault>().Value;
        }
    }
}