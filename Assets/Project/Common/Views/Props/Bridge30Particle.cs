using ME.ECS;

namespace Project.Common.Views {
    
    using ME.ECS.Views.Providers;
    
    public class Bridge30Particle : ParticleViewSource<ApplyBridge30ParticleStateParticle> { }
    
    [System.Serializable]
    public class ApplyBridge30ParticleStateParticle : ParticleView<ApplyBridge30ParticleStateParticle> {
        
        public override void OnInitialize() {
            
        }
        
        public override void OnDeInitialize() {
            
        }
        
        public override void ApplyStateJob(float deltaTime, bool immediately) {
        
        }
        
        public override void ApplyState(float deltaTime, bool immediately) {
            
            ref var rootData = ref this.GetRootData();
            
            
            
            this.SetRootData(ref rootData);
            
        }
        
    }
    
}