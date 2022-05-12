using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Mechanics.Features.Lifetime.Systems
{
	#region usage

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

	#endregion

	public sealed class LinearVisualLifeTimeSystem : ISystemFilter
	{
		public World world { get; set; }
		private LifeTimeFeature _feature;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
		}
		void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;
		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }

		Filter ISystemFilter.CreateFilter()
		{
			return Filter.Create("Filter-NewLinearVisualsSystem")
				.With<LinearVisual>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			if (entity.GetParent().Has<LinearActive>()) return;
			entity.Destroy();
		}
	}
}