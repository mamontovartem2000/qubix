using ME.ECS;

namespace Assets.Project.Common.Views.VFX
{
    using ME.ECS.Views.Providers;

    public class WallTileTParticleOne : ParticleViewSource<ApplyWallTileTParticleOneStateParticle> { }

    [System.Serializable]
    public class ApplyWallTileTParticleOneStateParticle : ParticleView<ApplyWallTileTParticleOneStateParticle>
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