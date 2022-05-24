using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.Events;
using Project.Mechanics.Features.Avatar.Systems;
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
        private readonly Vector3 _direction = new Vector3(0f,0f,1f);
        private readonly Vector3 _trajectory = new Vector3(0f, 1f, 0f);
        
        protected override void OnConstruct()
        {
            AddSystem<SpawnPlayerAvatarSystem>();
            AddSystem<ApplyDamageSystem>();
            AddSystem<PlayerHealthSystem>();
            AddSystem<PlayerMovementSystem>();
        }

        protected override void OnDeconstruct() {}
        
        public Entity SpawnPlayerAvatar(Entity owner)
        {
            var entity = new Entity("avatar");
            owner.Read<PlayerConfig>().AvatarConfig.Apply(entity);

            var view = world.RegisterViewSource(entity.Read<AvatarView>().Value);
            entity.InstantiateView(view);
            
            entity.Get<Owner>().Value = owner;

            entity.Get<WeaponEntities>().LeftWeapon = ConstructWeapon(owner.Read<PlayerConfig>().LeftWeaponConfig, entity);
            entity.Get<WeaponEntities>().RightWeapon = ConstructWeapon(owner.Read<PlayerConfig>().RightWeaponConfig, entity);
            
            ref var skills = ref entity.Get<SkillEntities>();
            
            skills.FirstSkill = ConstructSkill(owner.Read<PlayerConfig>().FirstSkillConfig, owner, 0);
            skills.SecondSkill = ConstructSkill(owner.Read<PlayerConfig>().SecondSkillConfig, owner, 1);
            skills.ThirdSkill = ConstructSkill(owner.Read<PlayerConfig>().ThirdSkillConfig, owner, 2);
            skills.FourthSkill = ConstructSkill(owner.Read<PlayerConfig>().FourthSkillConfig, owner, 3);
            
            world.GetFeature<EventsFeature>().SkillImageChange.Execute(owner);

            entity.SetPosition(SceneUtils.GetRandomFreePosition());
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
            aim.SetLocalPosition(weapon.Has<TrajectoryWeapon>() ?_direction +_trajectory: _direction);

            weapon.Get<WeaponAim>().Value = aim;
            weapon.Get<AmmoCapacity>().Value = weapon.Read<AmmoCapacityDefault>().Value;
            
            var view = world.RegisterViewSource(weapon.Read<WeaponView>().Value);
            weapon.InstantiateView(view);
            
            weapon.Get<Owner>().Value = parent.Get<Owner>().Value;
            
            return weapon;
        }

        private Entity ConstructSkill(DataConfig skillConfig, Entity owner, int id)
        {
            var skill = new Entity("skill");
            skillConfig.Apply(skill);
            skill.Set(new SkillTag{id = id});
            skill.Get<Owner>().Value = owner;
            return skill;
        }
    }
}