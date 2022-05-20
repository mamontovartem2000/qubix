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
}