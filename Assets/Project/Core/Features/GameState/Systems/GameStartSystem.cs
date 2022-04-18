using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.GameState.Components;

namespace Project.Core.Features.GameState.Systems
{
	#region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

	#endregion

	public sealed class GameStartSystem : ISystem, IAdvanceTick, IUpdate
	{
		public World world { get; set; }

		private GameStateFeature _feature;
		private Filter _playerFilter;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			Filter.Create("Player Filter")
				.With<PlayerTag>()
				.WithShared<GamePaused>()
				.Push(ref _playerFilter);
		}

		void ISystemBase.OnDeconstruct()
		{
		}

		void IAdvanceTick.AdvanceTick(in float deltaTime)
		{
			if (!world.HasSharedData<GamePaused>()) return;
			if (world.HasSharedData<GameFinished>()) return;

			var entity = new Entity("test");

			// entity.SetShared(new PlayerTag(), 0);

			// world.GetFeature<EventsFeature>().AllPlayersReady.Execute(world.GetFeature<PlayerFeature>().GetActivePlayer());
			// world.GetFeature<PlayerFeature>().ForceStart();

			//            if(timer != 0) return;
		}

		void IUpdate.Update(in float deltaTime)
		{
		}
	}
}