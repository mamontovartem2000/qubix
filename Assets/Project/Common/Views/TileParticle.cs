using ME.ECS;

namespace Project.Common.Views
{
    using ME.ECS.Views.Providers;

    public class TileParticle : ParticleViewSource<ApplyTileParticleStateParticle> { }

    [System.Serializable]
    public class ApplyTileParticleStateParticle : ParticleView<ApplyTileParticleStateParticle>
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