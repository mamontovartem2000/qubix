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

        public void SpawnLinear(Entity gun, Vector3 direction)
        {
            for (int i = 0; i < 8; i++)
            {
                var entity = new Entity("laser");
                gun.Read<ProjectileConfig>().Value.Apply(entity);

                entity.SetParent(gun);

                var newFace = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), Mathf.Abs(direction.z)) * i;

                if (direction.x > direction.z)
                {
                    newFace = direction * i;
                    
                }
                else
                {
                    newFace = direction * i;
                }
                // newFace = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), Mathf.Abs(direction.z)) * i;
                Debug.Log($"origin: {direction}, multiplied: {newFace}, added: {entity.GetPosition() + newFace}");

                entity.SetLocalPosition(entity.GetPosition() + newFace);

                var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
                entity.InstantiateView(view);

                entity.Get<Linear>().Value = gun;
                gun.Set(new LinearActive());
            }
        }
    }
}