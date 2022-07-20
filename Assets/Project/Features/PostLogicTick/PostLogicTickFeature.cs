using ME.ECS;
using Project.Common.Components;
using Project.Features.PostLogicTick.Systems;

namespace Project.Features.PostLogicTick
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
			//Modifier
			AddSystem<StormDisposeSystem>();
			AddSystem<StunBulletDisposeSystem>();
			AddSystem<EMPBulletDisposeSystem>();
			AddSystem<FreezeBulletDisposeSystem>();
			AddSystem<CritBulletDisposeSystem>();
			
			//Gun
			AddSystem<BulletDisposeSystem>();
			AddSystem<ShengbiaoBulletDisposeSystem>();
			
			//Bullet hit
			AddSystem<BulletHitAvatarSystem>();
			AddSystem<BulletHitDestructubleSystem>();
			AddSystem<BulletHitNonDestructubleSystem>();
			
			//Bullet destroy
			AddSystem<BulletDestroySystem>();
			AddSystem<LinearDestroySystem>();
			
			//World systems
			AddSystem<HealthDisposeSystem>();
			AddSystem<MineDisposeSystem>();
			AddSystem<PortalDisposeSystem>();

			AddSystem<DestroyEntitySystem>();
		}

		protected override void OnDeconstruct() {}
	}
}