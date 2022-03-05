using ME.ECS;
using DG.Tweening;
using UnityEngine;

namespace Project.Features.SceneBuilder.Views 
{
    using ME.ECS.Views.Providers;
    public class HealthMono : MonoBehaviourView 
    {
        public override bool applyStateJob => true;
        public override void OnInitialize()
        {
            transform.DORotate(new Vector3(0f, 180f, 0f), 2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
        }
        public override void OnDeInitialize() {}
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime,bool immediately){}
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
        }
    }
}