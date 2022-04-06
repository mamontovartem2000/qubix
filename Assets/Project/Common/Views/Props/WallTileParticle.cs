using ME.ECS;
using UnityEngine;

namespace Assets.Project.Common.Views.Props
{
    using ME.ECS.Views.Providers;

    public class WallTileParticle : ParticleViewSource<ApplyWallTileParticleStateParticle> { }

    [System.Serializable]
    public class ApplyWallTileParticleStateParticle : ParticleView<ApplyWallTileParticleStateParticle>
    {
        public override void OnInitialize() { }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(float deltaTime, bool immediately)
        {
            ref var rootData = ref this.GetRootData();
            rootData.position = entity.GetPosition() - new Vector3(0f,0.5f,0f);
            rootData.startSize = 1f;
            this.SetRootData(ref rootData);
        }
        public override void ApplyState(float deltaTime, bool immediately) { }
    }
}