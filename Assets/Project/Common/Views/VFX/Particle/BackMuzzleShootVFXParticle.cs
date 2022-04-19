using ME.ECS;

namespace Project.Common.Views.VFX.Particle
{
    using ME.ECS.Views.Providers;

    public class BackMuzzleShootVFXParticle : ParticleViewSource<ApplyBackMuzzleShootVFXParticleStateParticle> { }

    [System.Serializable]
    public class ApplyBackMuzzleShootVFXParticleStateParticle : ParticleView<ApplyBackMuzzleShootVFXParticleStateParticle>
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