using ME.ECS;

namespace Project.Features.Components
{
	public struct GamePaused : IComponent {}

	public struct GameFinished : IComponent
	{
		public float Timer;
	}

	public struct GameTimer : IComponent
	{
		public float Value;
	}
	
	public struct Initialized : IComponent {}

	public struct EndGame : IComponent
	{
		public bool Winner;
	}
}