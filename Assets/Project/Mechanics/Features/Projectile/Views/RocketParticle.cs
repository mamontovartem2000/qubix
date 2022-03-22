using ME.ECS;
using ME.ECS.Views.Providers;

namespace Project.Mechanics.Features.Projectile.Views 
{
    public class RocketParticle : ParticleViewSource<ApplyRocketParticleStateParticle> { }
    [System.Serializable]
    public class ApplyRocketParticleStateParticle : ParticleView<ApplyRocketParticleStateParticle> 
    {
        public override void OnInitialize() {}
        public override void OnDeInitialize() {}
        
        public override void ApplyStateJob(float deltaTime, bool immediately) 
        {
            ref var rootData = ref this.GetRootData();
            
            rootData.position = entity.GetPosition();
            // rootData.rotation3D = entity.GetRotation().eulerAngles;
            rootData.startSize = 1f;
            
            this.SetRootData(ref rootData);
        }
        
        public override void ApplyState(float deltaTime, bool immediately) {}
    }
}