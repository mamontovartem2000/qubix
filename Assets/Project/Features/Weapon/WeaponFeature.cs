using ME.ECS;
using Project.Common.Components;
using Project.Features.Weapon.Systems;
using Project.Mechanics.Features.Weapon.Systems;

namespace Project.Features.Weapon
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
            AddSystem<AutomaticFiringSystem>();
            AddSystem<LinearFiringSystem>();
            AddSystem<MeleeFiringSystem>();
            AddSystem<AmmoCapacityDefaultRestoreSystem>();
            AddSystem<NewFiringSystem>();

            AddSystem<CooldownSystem>();
            AddSystem<LinearReloadSystem>();
            AddSystem<WeaponReloadSystem>();

            AddSystem<RefreshLinearUISystem>();
        }

        protected override void OnDeconstruct() {}

        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder
                .WithoutShared<GamePaused>();
        }
    }
}