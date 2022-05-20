using ME.ECS;
using Project.Mechanics.Features.PostLogicTick.Systems;

namespace Project.Mechanics.Features.PostLogicTick
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class PostLogicTickFeature : Feature
	{
		protected override void OnConstruct()
		{
			// AddSystem<QuadTreeDispose>();
			AddSystem<BulletDisposeSystem>();
			//AddSystem<HealthDisposeSystem>();
			AddSystem<MineDisposeSystem>();
		}

		protected override void OnDeconstruct() {}
	}
}