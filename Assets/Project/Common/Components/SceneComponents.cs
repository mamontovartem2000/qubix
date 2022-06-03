using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Common.Components
{
	public struct MapConstruct : IComponent 
	{
		public TextAsset _sourceMap;
	}
	public struct TileView : IComponent
	{
		public ParticleViewSourceBase Value;
	}

	public struct TileRotation : IComponent
	{
		public fp3 Value;
	}

	public struct BridgeTile : IComponent
	{
		public bool Value;
	}
	
	public struct Pallette : IComponent {}

	public struct DestructibleTag : IComponent {}
	public struct DestructibleView : IComponent
	{
		public MonoBehaviourViewBase Value;
	}
	public struct DestructibleLifeTime : IComponent
	{
		public fp Value;
	}

	public struct MineBlink : IComponent
	{
		public float Value;
	}

	public struct MineBlinkTimer : IComponent
	{
		public float Value;
	}
	
	public struct MineBlinkTimerDefault : IComponent
	{
			public float Value;
	}

	public struct MineDamage : IComponent
	{
		public float Value;
	}
}