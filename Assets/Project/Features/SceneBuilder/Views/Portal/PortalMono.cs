using ME.ECS;
using UnityEngine;

namespace Project.Features.SceneBuilder.Views
{
    using ME.ECS.Views.Providers;

    public class PortalMono : MonoBehaviourView
    {
        [SerializeField] private Renderer _mat;
        [SerializeField] private Transform _portal; //TODO: Видимо, удалить.
        [SerializeField] private Material _glowMat;
        private SceneBuilderFeature _feature;

        public override void OnInitialize()
        {
            world.GetFeature(out _feature);
        }

        public override void OnDeInitialize() { }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            var mat = _glowMat;

            _mat.sharedMaterial = mat;
        }

        public void ChangeColorGlowingMaterial()
        {
            var rnd = world.GetRandomRange(1, 6);
            switch (rnd)
            {
                case 1:
                    _glowMat.SetColor("_EmissionColor", Color.blue);
                    break;
                case 2:
                    _glowMat.SetColor("_EmissionColor", Color.cyan);
                    break;
                case 3:
                    _glowMat.SetColor("_EmissionColor", Color.green);
                    break;
                case 4:
                    _glowMat.SetColor("_EmissionColor", Color.magenta);
                    break;
                case 5:
                    _glowMat.SetColor("_EmissionColor", Color.red);
                    break;
                case 6:
                    _glowMat.SetColor("_EmissionColor", Color.yellow);
                    break;
            }
        }
    }
}