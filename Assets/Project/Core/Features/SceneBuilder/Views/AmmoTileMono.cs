using ME.ECS;
using ME.ECS.Views.Providers;

namespace Project.Core.Features.SceneBuilder.Views 
{
    public class AmmoTileMono : MonoBehaviourView 
    {
        public override bool applyStateJob => true;
        public override void OnInitialize() {}
        public override void OnDeInitialize() {}

        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
        }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
        }
    }
}