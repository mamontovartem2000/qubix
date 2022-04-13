using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.Player.Components;
using Project.Mechanics.Features.Avatar.Systems;
using UnityEngine;

namespace Project.Mechanics.Features.Avatar
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class AvatarFeature : Feature
    {
        private readonly Vector3 _direction = new Vector3(0f,0f,1f);
        private readonly Vector3 _trajectory = new Vector3(0f, 1f, 0f);
        
        protected override void OnConstruct()
        {
            AddSystem<SpawnPlayerAvatarSystem>();
            AddSystem<PlayerMovementSystem>();
        }

        protected override void OnDeconstruct() {}
        
        public void SpawnPlayerAvatar(Entity parent)
        {
            var entity = new Entity("avatar");
            entity.Get<PlayerEntity>().Value = parent;
            parent.Read<AvatarSettings>().PlayerConfig.Apply(entity);

            var view = world.RegisterViewSource(entity.Read<NeedAvatar>().Value);
            entity.InstantiateView(view);

            ConstructWeapon(parent.Read<AvatarSettings>().LeftWeaponConfig, entity);
            ConstructWeapon(parent.Read<AvatarSettings>().RightWeaponConfig, entity);

            entity.SetPosition(SceneUtils.GetRandomSpawnPosition());
            entity.Get<PlayerMoveTarget>().Value = entity.GetPosition();
            parent.Get<PlayerAvatar>().Value = entity;
        }

        private void ConstructWeapon(DataConfig weaponConfig, Entity parent)
        {
            var weapon = new Entity("weapon");
            weaponConfig.Apply(weapon);
            
            weapon.SetPosition(weapon.Read<WeaponPosition>().Value);
            weapon.SetParent(parent);

            var aim = new Entity("aim");
            aim.SetParent(weapon);
            
            // aim.SetPosition(weapon.Has<TrajectoryWeapon>() ? weapon.GetPosition() + (_direction+_trajectory) : weapon.GetPosition() + _direction);
            aim.SetLocalPosition(weapon.Has<TrajectoryWeapon>() ?_direction+_trajectory: _direction);

            weapon.Get<WeaponAim>().Aim = aim;
            
            var view = world.RegisterViewSource(weapon.Read<NeedWeapon>().View);
            weapon.InstantiateView(view);
        }
    }
}