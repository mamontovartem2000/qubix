namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerCollided>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerShouldRotate>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.HealthTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.WalkableCheck>(false, true, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerCollided>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHasStopped>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerHealth>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsMoving>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerIsRotating>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerMovementSpeed>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerMoveTarget>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerScore>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerShouldRotate>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.Player.Components.PlayerTag>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.HealthTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.MineTag>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Project.Features.SceneBuilder.Components.WalkableCheck>(false, true, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerCollided>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerHasStopped>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerHealth>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerIsMoving>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerIsRotating>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerMovementSpeed>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerMoveTarget>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerScore>(false);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerShouldRotate>(true);
            structComponentsContainer.Validate<Project.Features.Player.Components.PlayerTag>(false);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.HealthTag>(true);
            structComponentsContainer.Validate<Project.Features.SceneBuilder.Components.MineTag>(true);
            structComponentsContainer.ValidateCopyable<Project.Features.SceneBuilder.Components.WalkableCheck>(false);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<Project.Features.Player.Components.PlayerCollided>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerHasStopped>(true);
            entity.ValidateData<Project.Features.Player.Components.PlayerHealth>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerIsMoving>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerIsRotating>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerMovementSpeed>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerMoveTarget>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerScore>(false);
            entity.ValidateData<Project.Features.Player.Components.PlayerShouldRotate>(true);
            entity.ValidateData<Project.Features.Player.Components.PlayerTag>(false);
            entity.ValidateData<Project.Features.SceneBuilder.Components.HealthTag>(true);
            entity.ValidateData<Project.Features.SceneBuilder.Components.MineTag>(true);
            entity.ValidateDataCopyable<Project.Features.SceneBuilder.Components.WalkableCheck>(false);

        }

    }

}
