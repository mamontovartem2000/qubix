namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.PlayerCollided>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.HealthComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LastHit>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LeftWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LeftWeaponReload>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.RespawnTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.RightWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.RightWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.LeftWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.MovementComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerShouldRotate>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileShouldDie>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileType>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.RightWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.AmmoTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.AmmoTileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.CollectibleTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.HealPoweUpTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.RifleAmmoTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.RocketAmmoTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.FiringCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.Magazine>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.ProjectileTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.Realod>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.WeaponTag>(false, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.PlayerCollided>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.HealthComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LastHit>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LeftWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LeftWeaponReload>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.RespawnTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.RightWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.RightWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.LeftWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.MovementComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerShouldRotate>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileShouldDie>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileType>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.RightWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.AmmoTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.AmmoTileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.CollectibleTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.HealPoweUpTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.RifleAmmoTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.RocketAmmoTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.FiringCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.Magazine>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.ProjectileTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.Realod>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Weapon.Components.WeaponTag>(false, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.ApplyDamage>(false);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.CollisionTag>(false);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.PlayerCollided>(false);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.PortalTag>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.DeadBody>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.HealthComponents>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.LastHit>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.LeftWeapon>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.LeftWeaponReload>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.LeftWeaponShot>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerHealth>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerIsRotating>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerScore>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerTag>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.RespawnTag>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.RightWeapon>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.RightWeaponShot>(true);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.LeftWeaponCooldown>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.MovementComponents>(true);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerHasStopped>(true);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerIsMoving>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerMovementSpeed>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerMoveTarget>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerShouldRotate>(true);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileDamage>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileDirection>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileShouldDie>(true);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileSpeed>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileTag>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileType>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.RightWeaponCooldown>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.TeleportPlayer>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.AmmoTag>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.AmmoTileTag>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.CollectibleTag>(true);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.HealPoweUpTag>(true);
            structComponentsContainer.ValidateCopyable<Project.Features.SceneBuilder.Components.MapComponents>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.MineTag>(true);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.RifleAmmoTag>(true);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.RocketAmmoTag>(true);
            structComponentsContainer.Validate<Project.Features.Weapon.Components.FiringCooldown>(false);
            structComponentsContainer.Validate<Project.Features.Weapon.Components.Magazine>(false);
            structComponentsContainer.Validate<Project.Features.Weapon.Components.ProjectileTag>(true);
            structComponentsContainer.Validate<Project.Features.Weapon.Components.Realod>(false);
            structComponentsContainer.Validate<Project.Features.Weapon.Components.WeaponTag>(false);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<Project.Features.CollisionHandler.Components.ApplyDamage>(false);
            entity.ValidateData<Project.Features.CollisionHandler.Components.CollisionTag>(false);
            entity.ValidateData<Project.Features.CollisionHandler.Components.PlayerCollided>(false);
            entity.ValidateData<Project.Features.CollisionHandler.Components.PortalTag>(false);
            entity.ValidateData<Project.Features.Player.Components.DeadBody>(false);
            entity.ValidateData<Project.Features.Player.Components.HealthComponents>(true);
            entity.ValidateData<Project.Features.Player.Components.LastHit>(false);
            entity.ValidateData<Project.Features.Player.Components.LeftWeapon>(false);
            entity.ValidateData<Project.Features.Player.Components.LeftWeaponReload>(false);
            entity.ValidateData<Project.Features.Player.Components.LeftWeaponShot>(true);
            entity.ValidateData<Project.Features.Player.Components.PlayerHealth>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerIsRotating>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerScore>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerTag>(false);
            entity.ValidateData<Project.Features.Player.Components.RespawnTag>(true);
            entity.ValidateData<Project.Features.Player.Components.RightWeapon>(false);
            entity.ValidateData<Project.Features.Player.Components.RightWeaponShot>(true);
            entity.ValidateData<Project.Features.Projectile.Components.LeftWeaponCooldown>(false);
            entity.ValidateData<Project.Features.Projectile.Components.MovementComponents>(true);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerHasStopped>(true);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerIsMoving>(false);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerMovementSpeed>(false);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerMoveTarget>(false);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerShouldRotate>(true);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileDamage>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileDirection>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileShouldDie>(true);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileSpeed>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileTag>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileType>(false);
            entity.ValidateData<Project.Features.Projectile.Components.RightWeaponCooldown>(false);
            entity.ValidateData<Project.Features.Projectile.Components.TeleportPlayer>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.AmmoTag>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.AmmoTileTag>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.CollectibleTag>(true);
            entity.ValidateData<Project.Features.SceneBuilder.Components.HealPoweUpTag>(true);
            entity.ValidateDataCopyable<Project.Features.SceneBuilder.Components.MapComponents>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.MineTag>(true);
            entity.ValidateData<Project.Features.SceneBuilder.Components.RifleAmmoTag>(true);
            entity.ValidateData<Project.Features.SceneBuilder.Components.RocketAmmoTag>(true);
            entity.ValidateData<Project.Features.Weapon.Components.FiringCooldown>(false);
            entity.ValidateData<Project.Features.Weapon.Components.Magazine>(false);
            entity.ValidateData<Project.Features.Weapon.Components.ProjectileTag>(true);
            entity.ValidateData<Project.Features.Weapon.Components.Realod>(false);
            entity.ValidateData<Project.Features.Weapon.Components.WeaponTag>(false);

        }

    }

}
