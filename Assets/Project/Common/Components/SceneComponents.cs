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
		public MonoBehaviourViewBase Value;
	}

	public struct TileRotation : IComponent
	{
		public Vector3 Value;
	}
}