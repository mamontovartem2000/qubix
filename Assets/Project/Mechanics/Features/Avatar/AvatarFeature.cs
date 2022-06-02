using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.Events;
using Project.Core.Features.Player;
using Project.Mechanics.Features.Avatar.Systems;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Mechanics.Features.Avatar
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
        //private fp3[] team1 = new fp3[] { new fp3(11, 0, 5), new fp3(4, 0, 7), new fp3(3, 0, 17), new fp3(3, 0, 26), new fp3(3, 0, 34), new fp3(3, 0, 42) };
        //private fp3[] team2 = new fp3[] { new fp3(44, 0, 8), new fp3(47, 0, 13), new fp3(48, 0, 20), new fp3(48, 0, 29), new fp3(48, 0, 39), new fp3(44, 0, 43) };

        protected override void OnConstruct()
        {
            AddSystem<SpawnPlayerAvatarSystem>();
            AddSystem<ApplyDamageSystem>();
            AddSystem<PlayerHealthSystem>();
            AddSystem<PlayerMovementSystem>();
            AddSystem<BlinkHurtSystem>();
            AddSystem<ShieldApplyDamageSystem>();

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

            var id = world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order;
            var local = world.GetFeature<PlayerFeature>().GetPlayerByID(id);
            
            var health = new Entity("Healthbar");
            health.SetParent(entity);
            health.Get<PlayerHealthOverlay>().Value = entity.Get<PlayerHealth>().Value;
            health.Get<Owner>().Value = owner;

            entity.Get<PlayerDamagedCounter>().Value = 0;
            
            if (owner != local)
            {
                health.InstantiateView(_playerHealth);
            }

            entity.Get<WeaponEntities>().LeftWeapon = ConstructWeapon(owner.Read<PlayerConfig>().LeftWeaponConfig, entity);
            entity.Get<WeaponEntities>().RightWeapon = ConstructWeapon(owner.Read<PlayerConfig>().RightWeaponConfig, entity);

            SpawnSkills(entity);

            entity.Get<MoveSpeedModifier>().Value = 1;

            world.GetFeature<EventsFeature>().SkillImageChange.Execute(owner);

            entity.SetPosition(SceneUtils.GetRandomFreePosition());

            ////TODO: КОСТЫЛЬ ЕБУЧИЙ
            //if (NetworkData.Info.map_id == 1 || NetworkData.Team == string.Empty)
            //{
            //    entity.SetPosition(SceneUtils.GetRandomFreePosition());
            //}
            //else
            //{
            //    var pos = fp3.zero;

            //    if (owner.Read<PlayerTag>().Team == "red")
            //    {
            //        while (!SceneUtils.IsWalkable(pos))
            //        {
            //            var rnd = Worlds.current.GetRandomRange(0, team1.Length);
            //            pos = team1[rnd];

            //            if (!SceneUtils.IsFree(pos)) pos = fp3.zero;
            //        }
            //    }
            //    else
            //    {
            //        while (!SceneUtils.IsWalkable(pos))
            //        {
            //            var rnd = Worlds.current.GetRandomRange(0, team2.Length);
            //            pos = team2[rnd];

            //            if (!SceneUtils.IsFree(pos)) pos = fp3.zero;
            //        }
            //    }

            //    entity.SetPosition(pos);
            //}

            SceneUtils.TakeTheCell(entity.GetPosition());
            entity.Get<FaceDirection>().Value = new fp3(0, 0, 1);
            entity.Get<PlayerMoveTarget>().Value = entity.GetPosition();

            world.GetFeature<EventsFeature>().PassLocalPlayer.Execute(owner);
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

        private void SpawnSkills(Entity entity)
        {
            ref var skills = ref entity.Get<SkillEntities>();
            var owner = entity.Read<Owner>().Value;
            
            skills.FirstSkill = ConstructSkill(owner.Read<PlayerConfig>().FirstSkillConfig, owner, 0);
            skills.SecondSkill = ConstructSkill(owner.Read<PlayerConfig>().SecondSkillConfig, owner, 1);
            skills.ThirdSkill = ConstructSkill(owner.Read<PlayerConfig>().ThirdSkillConfig, owner, 2);
            skills.FourthSkill = ConstructSkill(owner.Read<PlayerConfig>().FourthSkillConfig, owner, 3);
        }
        
        private Entity ConstructSkill(DataConfig skillConfig, Entity owner, int id)
        {
            var skill = new Entity("skill");
            skillConfig.Apply(skill);
            skill.Set(new SkillTag {id = id});
            skill.Get<Owner>().Value = owner;
            return skill;
        }
    }
}