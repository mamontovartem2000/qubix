namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerCollided>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerShot>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerShouldRotate>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.RespawnTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.BulletCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileShouldDie>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileType>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.RocketCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.CollectibleTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.PowerUpTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.TrapTag>(false, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerCollided>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerShot>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerShouldRotate>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.RespawnTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.TeleportPlayer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.BulletCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileShouldDie>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileType>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.RocketCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.CollectibleTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.PowerUpTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.TrapTag>(false, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.ApplyDamage>(false);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.CollisionTag>(false);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.PortalTag>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.DeadBody>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerCollided>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerHasStopped>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerHealth>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerIsMoving>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerIsRotating>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerMovementSpeed>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerMoveTarget>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerScore>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerShot>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerShouldRotate>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerTag>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.RespawnTag>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.TeleportPlayer>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.BulletCooldown>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileDamage>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileDirection>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileShouldDie>(true);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileSpeed>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileTag>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileType>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.RocketCooldown>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.CollectibleTag>(true);
            structComponentsContainer.ValidateCopyable<Project.Features.SceneBuilder.Components.MapComponents>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.PowerUpTag>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.TrapTag>(false);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<Project.Features.CollisionHandler.Components.ApplyDamage>(false);
            entity.ValidateData<Project.Features.CollisionHandler.Components.CollisionTag>(false);
            entity.ValidateData<Project.Features.CollisionHandler.Components.PortalTag>(false);
            entity.ValidateData<Project.Features.Player.Components.DeadBody>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerCollided>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerHasStopped>(true);
            entity.ValidateData<Project.Features.Player.Components.PlayerHealth>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerIsMoving>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerIsRotating>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerMovementSpeed>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerMoveTarget>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerScore>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerShot>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerShouldRotate>(true);
            entity.ValidateData<Project.Features.Player.Components.PlayerTag>(false);
            entity.ValidateData<Project.Features.Player.Components.RespawnTag>(true);
            entity.ValidateData<Project.Features.Player.Components.TeleportPlayer>(false);
            entity.ValidateData<Project.Features.Projectile.Components.BulletCooldown>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileDamage>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileDirection>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileShouldDie>(true);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileSpeed>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileTag>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileType>(false);
            entity.ValidateData<Project.Features.Projectile.Components.RocketCooldown>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.CollectibleTag>(true);
            entity.ValidateDataCopyable<Project.Features.SceneBuilder.Components.MapComponents>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.PowerUpTag>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.TrapTag>(false);

        }

    }

}
