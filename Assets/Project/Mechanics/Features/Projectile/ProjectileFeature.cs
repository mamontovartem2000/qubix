using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Common.Components;
using Project.Core.Features.Player.Components;
using Project.Mechanics.Features.Projectile.Systems;
using Project.Mechanics.Features.Projectile.Views;
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
        }

        public void SpawnLinear(Entity gun, int length, float delay)
        {
            for (var i = 1; i < length; i++)
            {
                var entity = new Entity("laser");
                gun.Read<ProjectileConfig>().Value.Apply(entity);
                entity.SetParent(gun);

                entity.SetPosition(gun.GetPosition() + new Vector3(0,0.5f,i/2f));
                
                entity.Get<Linear>().StartDelay = delay * i;
                entity.Get<Linear>().EndDelay = delay * (length - i) ;
                gun.Set(new LinearActive());
            }
        }
    }
}