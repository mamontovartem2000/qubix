using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.PostLogicTick;
using Project.Features.PostLogicTick.Systems;
using Project.Features.Projectile.Systems;
using UnityEngine;

namespace Project.Features.Projectile
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

    public sealed class ProjectileFeature : Feature
    {
        protected override void OnConstruct()
        {
            //Bullet movement 
            AddSystem<TrajectoryMovementSystem>();
            AddSystem<BoomerangMovementSystem>();
            AddSystem<ProjectileMovementSystem>();
            AddSystem<ShengbiaoDamageSpotMovement>();
            AddSystem<ShengbiaoProjectileMovementSystem>();
        }

        protected override void OnDeconstruct() {}

        public void SpawnProjectile(Entity gun)
        {
            var entity = new Entity("projectile");
            gun.Read<ProjectileConfig>().Value.Apply(entity);
            
            entity.SetPosition(gun.GetPosition());
            gun.Get<WeaponAim>().Value.SetLocalPosition(Vector3.forward);
            entity.Get<Owner>().Value = gun.Read<Owner>().Value;
            entity.Set(new ProjectileActive());
            entity.Get<ProjectileDirection>().Value = gun.Read<WeaponAim>().Value.GetPosition() - gun.GetPosition();

            gun.Get<SpawnBullet>().LastBullet = entity;

            if (gun.Has<ShengbiaoWeapon>())
            {
                gun.Get<ShengbiaoShot>();
                gun.Get<ShengbiaoWeapon>().MoveRatio = Consts.Weapons.Shengbiao.VISUAL_SPEED_RATIO;
                gun.Read<ShengbiaoDamageSpot>().Value.Get<ProjectileParent>().Speed = Consts.Weapons.Shengbiao.SPEED;
                gun.Read<ShengbiaoDamageSpot>().Value.Get<ProjectileParent>().Bullet = entity;

                entity.Get<LifeTimeLeft>().Value = gun.Read<ReloadTimeDefault>().Value;
            }

            var view = world.RegisterViewSource(entity.Read<ViewModel>().Value);
            entity.InstantiateView(view);
            
            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(gun.Get<Owner>().Value);
        }

        public void SpawnShotgunBullet(Entity gun)
        {
            for (var i = 0; i < gun.Read<Shotgun>().AmmoCount; i++)
            {
                var directionAngle = 0.5f;
                var directionStep = ((i * 1f)  / (gun.Read<Shotgun>().AmmoCount - 1)) * directionAngle;
                var aimPoint = gun.Read<WeaponAim>().Value;
                var entity = new Entity("projectile");
                gun.Read<ProjectileConfig>().Value.Apply(entity);
                
                gun.Get<WeaponAim>().Value.SetLocalPosition(Vector3.forward);
                aimPoint.SetLocalPosition(aimPoint.GetLocalPosition() + new Vector3(directionStep - directionAngle * directionAngle, 0, 0));
                entity.SetPosition(gun.GetPosition());
                entity.Get<ProjectileDirection>().Value = aimPoint.GetPosition() - gun.GetPosition();
                entity.Get<Owner>().Value = gun.Read<Owner>().Value;
                entity.Set(new ProjectileActive());

                var view = world.RegisterViewSource(entity.Read<ViewModel>().Value);
                entity.InstantiateView(view);
                
                if (gun.Has<ModifierConfig>()) gun.Read<ModifierConfig>().Value.Apply(entity);
            }

            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(gun.Get<Owner>().Value);
        }

        public void SpawnLinear(Entity gun, int length, float delay)
        {
            for (int i = 1; i < length; i++)
            {
                var entity = new Entity("linear");
                gun.Read<ProjectileConfig>().Value.Apply(entity);
                entity.Set(new Linear());
                entity.Set(new ProjectileActive());
                
                entity.Get<Owner>().Value = gun.Owner();
                var currentDamage = gun.Read<LinearPowerModifier>().Damage;
                
                entity.Get<ProjectileDamage>().Value = currentDamage;
                gun.Set(new LinearActive());
                entity.Get<LinearIndex>().Value = i;
            }
            
            var visual = new Entity("vis");
            visual.SetParent(gun);
            visual.Set(new LinearVisual());
            
            SoundUtils.PlaySound(gun);

            visual.SetLocalPosition(new Vector3(-0.15f,0f, 0.5f));
            visual.SetLocalRotation(gun.GetLocalRotation());

            visual.InstantiateView(gun.Read<LinearPowerModifier>().Damage > 1.4f
                ? world.RegisterViewSource(gun.Read<ProjectileConfig>().Value.Read<ProjectileAlternativeView>().Value)
                : world.RegisterViewSource(gun.Read<ProjectileConfig>().Value.Read<ViewModel>().Value));
        }

        public void SpawnMelee(in Entity entity, Entity gun)
        {
            entity.Set(new CollisionDynamic(), ComponentLifetime.NotifyAllSystems);
            
            var bullet = new Entity("meleeBullet");
            gun.Read<ProjectileConfig>().Value.Apply(bullet);
            SoundUtils.PlaySound(gun, gun.Read<SoundPath>().Value);
            
            bullet.Set(new ProjectileActive());
            bullet.Set(new MeleeProjectile());
            bullet.SetPosition(gun.Get<MeleeDamageSpot>().Value.GetPosition());
            bullet.Get<MeleeDamageSpot>().Value = gun.Get<MeleeDamageSpot>().Value;
            bullet.Get<Owner>().Value = gun.Owner();
            
            // Test viewModel for projectile
            // ref readonly var view = ref bullet.Read<ViewModel>().Value;
            // var vId = world.RegisterViewSource(view);
            // bullet.InstantiateView(vId);
            bullet.Get<LifeTimeLeft>().Value = 0.5f;

            gun.Get<ReloadTime>().Value = gun.Read<ReloadTimeDefault>().Value;
        }
    }
}