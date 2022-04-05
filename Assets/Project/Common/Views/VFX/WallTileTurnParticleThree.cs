using ME.ECS;

namespace Assets.Project.Common.Views.VFX
{
    using ME.ECS.Views.Providers;

    public class WallTileTurnParticleThree : ParticleViewSource<ApplyWallTileTurnParticleThreeStateParticle> { }

    [System.Serializable]
    public class ApplyWallTileTurnParticleThreeStateParticle : ParticleView<ApplyWallTileTurnParticleThreeStateParticle>
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