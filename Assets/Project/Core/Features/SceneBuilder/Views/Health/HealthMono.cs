using DG.Tweening;
using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Core.Features.SceneBuilder.Views.Health 
{
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