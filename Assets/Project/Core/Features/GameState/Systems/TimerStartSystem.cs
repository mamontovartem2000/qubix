using ME.ECS;
using Project.Common.Components;
using Project.Modules.Network;

namespace Project.Core.Features.GameState.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

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

				if (NetworkData.BuildType == BuildTypes.PC)
				{
					timerEntity.Get<GameTimer>().Value = 150;
				}
				else if (NetworkData.BuildType == BuildTypes.Front)
				{
					timerEntity.Get<GameTimer>().Value = 15;
				}

				world.RemoveSharedData<MapInitialized>();
			}
		}

		void IUpdate.Update(in float deltaTime) { }
	}
}