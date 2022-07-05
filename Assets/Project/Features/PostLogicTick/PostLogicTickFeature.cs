﻿using ME.ECS;
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
			//Modifier systems
			AddSystem<StormDisposeSystem>();
			AddSystem<StunBulletDisposeSystem>();
			AddSystem<EMPBulletDisposeSystem>();
			AddSystem<FreezeBulletDisposeSystem>();
			AddSystem<CritBulletDisposeSystem>();
			
			//Gun systems
			AddSystem<BulletDisposeSystem>();
			AddSystem<LinearDisposeSystem>();
			AddSystem<MeleeDisposeSystem>();

			//World systems
			AddSystem<HealthDisposeSystem>();
			AddSystem<MineDisposeSystem>();
			AddSystem<PortalDisposeSystem>();
		}

		protected override void OnDeconstruct() {}
	}
}