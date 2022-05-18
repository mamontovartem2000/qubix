using ME.ECS;
using Project.Common.Components;

namespace Project.Common.Views
{
	using ME.ECS.Views.Providers;
	public class PropsParticleView : ParticleViewSource<ApplyPropsParticleViewStateParticle> {}
	[System.Serializable]
	public class ApplyPropsParticleViewStateParticle : ParticleView<ApplyPropsParticleViewStateParticle>
	{
		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyStateJob(float deltaTime, bool immediately)
		{
			ref var rootData = ref this.GetRootData();
			rootData.position = entity.GetPosition() - new fp3(0, 0.3, 0);
			rootData.rotation3D = entity.GetRotation().eulerAngles;
			
			if (entity.Has<TileRotation>())
			{
				var rot = entity.Read<TileRotation>().Value;
				rootData.rotation3D += rot;
			}
			
			rootData.startSize = 1f;
			this.SetRootData(ref rootData);
		}

		public override void ApplyState(float deltaTime, bool immediately) {}
	}
}