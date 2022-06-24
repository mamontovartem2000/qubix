﻿using ME.ECS;
using ME.ECS.Views.Providers;

namespace Project.Common.Views.Particles
{
	public class ObjectParticleView : ParticleViewSource<ApplyObjectParticleViewStateParticle> {}
	[System.Serializable]
	public class ApplyObjectParticleViewStateParticle : ParticleView<ApplyObjectParticleViewStateParticle>
	{
		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyStateJob(float deltaTime, bool immediately)
		{
			ref var rootData = ref this.GetRootData();
			rootData.position = entity.GetPosition() - new fp3(0, 0.3, 0);
			rootData.rotation3D = entity.GetRotation().eulerAngles;
			rootData.startSize = 1f;
			this.SetRootData(ref rootData);
		}

		public override void ApplyState(float deltaTime, bool immediately) {}
	}
}