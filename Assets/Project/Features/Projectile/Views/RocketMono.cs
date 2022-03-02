using ME.ECS;

namespace Project.Features.Projectile.Views 
{
    using ME.ECS.Views.Providers;
    public class RocketMono : MonoBehaviourView 
    {
        public override bool applyStateJob => true;
        public override void OnInitialize() {}
        public override void OnDeInitialize() {}
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {}
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.forward = entity.GetRotation().eulerAngles;
            transform.position = entity.GetPosition();      
        }
    }
}