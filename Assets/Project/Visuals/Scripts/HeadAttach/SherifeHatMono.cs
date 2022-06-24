using ME.ECS;

namespace Assets.Project.NewVisuals.Scripts.HeadAttach {
    
    using ME.ECS.Views.Providers;
    
    public class SherifeHatMono : MonoBehaviourView {
        
        public override bool applyStateJob => true;
        public override void OnInitialize() { }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
        } 
    }
    
}