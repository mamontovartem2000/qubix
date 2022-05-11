using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Mechanics.Features.Projectile.Systems;
using UnityEngine;
using UnityEngine.EventSystems;

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
            entity.SetLocalPosition(new Vector3(0.15f, 0f, 0.25f));
            entity.SetParent(Entity.Empty);
            
            entity.SetLocalRotation(gun.GetParent().GetRotation());
            
            entity.Get<ProjectileDirection>().Value = direction;
            entity.Get<Owner>().Value = gun.Read<Owner>().Value;

            var damageBase = entity.Read<ProjectileDamage>().Value;
            var damageMod = damageBase * gun.Get<Owner>().Value.Get<AutomaticDamageModifier>().Value;
            var currentDamage = damageBase + damageMod;

            entity.Get<ProjectileDamage>().Value = currentDamage;
            entity.Set(new DamageSource());

            var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
            entity.InstantiateView(view);

            entity.Set(new ProjectileActive());

            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(gun.Get<Owner>().Value);
        }

        public void SpawnLinear(Entity gun, int length, float delay)
        {
            for (var i = 1; i < length; i++)
            {
                var entity = new Entity("laser");
                gun.Read<ProjectileConfig>().Value.Apply(entity);

                entity.SetParent(gun);
                entity.SetLocalPosition(new Vector3(0f,0f, i/2f));

                entity.Get<Owner>().Value = gun.Get<Owner>().Value;
                
                entity.Get<Linear>().StartDelay = delay * i;
                entity.Get<Linear>().EndDelay = delay * (length - i);
                
                var damageBase = entity.Read<ProjectileDamage>().Value;
                var damageMod = damageBase * gun.Get<Owner>().Value.Get<LinearDamageModifier>().Value;
                var currentDamage = damageBase + damageMod;

                entity.Get<ProjectileDamage>().Value = currentDamage;
                entity.Set(new DamageSource());
                                
                gun.Set(new LinearActive());
                entity.Set(new DamageSource());
            }

            var visual = new Entity("vis");
            visual.SetParent(gun);
            visual.Set(new LinearVisual());
            
            visual.SetLocalPosition(new Vector3(-0.15f,0f, 0.5f));
            
            var view = world.RegisterViewSource(gun.Read<ProjectileView>().Value);
            visual.InstantiateView(view);
            
            if (gun.Has<LinearFull>())
                gun.Remove<LinearFull>();
        }

        public void SpawnMelee(Entity gun, Vector3 position)
        {
            var entity = new Entity("melee");
            gun.Read<ProjectileConfig>().Value.Apply(entity);
            entity.SetLocalPosition(position);

            var damageBase = entity.Read<ProjectileDamage>().Value;
            var damageMod = damageBase * gun.Get<Owner>().Value.Get<MeleeDamageModifier>().Value;
            var currentDamage = damageBase + damageMod;
            
            entity.Get<ProjectileDamage>().Value = currentDamage;
            entity.Get<Owner>().Value = gun.Read<Owner>().Value;
            entity.Set(new DamageSource(), ComponentLifetime.NotifyAllSystems);
            
            var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
            entity.InstantiateView(view);
            
            entity.SetParent(Entity.Empty);
            world.GetFeature<EventsFeature>().leftWeaponFired.Execute(gun.Get<Owner>().Value);
        }
    }
}