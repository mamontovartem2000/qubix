﻿using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;

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
			if (world.HasSharedData<MapInitialized>())
			{
				Entity timerEntity = new Entity("Timer");
				timerEntity.Get<GameTimer>().Value = Consts.Main.GAME_TIMER_SECONDS;
				world.RemoveSharedData<MapInitialized>();
			}
		}

		void IUpdate.Update(in float deltaTime) { }
	}
}