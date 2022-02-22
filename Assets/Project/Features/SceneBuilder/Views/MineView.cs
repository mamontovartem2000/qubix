using ME.ECS;

namespace Project.Features.SceneBuilder.Views 
{
    using ME.ECS.Views.Providers;
    
    public class MineView : ParticleViewSource<ApplyMineViewStateParticle> { }
    
    [System.Serializable]
    public class ApplyMineViewStateParticle : ParticleView<ApplyMineViewStateParticle> 
    {
        public override void OnInitialize() {}
        public override void OnDeInitialize() {}
        
        public override void ApplyStateJob(float deltaTime, bool immediately) {
        
            ref var rootData = ref this.GetRootData();

            rootData.position = this.entity.GetPosition();
            rootData.startSize = 1f;

            this.SetRootData(ref rootData);
        }
        
        public override void ApplyState(float deltaTime, bool immediately) {}
        
    }
    
}