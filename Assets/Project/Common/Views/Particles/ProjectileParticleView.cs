using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views.Particles
{
	public class ProjectileParticleView : ParticleViewSource<ApplyProjectileParticleViewStateParticle> {}
	[System.Serializable]
	public class ApplyProjectileParticleViewStateParticle : ParticleView<ApplyProjectileParticleViewStateParticle>
	{
		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyStateJob(float deltaTime, bool immediately) {}
		public override void ApplyState(float deltaTime, bool immediately)
		{
			ref var rootData = ref this.GetRootData();

			var offset = Vector3.Cross(entity.Read<ProjectileDirection>().Value, Vector3.up) * 0.35f;

			rootData.position = entity.GetPosition() - offset;
			rootData.rotation3D = entity.Read<ProjectileDirection>().Value;
			rootData.startSize = 1f;
			this.SetRootData(ref rootData);
		}
	}
}