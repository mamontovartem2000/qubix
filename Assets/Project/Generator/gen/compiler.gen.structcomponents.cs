namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Common.Components.ActiveSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AmmoCapacity>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AmmoCapacityDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AutomaticWeapon>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ChannelSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Cooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.CooldownDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.DamageSource>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FirstSkillTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FourthSkillTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LifeTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LifeTimeLeft>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Linear>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearActive>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearFull>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearVisual>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeActive>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeDelay>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeDelayDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PassiveSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileActive>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileConfig>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDamageModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileSpeedModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileView>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ReloadTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ReloadTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.RightWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SecondSkillTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SpreadAmount>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ThirdSkillTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ToggleSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Trajectory>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.TrajectoryWeapon>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponAim>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponView>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.EndGame>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameFinished>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GamePaused>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameTimer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.Initialized>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.FaceDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LastHit>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LockTarget>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.MoveInput>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.NeedWeapon>(true, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.DispenserTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.HealthTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.PortalTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.Spawned>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Common.Components.ActiveSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AmmoCapacity>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AmmoCapacityDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.AutomaticWeapon>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ChannelSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Cooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.CooldownDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.DamageSource>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FirstSkillTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FourthSkillTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LifeTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LifeTimeLeft>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Linear>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearActive>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearFull>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearVisual>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.LinearWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeActive>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeDelay>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeDelayDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.MeleeWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.PassiveSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileActive>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileConfig>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDamageModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileSpeedModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileView>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ReloadTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ReloadTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.RightWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SecondSkillTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SkillComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SpreadAmount>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ThirdSkillTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ToggleSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.Trajectory>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.TrajectoryWeapon>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponAim>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponView>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.EndGame>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameFinished>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GamePaused>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameTimer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.Initialized>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.FaceDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LastHit>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LockTarget>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.MoveInput>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.NeedWeapon>(true, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.DispenserTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.HealthTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.PortalTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.Spawned>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<Project.Common.Components.ActiveSkill>(false);
            structComponentsContainer.Validate<Project.Common.Components.AmmoCapacity>(false);
            structComponentsContainer.Validate<Project.Common.Components.AmmoCapacityDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.ApplyDamage>(false);
            structComponentsContainer.Validate<Project.Common.Components.AutomaticWeapon>(true);
            structComponentsContainer.Validate<Project.Common.Components.ChannelSkill>(false);
            structComponentsContainer.Validate<Project.Common.Components.Cooldown>(false);
            structComponentsContainer.Validate<Project.Common.Components.CooldownDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.DamageSource>(true);
            structComponentsContainer.Validate<Project.Common.Components.FirstSkillTag>(false);
            structComponentsContainer.Validate<Project.Common.Components.FourthSkillTag>(false);
            structComponentsContainer.Validate<Project.Common.Components.LeftWeaponShot>(true);
            structComponentsContainer.Validate<Project.Common.Components.LifeTimeDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.LifeTimeLeft>(false);
            structComponentsContainer.Validate<Project.Common.Components.Linear>(false);
            structComponentsContainer.Validate<Project.Common.Components.LinearActive>(false);
            structComponentsContainer.Validate<Project.Common.Components.LinearFull>(true);
            structComponentsContainer.Validate<Project.Common.Components.LinearVisual>(false);
            structComponentsContainer.Validate<Project.Common.Components.LinearWeapon>(false);
            structComponentsContainer.Validate<Project.Common.Components.MeleeActive>(false);
            structComponentsContainer.Validate<Project.Common.Components.MeleeDelay>(false);
            structComponentsContainer.Validate<Project.Common.Components.MeleeDelayDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.MeleeWeapon>(false);
            structComponentsContainer.Validate<Project.Common.Components.PassiveSkill>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileActive>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileConfig>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileDamage>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileDamageModifier>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileDirection>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileSpeed>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileSpeedModifier>(false);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileView>(false);
            structComponentsContainer.Validate<Project.Common.Components.ReloadTime>(false);
            structComponentsContainer.Validate<Project.Common.Components.ReloadTimeDefault>(false);
            structComponentsContainer.Validate<Project.Common.Components.RightWeaponShot>(true);
            structComponentsContainer.Validate<Project.Common.Components.SecondSkillTag>(false);
            structComponentsContainer.Validate<Project.Common.Components.SkillComponents>(true);
            structComponentsContainer.Validate<Project.Common.Components.SpreadAmount>(false);
            structComponentsContainer.Validate<Project.Common.Components.ThirdSkillTag>(false);
            structComponentsContainer.Validate<Project.Common.Components.ToggleSkill>(false);
            structComponentsContainer.Validate<Project.Common.Components.Trajectory>(false);
            structComponentsContainer.Validate<Project.Common.Components.TrajectoryWeapon>(true);
            structComponentsContainer.Validate<Project.Common.Components.WeaponAim>(false);
            structComponentsContainer.Validate<Project.Common.Components.WeaponView>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.EndGame>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GameFinished>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GamePaused>(true);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GameTimer>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.Initialized>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.DeadBody>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.FaceDirection>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.LastHit>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.LockTarget>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.MoveInput>(false);
            structComponentsContainer.ValidateOneShot<Project.Core.Features.Player.Components.NeedWeapon>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerHealth>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerMoveTarget>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerScore>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.TeleportPlayer>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.DispenserTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.HealthTag>(true);
            structComponentsContainer.ValidateCopyable<Project.Core.Features.SceneBuilder.Components.MapComponents>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.MineTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.PortalTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.Spawned>(true);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<Project.Common.Components.ActiveSkill>(false);
            entity.ValidateData<Project.Common.Components.AmmoCapacity>(false);
            entity.ValidateData<Project.Common.Components.AmmoCapacityDefault>(false);
            entity.ValidateData<Project.Common.Components.ApplyDamage>(false);
            entity.ValidateData<Project.Common.Components.AutomaticWeapon>(true);
            entity.ValidateData<Project.Common.Components.ChannelSkill>(false);
            entity.ValidateData<Project.Common.Components.Cooldown>(false);
            entity.ValidateData<Project.Common.Components.CooldownDefault>(false);
            entity.ValidateData<Project.Common.Components.DamageSource>(true);
            entity.ValidateData<Project.Common.Components.FirstSkillTag>(false);
            entity.ValidateData<Project.Common.Components.FourthSkillTag>(false);
            entity.ValidateData<Project.Common.Components.LeftWeaponShot>(true);
            entity.ValidateData<Project.Common.Components.LifeTimeDefault>(false);
            entity.ValidateData<Project.Common.Components.LifeTimeLeft>(false);
            entity.ValidateData<Project.Common.Components.Linear>(false);
            entity.ValidateData<Project.Common.Components.LinearActive>(false);
            entity.ValidateData<Project.Common.Components.LinearFull>(true);
            entity.ValidateData<Project.Common.Components.LinearVisual>(false);
            entity.ValidateData<Project.Common.Components.LinearWeapon>(false);
            entity.ValidateData<Project.Common.Components.MeleeActive>(false);
            entity.ValidateData<Project.Common.Components.MeleeDelay>(false);
            entity.ValidateData<Project.Common.Components.MeleeDelayDefault>(false);
            entity.ValidateData<Project.Common.Components.MeleeWeapon>(false);
            entity.ValidateData<Project.Common.Components.PassiveSkill>(false);
            entity.ValidateData<Project.Common.Components.ProjectileActive>(false);
            entity.ValidateData<Project.Common.Components.ProjectileConfig>(false);
            entity.ValidateData<Project.Common.Components.ProjectileDamage>(false);
            entity.ValidateData<Project.Common.Components.ProjectileDamageModifier>(false);
            entity.ValidateData<Project.Common.Components.ProjectileDirection>(false);
            entity.ValidateData<Project.Common.Components.ProjectileSpeed>(false);
            entity.ValidateData<Project.Common.Components.ProjectileSpeedModifier>(false);
            entity.ValidateData<Project.Common.Components.ProjectileView>(false);
            entity.ValidateData<Project.Common.Components.ReloadTime>(false);
            entity.ValidateData<Project.Common.Components.ReloadTimeDefault>(false);
            entity.ValidateData<Project.Common.Components.RightWeaponShot>(true);
            entity.ValidateData<Project.Common.Components.SecondSkillTag>(false);
            entity.ValidateData<Project.Common.Components.SkillComponents>(true);
            entity.ValidateData<Project.Common.Components.SpreadAmount>(false);
            entity.ValidateData<Project.Common.Components.ThirdSkillTag>(false);
            entity.ValidateData<Project.Common.Components.ToggleSkill>(false);
            entity.ValidateData<Project.Common.Components.Trajectory>(false);
            entity.ValidateData<Project.Common.Components.TrajectoryWeapon>(true);
            entity.ValidateData<Project.Common.Components.WeaponAim>(false);
            entity.ValidateData<Project.Common.Components.WeaponView>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.EndGame>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.GameFinished>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.GamePaused>(true);
            entity.ValidateData<Project.Core.Features.GameState.Components.GameTimer>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.Initialized>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.DeadBody>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.FaceDirection>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.LastHit>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.LockTarget>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.MoveInput>(false);
            entity.ValidateDataOneShot<Project.Core.Features.Player.Components.NeedWeapon>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerHealth>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerMoveTarget>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerScore>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerTag>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.TeleportPlayer>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.DispenserTag>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.HealthTag>(true);
            entity.ValidateDataCopyable<Project.Core.Features.SceneBuilder.Components.MapComponents>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.MineTag>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.PortalTag>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.Spawned>(true);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false);

        }

    }

}
