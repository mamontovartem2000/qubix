using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;

namespace Project.Common.Views.Bullets.Mono
{
    public class RpgGrenadeMono : MonoBehaviourView
    {
        public override bool applyStateJob => true;
        public override void OnInitialize() { }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            // transform.rotation = entity.GetRotation();
            transform.forward = entity.Read<ProjectileDirection>().Value;
        }
    }
}