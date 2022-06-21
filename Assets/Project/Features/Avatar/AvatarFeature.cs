using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Features.Avatar.Systems;
using Project.Features.Events;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Features.Avatar
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class AvatarFeature : Feature
    {
        public MonoBehaviourViewBase PlayerHealthView;
    
        private readonly Vector3 _direction = new Vector3(0f, 0f, 1f);
        private readonly Vector3 _trajectory = new Vector3(0f, 1f, 0f);
        private ViewId _playerHealth;

        protected override void OnConstruct()
        {
            AddSystem<SpawnPlayerAvatarSystem>();
            AddSystem<ApplyDamageSystem>();
            AddSystem<PlayerHealthSystem>();
            AddSystem<PlayerMovementSystem>();
            AddSystem<BlinkHurtSystem>();
            AddSystem<ShieldApplyDamageSystem>();

            AddSystem<PlaySoundSystem>();
            AddSystem<SlownessAfterTakeDamageSystem>();
            AddSystem<SlownessRemoveSystem>();
            AddSystem<StunLifeTimeSystem>();
            AddSystem<PlayerHealthVisualSystem>();
            AddSystem<BlinkIntensitySystem>();

            _playerHealth = world.RegisterViewSource(PlayerHealthView);
        }

        protected override void OnDeconstruct() {}

        public Entity SpawnPlayerAvatar(Entity owner)
        {
            var entity = new Entity("avatar");
            owner.Read<PlayerConfig>().AvatarConfig.Apply(entity);

            var view = world.RegisterViewSource(entity.Read<AvatarView>().Value);
            entity.InstantiateView(view);

            entity.Get<Owner>().Value = owner;
            entity.Set(new Hover {Direction = false, Amount = 0});
            
            var health = new Entity("Healthbar");
            health.SetParent(entity);
            health.Get<Owner>().Value = owner;
            health.Get<PlayerHealthOverlay>().Value = entity.Get<PlayerHealth>().Value;

            entity.Get<PlayerDamagedCounter>().Value = 0;

            //var id = NetworkData.SlotInRoom;
            //var local = world.GetFeature<PlayerFeature>().GetPlayerByID(id);
            //if (owner != local)
            //{
            //    health.InstantiateView(_playerHealth);
            //}

            health.InstantiateView(_playerHealth);

            entity.Get<WeaponEntities>().LeftWeapon = ConstructWeapon(owner.Read<PlayerConfig>().LeftWeaponConfig, entity);
            entity.Get<WeaponEntities>().RightWeapon = ConstructWeapon(owner.Read<PlayerConfig>().RightWeaponConfig, entity);

            entity.Get<MoveSpeedModifier>().Value = 1;

            SetAvatarPosition(owner, entity);

            world.GetFeature<EventsFeature>().SkillImageChange.Execute(owner);
            world.GetFeature<EventsFeature>().PassLocalPlayer.Execute(owner);

            if (NetworkData.Connect != null)
                SystemMessages.PlayerJoinedWorld();
            return entity;
        }

        private Entity ConstructWeapon(DataConfig weaponConfig, Entity parent)
        {
            var weapon = new Entity("weapon");
            weaponConfig.Apply(weapon);

            weapon.SetParent(parent);
            weapon.SetLocalPosition(weapon.Read<WeaponPosition>().Value);

            var aim = new Entity("aim");
            aim.SetParent(weapon);
            aim.SetLocalPosition(weapon.Has<TrajectoryWeapon>() ? _direction + _trajectory : _direction);

            weapon.Get<WeaponAim>().Value = aim;
            weapon.Get<AmmoCapacity>().Value = weapon.Read<AmmoCapacityDefault>().Value;

            var view = world.RegisterViewSource(weapon.Read<WeaponView>().Value);
            weapon.InstantiateView(view);

            weapon.Get<Owner>().Value = parent.Get<Owner>().Value;

            return weapon;
        }

        public void SpawnSkills(Entity entity)
        {
            ref var skills = ref entity.Get<SkillEntities>();
            var owner = entity;
            
            skills.FirstSkill = ConstructSkill(entity.Read<PlayerConfig>().FirstSkillConfig, owner, 0);
            skills.SecondSkill = ConstructSkill(entity.Read<PlayerConfig>().SecondSkillConfig, owner, 1);
            skills.ThirdSkill = ConstructSkill(entity.Read<PlayerConfig>().ThirdSkillConfig, owner, 2);
            skills.FourthSkill = ConstructSkill(entity.Read<PlayerConfig>().FourthSkillConfig, owner, 3);
        }
        
        private Entity ConstructSkill(DataConfig skillConfig, Entity owner, int id)
        {
            var skill = new Entity("skill");
            skillConfig.Apply(skill);
            skill.Set(new SkillTag {id = id});
            skill.Get<Owner>().Value = owner;
            return skill;
        }

        private void SetAvatarPosition(Entity owner, Entity entity)
        {
            var redSpawnPoints = world.ReadSharedData<MapComponents>().RedTeamSpawnPoints;
            var blueSpawnPoints = world.ReadSharedData<MapComponents>().BlueTeamSpawnPoints;
            var pos = fp3.zero;

            if (NetworkData.GameMode == GameModes.teambattle)
            {
                if (owner.Read<PlayerTag>().Team == TeamTypes.red)
                {
                    pos = SceneUtils.GetTeamSpawnPosition(redSpawnPoints);
                }
                else
                {
                    pos = SceneUtils.GetTeamSpawnPosition(blueSpawnPoints);
                }
            }

            if (pos == fp3.zero)
                pos = SceneUtils.GetRandomPosition();

            entity.SetPosition(pos);
            SceneUtils.ModifyWalkable(entity.GetPosition(), false);
            entity.Get<FaceDirection>().Value = new fp3(0, 0, 1);
            entity.Get<PlayerMoveTarget>().Value = entity.GetPosition();
        }
    }
}