namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Common.Components.ActivateSkill>(true, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AmmoCapacity>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AmmoCapacityDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AutomaticWeapon>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AvatarTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Cooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.CooldownDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.DamagedBy>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.DamageSource>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FaceDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FiringCooldownModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LifeTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LifeTimeLeft>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Linear>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearActive>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearFull>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearVisual>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LockTarget>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeActive>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeDelay>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeDelayDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MoveInput>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MoveSpeedModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.NeedAvatar>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.NeedWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Owner>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PassiveSkill>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerAvatar>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerConfig>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileActive>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileConfig>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileView>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ReloadTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ReloadTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.RespawnTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.RightWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillAmount>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillConfig>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillDuration>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillDurationDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillEffect>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillEntities>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SpeedModifier>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SpreadAmount>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Trajectory>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.TrajectoryWeapon>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponAim>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponEntities>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponPosition>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.EndGame>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameFinished>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GamePaused>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameTimer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.Initialized>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.DispenserTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.HealthTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.PortalTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.Spawned>(true, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Common.Components.ActivateSkill>(true, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AmmoCapacity>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AmmoCapacityDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AutomaticWeapon>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AvatarTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Cooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.CooldownDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.DamagedBy>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.DamageSource>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FaceDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FiringCooldownModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LifeTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LifeTimeLeft>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Linear>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearActive>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearFull>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearVisual>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LockTarget>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeActive>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeDelay>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeDelayDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MoveInput>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MoveSpeedModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.NeedAvatar>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.NeedWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Owner>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PassiveSkill>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerAvatar>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerConfig>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileActive>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileConfig>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileView>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ReloadTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ReloadTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.RespawnTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.RightWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillAmount>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillConfig>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillDuration>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillDurationDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillEffect>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillEntities>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SpeedModifier>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SpreadAmount>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Trajectory>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.TrajectoryWeapon>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponAim>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponEntities>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponPosition>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.EndGame>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameFinished>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GamePaused>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameTimer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.Initialized>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.DispenserTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.HealthTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.PortalTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.Spawned>(true, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.ValidateOneShot<Project.Common.Components.ActivateSkill>(true);
            structComponentsContainer.Validate<Project.Common.Components.AmmoCapacity>(false);
            structComponentsContainer.Validate<Project.Common.Components.AmmoCapacityDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.ApplyDamage>(false);
            structComponentsContainer.Validate<Project.Common.Components.AutomaticWeapon>(true);
            structComponentsContainer.Validate<Project.Common.Components.AvatarTag>(true);
            structComponentsContainer.Validate<Project.Common.Components.Cooldown>(false);
            structComponentsContainer.Validate<Project.Common.Components.CooldownDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.DamagedBy>(false);
            structComponentsContainer.Validate<Project.Common.Components.DamageSource>(true);
            structComponentsContainer.Validate<Project.Common.Components.FaceDirection>(false);
            structComponentsContainer.Validate<Project.Common.Components.FiringCooldownModifier>(false);
            structComponentsContainer.Validate<Project.Common.Components.LeftWeaponShot>(true);
            structComponentsContainer.Validate<Project.Common.Components.LifeTimeDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.LifeTimeLeft>(false);
            structComponentsContainer.Validate<Project.Common.Components.Linear>(false);
            structComponentsContainer.Validate<Project.Common.Components.LinearActive>(true);
            structComponentsContainer.Validate<Project.Common.Components.LinearFull>(true);
            structComponentsContainer.Validate<Project.Common.Components.LinearVisual>(true);
            structComponentsContainer.Validate<Project.Common.Components.LinearWeapon>(false);
            structComponentsContainer.Validate<Project.Common.Components.LockTarget>(true);
            structComponentsContainer.Validate<Project.Common.Components.MeleeActive>(true);
            structComponentsContainer.Validate<Project.Common.Components.MeleeDelay>(false);
            structComponentsContainer.Validate<Project.Common.Components.MeleeDelayDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.MeleeWeapon>(false);
            structComponentsContainer.Validate<Project.Common.Components.MoveInput>(false);
            structComponentsContainer.Validate<Project.Common.Components.MoveSpeedModifier>(false);
            structComponentsContainer.Validate<Project.Common.Components.NeedAvatar>(false);
            structComponentsContainer.Validate<Project.Common.Components.NeedWeapon>(false);
            structComponentsContainer.Validate<Project.Common.Components.Owner>(false);
            structComponentsContainer.Validate<Project.Common.Components.PassiveSkill>(true);
            structComponentsContainer.Validate<Project.Common.Components.PlayerAvatar>(false);
            structComponentsContainer.Validate<Project.Common.Components.PlayerConfig>(false);
            structComponentsContainer.Validate<Project.Common.Components.PlayerHealth>(false);
            structComponentsContainer.Validate<Project.Common.Components.PlayerMovementSpeed>(false);
            structComponentsContainer.Validate<Project.Common.Components.PlayerMoveTarget>(false);
            structComponentsContainer.Validate<Project.Common.Components.PlayerScore>(false);
            structComponentsContainer.Validate<Project.Common.Components.PlayerTag>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileActive>(true);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileConfig>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileDamage>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileDirection>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileSpeed>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileView>(false);
            structComponentsContainer.Validate<Project.Common.Components.ReloadTime>(false);
            structComponentsContainer.Validate<Project.Common.Components.ReloadTimeDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.RespawnTime>(false);
            structComponentsContainer.Validate<Project.Common.Components.RightWeaponShot>(true);
            structComponentsContainer.Validate<Project.Common.Components.SkillAmount>(false);
            structComponentsContainer.Validate<Project.Common.Components.SkillConfig>(false);
            structComponentsContainer.Validate<Project.Common.Components.SkillDuration>(false);
            structComponentsContainer.Validate<Project.Common.Components.SkillDurationDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.SkillEffect>(false);
            structComponentsContainer.Validate<Project.Common.Components.SkillEntities>(false);
            structComponentsContainer.Validate<Project.Common.Components.SkillTag>(true);
            structComponentsContainer.Validate<Project.Common.Components.SpeedModifier>(true);
            structComponentsContainer.Validate<Project.Common.Components.SpreadAmount>(false);
            structComponentsContainer.Validate<Project.Common.Components.TeleportPlayer>(false);
            structComponentsContainer.Validate<Project.Common.Components.Trajectory>(false);
            structComponentsContainer.Validate<Project.Common.Components.TrajectoryWeapon>(true);
            structComponentsContainer.Validate<Project.Common.Components.WeaponAim>(false);
            structComponentsContainer.Validate<Project.Common.Components.WeaponEntities>(false);
            structComponentsContainer.Validate<Project.Common.Components.WeaponPosition>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.EndGame>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GameFinished>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GamePaused>(true);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GameTimer>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.Initialized>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.DispenserTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.HealthTag>(true);
            structComponentsContainer.ValidateCopyable<Project.Core.Features.SceneBuilder.Components.MapComponents>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.MineTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.PortalTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.Spawned>(true);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateDataOneShot<Project.Common.Components.ActivateSkill>(true);
            entity.ValidateData<Project.Common.Components.AmmoCapacity>(false);
            entity.ValidateData<Project.Common.Components.AmmoCapacityDefault>(false);
            entity.ValidateData<Project.Common.Components.ApplyDamage>(false);
            entity.ValidateData<Project.Common.Components.AutomaticWeapon>(true);
            entity.ValidateData<Project.Common.Components.AvatarTag>(true);
            entity.ValidateData<Project.Common.Components.Cooldown>(false);
            entity.ValidateData<Project.Common.Components.CooldownDefault>(false);
            entity.ValidateData<Project.Common.Components.DamagedBy>(false);
            entity.ValidateData<Project.Common.Components.DamageSource>(true);
            entity.ValidateData<Project.Common.Components.FaceDirection>(false);
            entity.ValidateData<Project.Common.Components.FiringCooldownModifier>(false);
            entity.ValidateData<Project.Common.Components.LeftWeaponShot>(true);
            entity.ValidateData<Project.Common.Components.LifeTimeDefault>(false);
            entity.ValidateData<Project.Common.Components.LifeTimeLeft>(false);
            entity.ValidateData<Project.Common.Components.Linear>(false);
            entity.ValidateData<Project.Common.Components.LinearActive>(true);
            entity.ValidateData<Project.Common.Components.LinearFull>(true);
            entity.ValidateData<Project.Common.Components.LinearVisual>(true);
            entity.ValidateData<Project.Common.Components.LinearWeapon>(false);
            entity.ValidateData<Project.Common.Components.LockTarget>(true);
            entity.ValidateData<Project.Common.Components.MeleeActive>(true);
            entity.ValidateData<Project.Common.Components.MeleeDelay>(false);
            entity.ValidateData<Project.Common.Components.MeleeDelayDefault>(false);
            entity.ValidateData<Project.Common.Components.MeleeWeapon>(false);
            entity.ValidateData<Project.Common.Components.MoveInput>(false);
            entity.ValidateData<Project.Common.Components.MoveSpeedModifier>(false);
            entity.ValidateData<Project.Common.Components.NeedAvatar>(false);
            entity.ValidateData<Project.Common.Components.NeedWeapon>(false);
            entity.ValidateData<Project.Common.Components.Owner>(false);
            entity.ValidateData<Project.Common.Components.PassiveSkill>(true);
            entity.ValidateData<Project.Common.Components.PlayerAvatar>(false);
            entity.ValidateData<Project.Common.Components.PlayerConfig>(false);
            entity.ValidateData<Project.Common.Components.PlayerHealth>(false);
            entity.ValidateData<Project.Common.Components.PlayerMovementSpeed>(false);
            entity.ValidateData<Project.Common.Components.PlayerMoveTarget>(false);
            entity.ValidateData<Project.Common.Components.PlayerScore>(false);
            entity.ValidateData<Project.Common.Components.PlayerTag>(false);
            entity.ValidateData<Project.Common.Components.ProjectileActive>(true);
            entity.ValidateData<Project.Common.Components.ProjectileConfig>(false);
            entity.ValidateData<Project.Common.Components.ProjectileDamage>(false);
            entity.ValidateData<Project.Common.Components.ProjectileDirection>(false);
            entity.ValidateData<Project.Common.Components.ProjectileSpeed>(false);
            entity.ValidateData<Project.Common.Components.ProjectileView>(false);
            entity.ValidateData<Project.Common.Components.ReloadTime>(false);
            entity.ValidateData<Project.Common.Components.ReloadTimeDefault>(false);
            entity.ValidateData<Project.Common.Components.RespawnTime>(false);
            entity.ValidateData<Project.Common.Components.RightWeaponShot>(true);
            entity.ValidateData<Project.Common.Components.SkillAmount>(false);
            entity.ValidateData<Project.Common.Components.SkillConfig>(false);
            entity.ValidateData<Project.Common.Components.SkillDuration>(false);
            entity.ValidateData<Project.Common.Components.SkillDurationDefault>(false);
            entity.ValidateData<Project.Common.Components.SkillEffect>(false);
            entity.ValidateData<Project.Common.Components.SkillEntities>(false);
            entity.ValidateData<Project.Common.Components.SkillTag>(true);
            entity.ValidateData<Project.Common.Components.SpeedModifier>(true);
            entity.ValidateData<Project.Common.Components.SpreadAmount>(false);
            entity.ValidateData<Project.Common.Components.TeleportPlayer>(false);
            entity.ValidateData<Project.Common.Components.Trajectory>(false);
            entity.ValidateData<Project.Common.Components.TrajectoryWeapon>(true);
            entity.ValidateData<Project.Common.Components.WeaponAim>(false);
            entity.ValidateData<Project.Common.Components.WeaponEntities>(false);
            entity.ValidateData<Project.Common.Components.WeaponPosition>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.EndGame>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.GameFinished>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.GamePaused>(true);
            entity.ValidateData<Project.Core.Features.GameState.Components.GameTimer>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.Initialized>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.DispenserTag>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.HealthTag>(true);
            entity.ValidateDataCopyable<Project.Core.Features.SceneBuilder.Components.MapComponents>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.MineTag>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.PortalTag>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.Spawned>(true);

        }

    }

}
