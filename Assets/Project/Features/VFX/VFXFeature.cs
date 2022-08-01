using FlatBuffers;
using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX.Systems;
using UnityEngine;

namespace Project.Features.VFX
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class VFXFeature : Feature
	{
		protected override void OnConstruct()
		{
			AddSystem<PlayerHoverSystem>();
			AddSystem<TileGlowSystem>();
		}

		protected override void OnDeconstruct() {}
		
		public Entity SpawnVFX(DataConfig vfxConfig, Entity parent, float lifeTime)
		{
			var fx = new Entity("vfx");

			if(!parent.IsAlive()) return fx;
			
			vfxConfig.Apply(fx);

			fx.Get<LifeTimeLeft>().Value = lifeTime;
			fx.SetPosition(parent.GetPosition());
			fx.SetRotation(parent.GetRotation());
			fx.SetParent(parent);
			
			var _viewId = world.RegisterViewSource(fx.Read<ViewModel>().Value);
			fx.InstantiateView(_viewId);
			
			return fx;
		}

		public void SpawnVFX(DataConfig vfxConfig, Vector3 position)
		{
			if (vfxConfig == null) return;
			
			var fx = new Entity("vfx");
			vfxConfig.Apply(fx);

			fx.Get<LifeTimeLeft>().Value = Consts.Main.DEFAULT_LIFETIME;
			fx.SetPosition(position);

			var _viewId = world.RegisterViewSource(fx.Read<ViewModel>().Value);
			fx.InstantiateView(_viewId);
		}
	}
}