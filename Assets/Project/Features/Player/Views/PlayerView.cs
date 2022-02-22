using ME.ECS;
using Project.Features.Player.Components;

namespace Project.Features.Player.Views {
    
    using ME.ECS.Views.Providers;
    
    public class PlayerView : MonoBehaviourView {
        
        public override bool applyStateJob => true;

        public override void OnInitialize() {}
        
        public override void OnDeInitialize() {}
        
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
        }
    }
}