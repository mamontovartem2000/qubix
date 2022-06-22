using ME.ECS;
using ME.ECS.Views.Providers;
using Unity.Mathematics;
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

	public struct TileAlternativeView : IComponent
	{
		public MonoBehaviourView Value;
	}
	public struct TileRotation : IComponent
	{
		public float3 Value;
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
		public float Value;
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

	public struct GlowTile : IComponent
	{
		public bool Direction;
		public float Amount;
	}
}