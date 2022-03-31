namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FirstSkillTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SecondSkillTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ThirdSkillTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponTag>(false, false, false, false, false, false, false);
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
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.AmmoTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.AmmoTileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.HealPoweUpTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.PortalTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Lifetime.Components.LifeTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Lifetime.Components.LifeTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Lifetime.Components.LifeTimeTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileIsLaser>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileLaserIsntActive>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileSpeedModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.ActiveSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.ChannelSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.PassiveSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.SkillComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.ToggleSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponAmmo>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponAmmoDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponCooldownDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponReloadTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponReloadTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponSpread>(false, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.FirstSkillTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ProjectileTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.SecondSkillTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ThirdSkillTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.WeaponTag>(false, false, false, false, false, false, false);
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
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.AmmoTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.AmmoTileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.HealPoweUpTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.PortalTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Lifetime.Components.LifeTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Lifetime.Components.LifeTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Lifetime.Components.LifeTimeTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileIsLaser>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileLaserIsntActive>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileSpeedModifier>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.ActiveSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.ChannelSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.PassiveSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.SkillComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Skills.Components.ToggleSkill>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponAmmo>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponAmmoDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponCooldownDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponReloadTime>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponReloadTimeDefault>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Weapon.Components.WeaponSpread>(false, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<Project.Common.Components.ApplyDamage>(false);
            structComponentsContainer.Validate<Project.Common.Components.FirstSkillTag>(true);
            structComponentsContainer.Validate<Project.Common.Components.ProjectileTag>(true);
            structComponentsContainer.Validate<Project.Common.Components.SecondSkillTag>(true);
            structComponentsContainer.Validate<Project.Common.Components.ThirdSkillTag>(true);
            structComponentsContainer.Validate<Project.Common.Components.WeaponShot>(true);
            structComponentsContainer.Validate<Project.Common.Components.WeaponTag>(false);
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
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerIsRotating>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerMoveTarget>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerScore>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.TeleportPlayer>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.AmmoTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.AmmoTileTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.HealPoweUpTag>(true);
            structComponentsContainer.ValidateCopyable<Project.Core.Features.SceneBuilder.Components.MapComponents>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.MineTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.PortalTag>(true);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Lifetime.Components.LifeTime>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Lifetime.Components.LifeTimeDefault>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Lifetime.Components.LifeTimeTag>(true);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileDirection>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileIsLaser>(true);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileLaserIsntActive>(true);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileSpeed>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileSpeedModifier>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Skills.Components.ActiveSkill>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Skills.Components.ChannelSkill>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Skills.Components.PassiveSkill>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Skills.Components.SkillComponents>(true);
            structComponentsContainer.Validate<Project.Mechanics.Features.Skills.Components.ToggleSkill>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Weapon.Components.WeaponAmmo>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Weapon.Components.WeaponAmmoDefault>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Weapon.Components.WeaponCooldown>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Weapon.Components.WeaponCooldownDefault>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Weapon.Components.WeaponReloadTime>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Weapon.Components.WeaponReloadTimeDefault>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Weapon.Components.WeaponSpread>(false);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<Project.Common.Components.ApplyDamage>(false);
            entity.ValidateData<Project.Common.Components.FirstSkillTag>(true);
            entity.ValidateData<Project.Common.Components.ProjectileTag>(true);
            entity.ValidateData<Project.Common.Components.SecondSkillTag>(true);
            entity.ValidateData<Project.Common.Components.ThirdSkillTag>(true);
            entity.ValidateData<Project.Common.Components.WeaponShot>(true);
            entity.ValidateData<Project.Common.Components.WeaponTag>(false);
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
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerIsRotating>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerMoveTarget>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerScore>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerTag>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.TeleportPlayer>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.AmmoTag>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.AmmoTileTag>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.HealPoweUpTag>(true);
            entity.ValidateDataCopyable<Project.Core.Features.SceneBuilder.Components.MapComponents>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.MineTag>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.PortalTag>(true);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false);
            entity.ValidateData<Project.Mechanics.Features.Lifetime.Components.LifeTime>(false);
            entity.ValidateData<Project.Mechanics.Features.Lifetime.Components.LifeTimeDefault>(false);
            entity.ValidateData<Project.Mechanics.Features.Lifetime.Components.LifeTimeTag>(true);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileDirection>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileIsLaser>(true);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileLaserIsntActive>(true);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileSpeed>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileSpeedModifier>(false);
            entity.ValidateData<Project.Mechanics.Features.Skills.Components.ActiveSkill>(false);
            entity.ValidateData<Project.Mechanics.Features.Skills.Components.ChannelSkill>(false);
            entity.ValidateData<Project.Mechanics.Features.Skills.Components.PassiveSkill>(false);
            entity.ValidateData<Project.Mechanics.Features.Skills.Components.SkillComponents>(true);
            entity.ValidateData<Project.Mechanics.Features.Skills.Components.ToggleSkill>(false);
            entity.ValidateData<Project.Mechanics.Features.Weapon.Components.WeaponAmmo>(false);
            entity.ValidateData<Project.Mechanics.Features.Weapon.Components.WeaponAmmoDefault>(false);
            entity.ValidateData<Project.Mechanics.Features.Weapon.Components.WeaponCooldown>(false);
            entity.ValidateData<Project.Mechanics.Features.Weapon.Components.WeaponCooldownDefault>(false);
            entity.ValidateData<Project.Mechanics.Features.Weapon.Components.WeaponReloadTime>(false);
            entity.ValidateData<Project.Mechanics.Features.Weapon.Components.WeaponReloadTimeDefault>(false);
            entity.ValidateData<Project.Mechanics.Features.Weapon.Components.WeaponSpread>(false);

        }

    }

}
