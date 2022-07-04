using ME.ECS;
using Project.Common.Components;
using Project.Features.Events;
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
            AddSystem<ProjectileMovementSystem>();
        }

        protected override void OnDeconstruct() {}

        public void SpawnProjectile(Entity gun)
        {
            var entity = new Entity("projectile");
            gun.Read<ProjectileConfig>().Value.Apply(entity);

            entity.SetPosition(gun.GetPosition());
            entity.Get<ProjectileDirection>().Value = gun.Read<WeaponAim>().Value.GetPosition() - gun.GetPosition();
            entity.Get<Owner>().Value = gun.Read<Owner>().Value;
            entity.Set(new DamageSource());
            entity.Set(new ProjectileActive());

            if (gun.Has<StunModifier>())
            {
                entity.Set(new StunModifier { Value = 1 });
            }

            if (gun.Has<EMPModifier>())
            {
                entity.Set(new EMPModifier { LifeTime = gun.Read<EMPModifier>().LifeTime });
            }

            if (Worlds.current.GetRandomValue() < gun.Read<CriticalHitModifier>().Value)
            {
                entity.Set(new CriticalHit());
            }
            
            var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
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
                entity.Set(new DamageSource());
                entity.Set(new ProjectileActive());

                if (gun.Has<StunModifier>())
                {
                    entity.Set(new StunModifier { Value = 1 });
                }

                if (gun.Has<EMPModifier>())
                {
                    entity.Set(new EMPModifier { LifeTime = gun.Read<EMPModifier>().LifeTime });
                }
                
                if (Worlds.current.GetRandomValue() < gun.Read<CriticalHitModifier>().Value)
                {
                    entity.Set(new CriticalHit());
                }

                var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
                entity.InstantiateView(view);
            }
            
            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(gun.Get<Owner>().Value);
        }

        public void  SpawnLinear(Entity gun, int length, float delay)
        {
            for (int i = 1; i < length; i++)
            {
                var entity = new Entity("linear");
                gun.Read<ProjectileConfig>().Value.Apply(entity);
                entity.Get<Linear>();

                entity.Get<Owner>().Value = gun.Owner();
                var currentDamage = gun.Read<LinearPowerModifier>().Damage;
                
                entity.Get<ProjectileDamage>().Value = currentDamage;
                gun.Set(new LinearActive());
                entity.Get<LinearIndex>().Value = i;
            }
            
            var visual = new Entity("vis");
            visual.SetParent(gun);
            visual.Set(new LinearVisual());
                
            visual.SetLocalPosition(new Vector3(-0.15f,0f, 0.5f));
            visual.SetLocalRotation(gun.GetLocalRotation());

            visual.InstantiateView(gun.Read<LinearPowerModifier>().Damage > 1.4f
                ? world.RegisterViewSource(gun.Read<ProjectileAlternativeView>().Value)
                : world.RegisterViewSource(gun.Read<ProjectileView>().Value));
        }

        public void SpawnMelee(in Entity entity, Entity gun)
        {
            ref readonly var view = ref entity.Read<ProjectileView>().Value;

            var vId = world.RegisterViewSource(view);
            entity.Set(new CollisionDynamic(), ComponentLifetime.NotifyAllSystems);

            var thing = new Entity("thing");
            
            thing.SetPosition(SceneUtils.SafeCheckPosition(entity.GetPosition()));
            thing.InstantiateView(vId);
            thing.Get<LifeTimeLeft>().Value = 2f;

            gun.Get<ReloadTime>().Value = gun.Read<ReloadTimeDefault>().Value;
        }

    }
}