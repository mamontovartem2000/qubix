using ME.ECS;
using ME.ECS.Views.Providers;

namespace Project.Core.Features.SceneBuilder.Views
{
    public class TileView : ParticleViewSource<ApplyTileViewStateParticle> {}

    [System.Serializable]
    public class ApplyTileViewStateParticle : ParticleView<ApplyTileViewStateParticle>
    {
        public override void OnInitialize() {}
        public override void OnDeInitialize() {}

        public override void ApplyStateJob(float deltaTime, bool immediately)
        {
            ref var rootData = ref this.GetRootData();

            rootData.position = this.entity.GetPosition();
            rootData.startSize = 1f;

            this.SetRootData(ref rootData);
        }

        public override void ApplyState(float deltaTime, bool immediately) {}
    }
}