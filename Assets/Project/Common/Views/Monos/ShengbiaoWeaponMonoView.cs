using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views.Monos {
    
    using ME.ECS.Views.Providers;
    
    public class ShengbiaoWeaponMonoView : MonoBehaviourView {
        
        public Renderer SpearChainRenderer;
        private Material SpearChainMaterial;
        public override bool applyStateJob => true;

        public override void OnInitialize() {
        }
        
        public override void OnDeInitialize() {
            
        }
        
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {
            
        }
        
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
            
            SpearChainMaterial = SpearChainRenderer.materials[0];

            SpearChainMaterial.mainTextureOffset = new Vector2(1 , entity.Read<ShengbiaoWeapon>().Offset);
        }
    }
}