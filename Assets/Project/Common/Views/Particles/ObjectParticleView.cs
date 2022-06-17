using ME.ECS;
using ME.ECS.Views.Providers;
using Unity.Mathematics;
using UnityEngine;

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
			rootData.position = entity.GetPosition() - new float3(0, 0.3f, 0);
			
				var blah = (Quaternion)entity.GetRotation();
				rootData.rotation3D = blah.eulerAngles;

			rootData.startSize = 1f;
			this.SetRootData(ref rootData);
		}

		public override void ApplyState(float deltaTime, bool immediately) {}
	}
}