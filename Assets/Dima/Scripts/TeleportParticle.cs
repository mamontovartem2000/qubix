using ME.ECS;

namespace Dima.Scripts
{
    using ME.ECS.Views.Providers;
    
    public class TeleportParticle : ParticleViewSource<ApplyTeleportParticleStateParticle> { }

    [System.Serializable]
    public class ApplyTeleportParticleStateParticle : ParticleView<ApplyTeleportParticleStateParticle>
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