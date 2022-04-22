using ME.ECS;

namespace Project.Core.Features.GameState.Components
{
	public struct GamePaused : IComponent {}

	public struct GameFinished : IComponent { }

	public struct GameTimer : IComponent
	{
		public float Value;
	}
	
	public struct MapInitialized : IComponent {}

}