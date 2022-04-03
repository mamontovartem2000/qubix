using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.Projectile.Views
{
    using ME.ECS.Views.Providers;

    public class BulletMono : MonoBehaviourView
    {
        public override bool applyStateJob => true;

        public override void OnInitialize() { }

        public override void OnDeInitialize() { }

        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();

            // if (!entity.Has<Linear>())
            //     transform.rotation = entity.GetRotation();
        }
    }
}