using ME.ECS;

namespace Assets.Project.Common.Views.VFX
{
    using ME.ECS.Views.Providers;

    public class WallTileCrossParticle : ParticleViewSource<ApplyWallTileCrossParticleStateParticle> { }

    [System.Serializable]
    public class ApplyWallTileCrossParticleStateParticle : ParticleView<ApplyWallTileCrossParticleStateParticle>
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