using ME.ECS;

namespace Project.Features.Components
{
	public struct GamePaused : IComponent {}

	public struct GameTimer : IComponent
	{
		public float Value;
	}
	
	public struct Initialized : IComponent {}
}