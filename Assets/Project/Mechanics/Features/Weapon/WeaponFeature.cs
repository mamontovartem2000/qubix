﻿using ME.ECS;
using Project.Mechanics.Features.Weapon.Systems;

namespace Project.Mechanics.Features.Weapon
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
        [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
        Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
        Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class WeaponFeature : Feature
    {
        protected override void OnConstruct()
        {
            AddSystem<NewSpawnWeaponSystem>();
            
            AddSystem<NewAutomaticFiringSystem>();
            AddSystem<NewLinearFiringSystem>();
            AddSystem<NewMeleeFiringSystem>();
            
            AddSystem<WeaponCooldownSystem>();
            AddSystem<NewLinearReloadSystem>();
            AddSystem<WeaponReloadSystem>();
        }

        protected override void OnDeconstruct() {}
    }
}