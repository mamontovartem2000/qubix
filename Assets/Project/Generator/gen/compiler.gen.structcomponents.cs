namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<LeftWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<LeftWeaponReload>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.ExplosionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.EndGame>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.GameFinished>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.GamePaused>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.GameTimer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.Initialized>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.FaceDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LastHit>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.LeftWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileShouldDie>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileType>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.RightWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.TeleportPlayer>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.AmmoTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.AmmoTileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.HealPoweUpTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.PortalTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<RightWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<RightWeaponShot>(true, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<LeftWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<LeftWeaponReload>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<LeftWeaponShot>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.ApplyDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.CollisionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.ExplosionTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.CollisionHandler.Components.PortalTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.EndGame>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.GameFinished>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.GamePaused>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.GameTimer>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Components.Initialized>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.DeadBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.FaceDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.LastHit>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.LeftWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDamage>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileDirection>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileShouldDie>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.ProjectileType>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.RightWeaponCooldown>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Projectile.Components.TeleportPlayer>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.AmmoTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.AmmoTileTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.HealPoweUpTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MapComponents>(false, true, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.PortalTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<RightWeapon>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<RightWeaponShot>(true, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<LeftWeapon>(false);
            structComponentsContainer.Validate<LeftWeaponReload>(false);
            structComponentsContainer.Validate<LeftWeaponShot>(true);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.ApplyDamage>(false);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.CollisionTag>(false);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.ExplosionTag>(false);
            structComponentsContainer.Validate<Project.Features.CollisionHandler.Components.PortalTag>(false);
            structComponentsContainer.Validate<Project.Features.Components.EndGame>(false);
            structComponentsContainer.Validate<Project.Features.Components.GameFinished>(false);
            structComponentsContainer.Validate<Project.Features.Components.GamePaused>(true);
            structComponentsContainer.Validate<Project.Features.Components.GameTimer>(false);
            structComponentsContainer.Validate<Project.Features.Components.Initialized>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.DeadBody>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.FaceDirection>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.LastHit>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerHealth>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerIsRotating>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerScore>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerTag>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.LeftWeaponCooldown>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerHasStopped>(true);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerIsMoving>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerMovementSpeed>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.PlayerMoveTarget>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileDamage>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileDirection>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileShouldDie>(true);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileSpeed>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileTag>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.ProjectileType>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.RightWeaponCooldown>(false);
            structComponentsContainer.Validate<Project.Features.Projectile.Components.TeleportPlayer>(true);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.AmmoTag>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.AmmoTileTag>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.HealPoweUpTag>(true);
            structComponentsContainer.ValidateCopyable<Project.Features.SceneBuilder.Components.MapComponents>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.MineTag>(true);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.PortalTag>(true);
            structComponentsContainer.Validate<RightWeapon>(false);
            structComponentsContainer.Validate<RightWeaponShot>(true);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<LeftWeapon>(false);
            entity.ValidateData<LeftWeaponReload>(false);
            entity.ValidateData<LeftWeaponShot>(true);
            entity.ValidateData<Project.Features.CollisionHandler.Components.ApplyDamage>(false);
            entity.ValidateData<Project.Features.CollisionHandler.Components.CollisionTag>(false);
            entity.ValidateData<Project.Features.CollisionHandler.Components.ExplosionTag>(false);
            entity.ValidateData<Project.Features.CollisionHandler.Components.PortalTag>(false);
            entity.ValidateData<Project.Features.Components.EndGame>(false);
            entity.ValidateData<Project.Features.Components.GameFinished>(false);
            entity.ValidateData<Project.Features.Components.GamePaused>(true);
            entity.ValidateData<Project.Features.Components.GameTimer>(false);
            entity.ValidateData<Project.Features.Components.Initialized>(true);
            entity.ValidateData<Project.Features.Player.Components.DeadBody>(false);
            entity.ValidateData<Project.Features.Player.Components.FaceDirection>(false);
            entity.ValidateData<Project.Features.Player.Components.LastHit>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerHealth>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerIsRotating>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerScore>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerTag>(false);
            entity.ValidateData<Project.Features.Projectile.Components.LeftWeaponCooldown>(false);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerHasStopped>(true);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerIsMoving>(false);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerMovementSpeed>(false);
            entity.ValidateData<Project.Features.Projectile.Components.PlayerMoveTarget>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileDamage>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileDirection>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileShouldDie>(true);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileSpeed>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileTag>(false);
            entity.ValidateData<Project.Features.Projectile.Components.ProjectileType>(false);
            entity.ValidateData<Project.Features.Projectile.Components.RightWeaponCooldown>(false);
            entity.ValidateData<Project.Features.Projectile.Components.TeleportPlayer>(true);
            entity.ValidateData<Project.Features.SceneBuilder.Components.AmmoTag>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.AmmoTileTag>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.HealPoweUpTag>(true);
            entity.ValidateDataCopyable<Project.Features.SceneBuilder.Components.MapComponents>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.MineTag>(true);
            entity.ValidateData<Project.Features.SceneBuilder.Components.PortalTag>(true);
            entity.ValidateData<RightWeapon>(false);
            entity.ValidateData<RightWeaponShot>(true);

        }

    }

}
