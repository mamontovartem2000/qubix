using ME.ECS;
using UnityEngine;

namespace Project.Common.Views.Monos {
    
    using ME.ECS.Views.Providers;
    
    public class RotatingMono : MonoBehaviourView {
        
        public override bool applyStateJob => true;

        public override void OnInitialize() {
            
        }
        
        public override void OnDeInitialize() {
            
        }
        
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {
            
        }
        
        public override void ApplyState(float deltaTime, bool immediately) {
            transform.position = entity.GetPosition();
            transform.RotateAround(transform.position, Vector3.up, -10000 * Time.deltaTime);
        }
    }
}