using ME.ECS;
using UnityEngine;

namespace Assets.Dima.Scripts
{
    using ME.ECS.Views.Providers;

    public class WrenchMono : MonoBehaviourView
    {
        [SerializeField] private ParticleSystem _ps;

        public override bool applyStateJob => true;
        public override void OnInitialize() { }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetParent().GetRotation();
        }
        
        public void Play()
        {
            _ps.Play();
        }

        public void Stop()
        {
            _ps.Stop();
        }
    }
}