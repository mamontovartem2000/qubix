﻿using ME.ECS;
using ME.ECS.Views.Providers;

namespace Project.Common.Views.VFX.Particle
{
    public class PlayerTakeDamageParticle : ParticleViewSource<ApplyPlayerTakeDamageParticleStateParticle> { }

    [System.Serializable]
    public class ApplyPlayerTakeDamageParticleStateParticle : ParticleView<ApplyPlayerTakeDamageParticleStateParticle>
    {
        public override void OnInitialize() { }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(float deltaTime, bool immediately)
        {
            ref var rootData = ref this.GetRootData();
            rootData.position = entity.GetPosition();
            rootData.startSize = 1f;
            this.SetRootData(ref rootData);
        }
        public override void ApplyState(float deltaTime, bool immediately) { }
    }
}