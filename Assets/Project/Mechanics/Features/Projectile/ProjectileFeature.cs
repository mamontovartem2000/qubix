using ME.ECS;
using Project.Common.Components;
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
            AddSystem<LinearLifeSystem>();
            AddSystem<MeleeLifeSystem>();
        }

        protected override void OnDeconstruct() {}

        public void SpawnProjectile(Entity gun, Vector3 direction)
        {
            var entity = new Entity("projectile");
            gun.Read<ProjectileConfig>().Value.Apply(entity);

            var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
            entity.InstantiateView(view);

            entity.SetPosition(gun.GetPosition());
            entity.SetRotation(gun.GetParent().GetRotation());
            entity.Get<ProjectileDirection>().Value = direction;
            entity.Get<ProjectileActive>().Player = gun.GetParent();

            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(gun);
        }

        public void SpawnLinear(Entity gun, int length, float delay)
        {
            for (var i = 1; i < length; i++)
            {
                var entity = new Entity("laser");
                gun.Read<ProjectileConfig>().Value.Apply(entity);
                entity.SetParent(gun);

                entity.SetPosition(gun.GetPosition() + new Vector3(0f, 0f, i / 2f));
                entity.Get<Linear>().StartDelay = delay * i;
                entity.Get<Linear>().EndDelay = delay * (length - i);
                gun.Set(new LinearActive());
            }

            var visual = new Entity("vis");
            visual.SetParent(gun);
            visual.Set(new LinearVisual());
            
            visual.SetPosition(gun.GetPosition());
            var view = world.RegisterViewSource(gun.Read<ProjectileView>().Value);
            visual.InstantiateView(view);
            
            if (gun.Has<LinearFull>())
                gun.Remove<LinearFull>();
        }

        public void SpawnMelee(Entity gun, int length, Vector3 direction)
        {
           
            
            for (var i = 1; i < length; i++)
            {
                var entity = new Entity("melee");
                gun.Read<ProjectileConfig>().Value.Apply(entity);
                
                var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
                entity.InstantiateView(view);
                entity.Get<Melee>().LifeTime = 0.1f;
                entity.SetPosition(gun.GetParent().GetPosition() + (direction * 2) * i);
            }

            if(!gun.Has<LeftWeaponShot>())
                gun.Remove<MeleeActive>();
            
            gun.Get<MeleeDelay>().Value = gun.Read<MeleeDelayDefault>().Value;
            gun.Get<Cooldown>().Value = gun.Read<CooldownDefault>().Value;
            world.GetFeature<EventsFeature>().leftWeaponFired.Execute(gun);
        }
    }
}