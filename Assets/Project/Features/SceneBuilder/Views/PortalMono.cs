using DG.Tweening;
using ME.ECS;
using UnityEngine;

namespace Project.Features.SceneBuilder.Views
{

    using ME.ECS.Views.Providers;

    public class PortalMono : MonoBehaviourView
    {
        [SerializeField] private Renderer _mat;
        public override bool applyStateJob => true;
        private SceneBuilderFeature _feature;

        public Transform _portal;

        public override void OnInitialize()
        {
            world.GetFeature(out _feature);
        }

        public override void OnDeInitialize() { }

        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            var mat = _feature._glowMat;

            _mat.sharedMaterial = mat;
        }
    }
}