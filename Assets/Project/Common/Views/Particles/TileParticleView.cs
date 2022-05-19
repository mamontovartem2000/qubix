using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;

namespace Project.Common.Views.Particles
{
	public class TileParticleView : ParticleViewSource<ApplyTileParticleViewStateParticle> {}
	[System.Serializable]
	public class ApplyTileParticleViewStateParticle : ParticleView<ApplyTileParticleViewStateParticle>
	{
		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyStateJob(float deltaTime, bool immediately)
		{
			ref var rootData = ref this.GetRootData();
			rootData.position = entity.GetPosition() - new fp3(0, 0.3, 0);
			rootData.rotation3D = entity.GetRotation().eulerAngles;
			
			if (entity.Has<BridgeTile>())
			{
				var hor = entity.Read<BridgeTile>().Value;

				var rot = new fp3(0, hor ? 90 : 0, 0);
				var off = new fp3(hor ? 1 : 0.5, 0, hor ? 0.5 : 1);

				rootData.position += off;
				rootData.rotation3D += rot;
			}
			
			rootData.startSize = 1f;
			this.SetRootData(ref rootData);
		}
		
		public override void ApplyState(float deltaTime, bool immediately) {}
	}
}