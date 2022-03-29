using ME.ECS;
using ME.ECS.DataConfigs;
using UnityEngine;

namespace Project.Mechanics.Features.Weapon
{
    using Project.Common.Components;
    #region usage
    using Project.Core.Features.Player;
    using Project.Mechanics.Features.Weapon.Components;
    using Project.Mechanics.Features.Weapon.Systems;
    using Project.Mechanics.Features.Weapon.Views;
    using System;

    namespace Weapon.Components { }
    namespace Weapon.Modules { }
    namespace Weapon.Systems { }
    namespace Weapon.Markers { }

#if ECS_COMPILE_IL2CPP_OPTIONS
        [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
        Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
        Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class WeaponFeature : Feature
    {
        public WeaponMono AutoRifleView, RocketLauncherView, SniperRifleView, FireThrowerView, LaserSwordView;
        public DataConfig AutoRifleConfig, RocketLauncherConfig, SniperRifleConfig, FireThrowerConfig, LaserSwordConfig;
        public WeaponType CurrentLeft = WeaponType.AutoRifle, CurrentRight = WeaponType.RocketLancher;
        public Entity LeftDestinationPoint, RightDestinationPoint;
        private bool _left = true;

        protected override void OnConstruct()
        {           
            AddSystem<WeaponShootingSystem>();
            AddSystem<WeaponCooldownSystem>();
            AddSystem<WeaponReloadSystem>();
            AddSystem<SpawnPlayerWeapon>();
        }

        protected override void OnDeconstruct() { }

        public void ConstructWeapon(Entity parent, Entity weapon, WeaponType weaponType, float xPos)
        {
            if (_left)
            {
                weapon.Get<WeaponTag>().Hand = WeaponHand.Left;
                LeftDestinationPoint = new Entity("destinationPoint");
                _left = false;
                LeftDestinationPoint.SetPosition(Vector3.forward + weapon.GetPosition());
                LeftDestinationPoint.SetParent(weapon);
            }
            else
            {
                weapon.Get<WeaponTag>().Hand = WeaponHand.Right;
                RightDestinationPoint = new Entity("destinationPoint");
                RightDestinationPoint.SetPosition(Vector3.forward + weapon.GetPosition());
                RightDestinationPoint.SetParent(weapon);
            }

            var viewId = AutoRifleView;
            var config = AutoRifleConfig;

            switch (weaponType)
            {
                case WeaponType.AutoRifle:
                    {
                        viewId = AutoRifleView;
                        config = AutoRifleConfig;
                        break;
                    }

                case WeaponType.RocketLancher:
                    {
                        viewId = RocketLauncherView;
                        config = RocketLauncherConfig;
                        break;
                    }

                case WeaponType.SniperRifle:
                    {
                        viewId = SniperRifleView;
                        config = SniperRifleConfig;
                        break;
                    }

                case WeaponType.FlameTrower:
                    {
                        viewId = FireThrowerView;
                        config = FireThrowerConfig;
                        break;
                    }

                case WeaponType.LaserSword:
                    {
                        viewId = LaserSwordView;
                        config = LaserSwordConfig;
                        break;
                    }
            }

            weapon.SetParent(parent);
            var weaponView = world.RegisterViewSource(viewId);
            weapon.InstantiateView(weaponView);
            weapon.SetLocalPosition(new Vector3(xPos, 0, 0));
            weapon.SetRotation(Quaternion.LookRotation(Vector3.forward));

            config.Apply(weapon);
            weapon.Get<WeaponAmmo>().Value = weapon.Read<WeaponAmmoDefault>().Value;
        }
    }
}