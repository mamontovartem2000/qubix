using DG.Tweening;
using ME.ECS;
using ME.ECS.Views.Providers;

namespace Project.Core.Features.Player.Views
{
    public class PlayerView : MonoBehaviourView
    {
        public override bool applyStateJob => true;

        public override void OnInitialize()
        {
        }

        public override void OnDeInitialize() {}

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();      
        }
    }
}