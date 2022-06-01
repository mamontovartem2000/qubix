using ME.ECS;
using ME.ECS.Views.Providers;

namespace Project.Common.Views.Bullets.Particle
{
    public class AutorifleBulletParticle : ParticleViewSource<ApplyAutorifleBulletParticleStateParticle> { }

    [System.Serializable]
    public class ApplyAutorifleBulletParticleStateParticle : ParticleView<ApplyAutorifleBulletParticleStateParticle>
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