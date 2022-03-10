using ME.ECS;
using Project.Features.Player.Components;

namespace Project.Features 
{
	#region usage

	

	 using Components; using Modules; using Systems; using Features; using Markers;
    using GameState.Components; using GameState.Modules; using GameState.Systems; using GameState.Markers;
    
    namespace GameState.Components {}
    namespace GameState.Modules {}
    namespace GameState.Systems {}
    namespace GameState.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
	#endregion
	public sealed class GameStateFeature : Feature {

        protected override void OnConstruct()
        {
	        AddSystem<GameOverSystem>();
        }

        protected override void OnDeconstruct() 
        {
        }
    }
}