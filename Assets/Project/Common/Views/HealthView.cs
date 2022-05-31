using ME.ECS;
using Project.Common.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Common.Views
{
    using ME.ECS.Views.Providers;

    public class HealthView : MonoBehaviourView
    {
        public Image Fill;
        public Image Overlay;
        
        public override bool applyStateJob => true;
        public override void OnInitialize() {}
        public override void OnDeInitialize() {}
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {}
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition() + new Vector3(0f, 0.8f, 0f);

            var fill = entity.GetParent().Read<PlayerHealthDefault>().Value;
            
            Fill.fillAmount = entity.GetParent().Read<PlayerHealth>().Value / fill;
            Overlay.fillAmount = entity.Read<PlayerHealthOverlay>().Value / fill;
        }
    }
}