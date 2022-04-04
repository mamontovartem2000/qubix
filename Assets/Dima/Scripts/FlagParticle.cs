using ME.ECS;

<<<<<<< Updated upstream:Assets/Dima/Scripts/BalloonPackParticle.cs
namespace Dima.Scripts
=======
namespace Assets.Project.Common.Views.Props
>>>>>>> Stashed changes:Assets/Dima/Scripts/FlagParticle.cs
{
    using ME.ECS.Views.Providers;

    public class FlagParticle : ParticleViewSource<ApplyFlagParticleStateParticle> { }

    [System.Serializable]
    public class ApplyFlagParticleStateParticle : ParticleView<ApplyFlagParticleStateParticle>
    {
        public override void OnInitialize() { }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(float deltaTime, bool immediately)
        {
            ref var rootData = ref this.GetRootData();
            rootData.position = entity.GetPosition();
            rootData.startSize = 1f;
            this.SetRootData(ref rootData);
        }
        public override void ApplyState(float deltaTime, bool immediately) { }
    }
}