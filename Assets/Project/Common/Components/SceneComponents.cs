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

	public struct TileAlternativeView : IComponent
	{
		public MonoBehaviourView Value;
	}
	public struct TileRotation : IComponent
	{
		public fp3 Value;
	}

	public struct BridgeTile : IComponent
	{
		public bool IsHorizontal;
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

	public struct FreeAndWalkableMap : IComponent
	{
		public byte FreeMapValue;
		public byte WalkableMapValue;
	}
	
	public struct TileName : IComponent
	{
		public string Value;
	}

	public struct GlowTile : IComponent
	{
		public bool Direction;
		public Vector2 AmountRange;
		[HideInInspector] public float Amount;
	}

	public struct GlowTileRandomAmount : IComponent
	{
		public float Amount;
	}
	
}