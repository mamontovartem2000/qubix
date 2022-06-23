using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
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

        public void SpawnProjectile(Entity gun, Vector3 direction)
        {
            var entity = new Entity("projectile");
            gun.Read<ProjectileConfig>().Value.Apply(entity);

            entity.SetParent(gun);
            entity.SetLocalPosition(new Vector3(0f, 0f, 0.35f));
            entity.SetParent(Entity.Empty);
            
            entity.Get<ProjectileDirection>().Value = direction;
            entity.Get<Owner>().Value = gun.Read<Owner>().Value;
            
            if (gun.Has<StunModifier>())
            {
                entity.Set(new StunModifier { Value = 1 });
            }

            if (gun.Has<EMPModifier>())
            {
                entity.Set(new EMPModifier { LifeTime = gun.Read<EMPModifier>().LifeTime });
            }
            var currentDamage = entity.Read<ProjectileDamage>().Value;

            entity.Get<ProjectileDamage>().Value = currentDamage;
            entity.Set(new DamageSource());

            var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
            entity.InstantiateView(view);

            entity.Set(new ProjectileActive());

            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(gun.Get<Owner>().Value);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void  SpawnLinear(Entity gun, int length, float delay)
        {
            for (int i = 1; i < length; i++)
            {
                var entity = new Entity("linear");
                gun.Read<ProjectileConfig>().Value.Apply(entity);
                
                entity.Get<Linear>().StartDelay = delay * i;
                entity.Get<Linear>().EndDelay = delay * (length - i);
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

            visual.InstantiateView(gun.Read<LinearPowerModifier>().Damage > 1.3f
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