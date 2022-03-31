using ME.ECS;
using ME.ECS.DataConfigs;
using UnityEngine;

namespace Project.Mechanics.Features.Projectile
{
    #region usage
    using Components;
    using Project.Common.Components;
    using Project.Mechanics.Features.Lifetime.Components;
    using Project.Mechanics.Features.Projectile.Views;
    using Systems;
    using Weapon.Components;

    namespace Projectile.Components { }
    namespace Projectile.Modules { }
    namespace Projectile.Systems { }
    namespace Projectile.Markers { }

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class ProjectileFeature : Feature
    {
        public ViewId BulletId, RocketId, SniperBulletId, FireFlameId, LaserSwordId;
        public BulletMono BulletView, RocketView, SniperBulletView, FireFlameView, LaserSwordPartView;
        public DataConfig AutoRifleBulletConfig, RocketLauncherConfig, SniperRifleConfig, FireFlameConfig, LaserSwordPartConfig;

        protected override void OnConstruct()
        {
            BulletId = world.RegisterViewSource(BulletView);
            RocketId = world.RegisterViewSource(RocketView);
            SniperBulletId = world.RegisterViewSource(SniperBulletView);
            FireFlameId = world.RegisterViewSource(FireFlameView);
            LaserSwordId = world.RegisterViewSource(LaserSwordPartView);
            AddSystem<ProjectileMovemetSystem>();
        }

        protected override void OnDeconstruct() { }
        public void SpawnProjectile(Entity gun, Vector3 direction, WeaponType weapon)
        {
            var entity = new Entity("projectile");
            entity.Set(new ProjectileTag());
            entity.Set(new LifeTimeTag());

            var config = AutoRifleBulletConfig;
            var viewId = BulletId;

            switch (weapon)
            {
                case WeaponType.AutoRifle:
                    {
                        viewId = BulletId;
                        config = AutoRifleBulletConfig;
                        break;
                    }

                case WeaponType.RocketLauncher:
                    {
                        viewId = RocketId;
                        config = RocketLauncherConfig;
                        break;
                    }

                case WeaponType.SniperRifle:
                    {
                        viewId = SniperBulletId;
                        config = SniperRifleConfig;
                        break;
                    }

                case WeaponType.FlameTrower:
                    {
                        viewId = FireFlameId;
                        config = FireFlameConfig;
                        break;
                    }

                case WeaponType.LaserSword:
                    {
                        viewId = LaserSwordId;
                        config = LaserSwordPartConfig;
                        entity.SetParent(gun);
                        entity.Set(new ProjectileIsLaser());
                        break;
                    }
            }

            entity.InstantiateView(viewId);

            entity.SetPosition(gun.GetPosition());
            entity.SetRotation(gun.GetParent().GetRotation());
            config.Apply(entity);
            if(weapon == WeaponType.LaserSword) 
                direction = Vector3.forward;
            entity.Get<ProjectileDirection>().Value = direction;
            var projectileSpeedModified = world.GetRandomRange(-entity.Read<ProjectileSpeedModifier>().Value, entity.Read<ProjectileSpeedModifier>().Value);
            entity.Get<ProjectileSpeed>().Value += projectileSpeedModified;
        }
    }
}