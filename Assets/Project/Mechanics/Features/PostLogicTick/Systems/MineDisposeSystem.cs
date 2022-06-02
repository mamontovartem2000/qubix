﻿using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Mechanics.Features.VFX;
using UnityEngine;

namespace Project.Mechanics.Features.PostLogicTick.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class MineDisposeSystem : ISystemFilter
	{
		public World world { get; set; }
		
		private PostLogicTickFeature _feature;
		private VFXFeature _vfx;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			world.GetFeature(out _vfx);
		}
		void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;
		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }
		Filter ISystemFilter.CreateFilter()
		{
			return Filter.Create("Filter-MineDisposeSystem")
				.With<MineTag>()
				.With<Collided>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			ref var owner = ref entity.Get<Collided>().ApplyTo;
			ref var player = ref owner.Get<PlayerAvatar>().Value;
			ref var from = ref entity.Get<Collided>().ApplyFrom;
			
			if (owner.Has<DamagedBy>())
				owner.Remove<DamagedBy>();

			var collision = new Entity("collision");
			collision.Set(new ApplyDamage {ApplyTo = player, ApplyFrom = from, Damage = 25f}, ComponentLifetime.NotifyAllSystems);

			_vfx.SpawnVFX(VFXFeature.VFXType.MineExplosion, entity.GetPosition());
			SceneUtils.ReleaseMine(entity.GetPosition());
			entity.Destroy();
		}
	}
}