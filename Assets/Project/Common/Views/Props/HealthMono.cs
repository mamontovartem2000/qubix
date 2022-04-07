using DG.Tweening;
using ME.ECS;
using UnityEngine;

namespace Dima.Scripts
{
    using ME.ECS.Views.Providers;
    public class HealthMono : MonoBehaviourView
    {
        public override bool applyStateJob => true;

        public override void OnInitialize()
        {
            transform.DORotate(new Vector3(0f, 360f, 0f), 3f, RotateMode.WorldAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
        }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
        }
    }
}