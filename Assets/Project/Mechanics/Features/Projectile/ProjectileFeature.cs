using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Common.Components;
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
        public BulletMono BulletView;
        public DataConfig BulletConfig;
        private ViewId _bulletID;

        protected override void OnConstruct()
        {
            _bulletID = world.RegisterViewSource(BulletView);
            AddSystem<ProjectileMovementSystem>();
        }

        protected override void OnDeconstruct() {}

        public void SpawnProjectile(Entity gun, Vector3 direction)
        {
            var entity = new Entity("projectile");

            BulletConfig.Apply(entity);

            entity.InstantiateView(_bulletID);

            entity.SetPosition(gun.GetPosition());
            entity.SetRotation(gun.GetParent().GetRotation());

            entity.Get<ProjectileDirection>().Value = direction;
        }
    }
}