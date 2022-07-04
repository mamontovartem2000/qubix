using ME.ECS;
using Project.Common.Components;
using Project.Features.Weapon.Systems;

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
            //Firing inputs
            AddSystem<NewFiringSystem>();
            
            //Modifiers reload systems
            AddSystem<EMPModifierBulletRecoverySystem>();
            AddSystem<StunModifierBulletRecoverySystem>();
            AddSystem<FireRateModifierBulletRecoverySystem>();
            
            //Firing systems
            AddSystem<AutomaticFiringSystem>();
            AddSystem<LinearFiringSystem>();
            AddSystem<MeleeFiringSystem>();

            AddSystem<SingleBulletShotSystem>();
            AddSystem<ShotgunFiringSystem>();

            //Reload systems
            AddSystem<CooldownSystem>();
            AddSystem<LinearReloadSystem>();
            AddSystem<WeaponReloadSystem>();

            //UI systems
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