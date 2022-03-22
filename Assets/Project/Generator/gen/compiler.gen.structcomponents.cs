namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamageOneShot>(true, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.EndGame>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameFinished>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GamePaused>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameTimer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.Initialized>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.HealthComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LastHit>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LeftWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LeftWeaponReload>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.MovementComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerDisplay>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerShouldRotate>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.RespawnTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.RightWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.RightWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.AmmoTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.AmmoTileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.CollectibleTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.HealPoweUpTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.RifleAmmoTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.RocketAmmoTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.LeftWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileShouldDie>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileType>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.RightWeaponCooldown>(false, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Common.Components.ApplyDamageOneShot>(true, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.EndGame>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameFinished>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GamePaused>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.GameTimer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.GameState.Components.Initialized>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.HealthComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LastHit>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LeftWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LeftWeaponReload>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.MovementComponents>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerDisplay>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerShouldRotate>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.RespawnTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.RightWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.RightWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.Player.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.AmmoTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.AmmoTileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.CollectibleTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.HealPoweUpTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.RifleAmmoTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Core.Features.SceneBuilder.Components.RocketAmmoTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.LeftWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileShouldDie>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.ProjectileType>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Mechanics.Features.Projectile.Components.RightWeaponCooldown>(false, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<Project.Common.Components.ApplyDamage>(false);
            structComponentsContainer.ValidateOneShot<Project.Common.Components.ApplyDamageOneShot>(true);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.EndGame>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GameFinished>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GamePaused>(true);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.GameTimer>(false);
            structComponentsContainer.Validate<Project.Core.Features.GameState.Components.Initialized>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.DeadBody>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.HealthComponents>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.LastHit>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.LeftWeapon>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.LeftWeaponReload>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.LeftWeaponShot>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.MovementComponents>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerDisplay>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerHasStopped>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerHealth>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerIsMoving>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerIsRotating>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerMoveTarget>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerScore>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerShouldRotate>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.PlayerTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.RespawnTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.RightWeapon>(false);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.RightWeaponShot>(true);
            structComponentsContainer.Validate<Project.Core.Features.Player.Components.TeleportPlayer>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.AmmoTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.AmmoTileTag>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.CollectibleTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.HealPoweUpTag>(true);
            structComponentsContainer.ValidateCopyable<Project.Core.Features.SceneBuilder.Components.MapComponents>(false);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.MineTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.RifleAmmoTag>(true);
            structComponentsContainer.Validate<Project.Core.Features.SceneBuilder.Components.RocketAmmoTag>(true);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.LeftWeaponCooldown>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileDamage>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileDirection>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileShouldDie>(true);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileSpeed>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileTag>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.ProjectileType>(false);
            structComponentsContainer.Validate<Project.Mechanics.Features.Projectile.Components.RightWeaponCooldown>(false);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<Project.Common.Components.ApplyDamage>(false);
            entity.ValidateDataOneShot<Project.Common.Components.ApplyDamageOneShot>(true);
            entity.ValidateData<Project.Core.Features.GameState.Components.EndGame>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.GameFinished>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.GamePaused>(true);
            entity.ValidateData<Project.Core.Features.GameState.Components.GameTimer>(false);
            entity.ValidateData<Project.Core.Features.GameState.Components.Initialized>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.DeadBody>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.HealthComponents>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.LastHit>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.LeftWeapon>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.LeftWeaponReload>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.LeftWeaponShot>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.MovementComponents>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerDisplay>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerHasStopped>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerHealth>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerIsMoving>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerIsRotating>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerMovementSpeed>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerMoveTarget>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerScore>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerShouldRotate>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.PlayerTag>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.RespawnTag>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.RightWeapon>(false);
            entity.ValidateData<Project.Core.Features.Player.Components.RightWeaponShot>(true);
            entity.ValidateData<Project.Core.Features.Player.Components.TeleportPlayer>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.AmmoTag>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.AmmoTileTag>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.CollectibleTag>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.HealPoweUpTag>(true);
            entity.ValidateDataCopyable<Project.Core.Features.SceneBuilder.Components.MapComponents>(false);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.MineTag>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.RifleAmmoTag>(true);
            entity.ValidateData<Project.Core.Features.SceneBuilder.Components.RocketAmmoTag>(true);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.CollisionTag>(false);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.ExplosionTag>(false);
            entity.ValidateData<Project.Mechanics.Features.CollisionHandler.Components.PortalTag>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.LeftWeaponCooldown>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileDamage>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileDirection>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileShouldDie>(true);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileSpeed>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileTag>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.ProjectileType>(false);
            entity.ValidateData<Project.Mechanics.Features.Projectile.Components.RightWeaponCooldown>(false);

        }

    }

}
