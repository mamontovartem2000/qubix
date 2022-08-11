using ME.ECS;
using Project.Common.Components;

namespace Project.Features.GameState.Systems
{
    public sealed class TimerStartSystem : ISystem, IAdvanceTick, IUpdate
	{
		public World world { get; set; }

		private GameStateFeature _feature;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
		}

		void ISystemBase.OnDeconstruct() { }

		void IAdvanceTick.AdvanceTick(in float deltaTime)
		{
			if (!world.HasSharedData<MapInitialized>()) return;
			
			if (world.HasSharedData<GameWithoutStages>())
			{
				CreateGameTimer(world.ReadSharedData<GameWithoutStages>().Time);
			}
			else if (world.HasSharedData<GameStage>())
			{
				CreateGameTimer(world.ReadSharedData<GameStage>().Time);
			}
			else return;
				
			world.RemoveSharedData<MapInitialized>();
		}
		
		private void CreateGameTimer(float time)
		{
			var timerEntity = new Entity("Timer");
			timerEntity.Set(new GameTimer {Value = time});
		}

		void IUpdate.Update(in float deltaTime) { }
	}
}