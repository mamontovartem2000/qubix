using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Mechanics.Features.VFX.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class PlayerHoverSystem : ISystemFilter
	{
		private VFXFeature _feature;
		public World world { get; set; }
		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out this._feature);
		}
		void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;
		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }
		Filter ISystemFilter.CreateFilter()
		{
			return Filter.Create("Filter-HealthRotateSystem")
				.With<AvatarTag>()
				.With<Hover>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref var up = ref entity.Get<Hover>().Direction;
			ref var amount = ref entity.Get<Hover>().Amount;

			amount += up ? 0.005 : -0.005;
			if (amount >= 0.1)
			{
				up = false;
			}
			else if (amount <= 0.0)
			{
				up = true;
			}
		}
	}
}