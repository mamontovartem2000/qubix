using ME.ECS;

namespace Project.Common.Views.Bullets
{
    using ME.ECS.Views.Providers;

    public class MinigunBulletParticle : ParticleViewSource<ApplyMinigunBulletParticleStateParticle> { }

    [System.Serializable]
    public class ApplyMinigunBulletParticleStateParticle : ParticleView<ApplyMinigunBulletParticleStateParticle>
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