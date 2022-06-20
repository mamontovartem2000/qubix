using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.CollisionHandler.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class MineBlinkSystem : ISystemFilter
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
			return Filter.Create("Filter-MineBlinkSystem")
				.With<MineBlinkTimer>()
				.With<MineBlinkTimerDefault>()
				.Without<MineBlink>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref var time = ref entity.Get<MineBlinkTimer>().Value;
			ref readonly var defaultTime = ref entity.Read<MineBlinkTimerDefault>().Value;
			time -= deltaTime;
			
			if(time > 0) return;
			
			entity.Get<MineBlink>().Value = 0.15f;
			time = defaultTime;
		}
	}
}