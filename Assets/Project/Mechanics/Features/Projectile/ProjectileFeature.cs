using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.Events;
using Project.Mechanics.Features.Projectile.Systems;
using UnityEngine;

namespace Project.Mechanics.Features.Projectile
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
            
            var damageBase = entity.Read<ProjectileDamage>().Value;
            var damageMod = damageBase * gun.Get<Owner>().Value.Get<AutomaticDamageModifier>().Value;
            var currentDamage = damageBase + damageMod;

            entity.Get<ProjectileDamage>().Value = currentDamage;
            entity.Set(new DamageSource());

            var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
            entity.InstantiateView(view);
            
            var sound = new Entity("sound");
            sound.SetPosition(gun.GetPosition());
			// sound.Get<SoundEffect>() = gun.Get<SoundEffect>();
			// sound.Set(new SoundPlay());

            entity.Set(new ProjectileActive());

            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(gun.Get<Owner>().Value);
        }

        public void SpawnLinear(Entity gun, int length, float delay)
        {
            for (int i = 1; i < length; i++)
            {
                var entity = new Entity("linear");
                gun.Read<ProjectileConfig>().Value.Apply(entity);

                var owner = entity.Get<Owner>().Value = gun.Read<Owner>().Value;
                
                entity.Get<Linear>().StartDelay = delay * i;
                entity.Get<Linear>().EndDelay = delay * (length - i);
                
                var damageBase = entity.Read<ProjectileDamage>().Value;
                var damageMod = damageBase * owner.Read<PlayerAvatar>().Value.Read<LinearPowerModifier>().Damage;
                var currentDamage = damageMod + damageBase;
                
                entity.Get<ProjectileDamage>().Value = currentDamage;
                gun.Set(new LinearActive());
                entity.Get<LinearIndex>().Value = i;
                
                var visual = new Entity("vis");
                visual.SetParent(gun);
                visual.Set(new LinearVisual());
                
                visual.SetLocalPosition(new Vector3(-0.15f,0f, 0.5f));
                visual.SetLocalRotation(gun.GetLocalRotation());
                if (owner.Read<PlayerAvatar>().Value.Has<LinearPowerModifier>())
                {
                    visual.InstantiateView(world.RegisterViewSource(gun.Read<ProjectileAlternativeView>().Value));
                }
                else
                {
                    visual.InstantiateView(world.RegisterViewSource(gun.Read<ProjectileView>().Value));
                }
            }
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