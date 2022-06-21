using ME.ECS;
using Project.Common.Components;

namespace Project.Features.LifeTime.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class DestructibleHealthSystem : ISystemFilter
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
			return Filter.Create("Filter-DestructibleHealthSystem")
				.With<DestructibleTag>()
				.With<PlayerHealth>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref var hp = ref entity.Get<PlayerHealth>().Value;

			if (hp > 0) return;
			
			ref readonly var view = ref entity.Read<DestructibleView>().Value;
			var v = world.RegisterViewSource(view);

			var dVfx = new Entity("dVfx");
			dVfx.Get<LifeTimeLeft>().Value = entity.Read<DestructibleLifeTime>().Value;
				
			dVfx.SetPosition(entity.GetPosition());
			dVfx.InstantiateView(v);
				
			SceneUtils.ModifyWalkable(entity.GetPosition(), true);
				
			entity.Destroy();
		}
	}
}