using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Core.Features;
using Project.Core.Features.Player.Components;
using Project.Mechanics.Features.Avatar.Systems;

namespace Project.Mechanics.Features
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
        public MonoBehaviourViewBase AvatarView;
        
        private ViewId _avatarID;
        
        protected override void OnConstruct()
        {
            _avatarID = world.RegisterViewSource(AvatarView);

            AddSystem<SpawnPlayerAvatarSystem>();
        }

        protected override void OnDeconstruct() {}
        
        public void SpawnPlayerAvatar(Entity parent)
        {
            var entity = new Entity("avatar");

            entity.Get<PlayerEntity>().Value = parent;
            parent.Read<AvatarSettings>().PlayerConfig.Apply(entity);

            entity.SetPosition(SceneUtils.GetRandomSpawnPosition());
            entity.Get<PlayerMoveTarget>().Value = entity.GetPosition();
            
            // var leftWeapon = new Entity("leftWeapon");
            // LeftWeaponConfig.Apply(leftWeapon);
            // leftWeapon.SetParent(entity);
            // leftWeapon.SetPosition(entity.GetPosition() - new Vector3(0.35f,0,0));
            //
            // var leftAim = new Entity("leftAim");
            // leftAim.SetParent(leftWeapon);
            // leftAim.SetPosition(leftWeapon.GetPosition() + dir/2);
            // leftWeapon.Get<WeaponAim>().Aim = leftAim;
            //
            // var rightWeapon = new Entity("rightWeapon");
            // RightWeaponConfig.Apply(rightWeapon);
            // rightWeapon.SetParent(entity);
            // rightWeapon.SetPosition(entity.GetPosition() + new Vector3(0.35f,0,0));
            //
            // var rightAim = new Entity("rightAim");
            // rightAim.SetParent(rightWeapon);
            // rightAim.SetPosition(rightWeapon.Has<TrajectoryWeapon>() ? rightWeapon.GetPosition() + (dir+traj)/2 : rightWeapon.GetPosition() + dir/2);
            // rightWeapon.Get<WeaponAim>().Aim = rightAim;
            //
            // world.RemoveSharedData<GamePaused>();
            //
            // entity.InstantiateView(_avatarID);
        }
    }
}