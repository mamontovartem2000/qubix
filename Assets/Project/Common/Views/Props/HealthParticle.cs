using ME.ECS;
using UnityEngine;

namespace Project.Common.Views
{
    using ME.ECS.Views.Providers;

    public class HealthParticle : ParticleViewSource<ApplyHealthParticleStateParticle> { }

    [System.Serializable]
    public class ApplyHealthParticleStateParticle : ParticleView<ApplyHealthParticleStateParticle>
    {
        public override void OnInitialize() {}
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(float deltaTime, bool immediately)
        {
            ref var rootData = ref GetRootData();
            rootData.position = entity.GetPosition() - new Vector3(0f, 0.5f, 0f);
            rootData.startSize = 1f;
            SetRootData(ref rootData);
        }
        public override void ApplyState(float deltaTime, bool immediately) { }
    }
}