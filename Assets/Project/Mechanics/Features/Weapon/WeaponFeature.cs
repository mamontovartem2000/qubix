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
        public WeaponMono AutoRifleView, RocketLauncherView, SniperRifleView, FlamethrowerView, LaserSwordView;
        public DataConfig AutoRifleConfig, RocketLauncherConfig, SniperRifleConfig, FlamethrowerConfig, LaserSwordConfig;
        public WeaponType CurrentLeft = WeaponType.AutoRifle, CurrentRight = WeaponType.RocketLauncher;
        public Entity LeftDestinationPoint, RightDestinationPoint;

        protected override void OnConstruct()
        {           
            AddSystem<WeaponShootingSystem>();
            AddSystem<WeaponCooldownSystem>();
            AddSystem<WeaponReloadSystem>();
            AddSystem<LaserActivateSystem>();
            AddSystem<SpawnPlayerWeaponSystem>();
        }

        protected override void OnDeconstruct() { }
    }
}