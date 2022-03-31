using ME.ECS;
using Project.Common.Components;
using Project.Mechanics.Components;

namespace Project.Mechanics.Features.Weapon.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion
    public sealed class NewWeaponShootingSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private WeaponFeature _feature;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
        }

        void ISystemBase.OnDeconstruct() {}

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-NewWeaponShootingSystem")
                .With<WeaponShot>()
                .Without<Cooldown>()
                .Without<ReloadTime>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            // var spread = entity.Read<WeaponSpread>().Value;
//             var direction = new Vector3(world.GetRandomRange(-spread.x, spread.x), spread.y, world.GetRandomRange(-spread.z, spread.z));
//
//             // var direction = spread;
//
//             entity.Get<WeaponAmmo>().Value--;
//
//             if (entity.Read<WeaponTag>().Hand == WeaponHand.Left)
//             {
//                 direction += _feature.LeftDestinationPoint.GetPosition() - entity.GetPosition();
//                 _projectileFeature.SpawnProjectile(entity, direction, _feature.CurrentLeft);
//             }
//             else if(entity.Read<WeaponTag>().Hand == WeaponHand.Right)
//             {
//                 direction += _feature.RightDestinationPoint.GetPosition() - entity.GetPosition();
//                 _projectileFeature.SpawnProjectile(entity, direction, _feature.CurrentRight);
//             }
//
//
//             entity.Get<WeaponCooldown>().Value = entity.Read<WeaponCooldownDefault>().Value;
//             if (entity.Get<WeaponAmmo>().Value < 1)
//             {
//                 entity.Get<WeaponReloadTime>().Value = entity.Read<WeaponReloadTimeDefault>().Value;
//             }
        }
    }
}