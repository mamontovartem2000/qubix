using ME.ECS;
using Project.Common.Components;

namespace Project.Features.CollisionHandler.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class NewPortalDispenserSystem : ISystemFilter
	{
		public World world { get; set; }
		
		private CollisionHandlerFeature _feature;

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
			return Filter.Create("Filter-NewPortalDispenserSystem")
				.With<PortalDispenserTag>()
				.Without<SpawnedPortal>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref var dispenser = ref entity.Get<PortalDispenserTag>();
			dispenser.Timer -= deltaTime;

			if (dispenser.Timer <= 0)
			{
				var portal = _feature.SpawnPortal(entity);
				portal.SetPosition(entity.GetPosition());
				// SceneUtils.ReleasePortal(portal.GetPosition());

				dispenser.Timer = dispenser.TimerDefault;
				entity.Get<SpawnedPortal>().Value = portal;
			}
		}
	}
}