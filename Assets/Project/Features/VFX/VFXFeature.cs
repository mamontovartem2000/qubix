﻿using FlatBuffers;
using ME.ECS;
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
		// public MonoBehaviourViewBase[] Views;
		// private ViewId[] _viewIds;
		
		protected override void OnConstruct()
		{
			AddSystem<PlayerHoverSystem>();
			AddSystem<TileGlowSystem>();
			
			// _viewIds = new ViewId[Views.Length];
			//
			// for (int i = 0; i < Views.Length; i++)
			// {
			// 	_viewIds[i] = world.RegisterViewSource(Views[i]);
			// }
		}

		protected override void OnDeconstruct() {}
		
		public void SpawnVFX(VFXType type, Vector3 position, Entity player)
		{
			if(!player.IsAlive()) return;
			//
			// var fx = new Entity("vfx");
			// fx.Get<LifeTimeLeft>().Value = 4;
			// fx.Set(new Owner { Value = player.Owner() });
			// fx.SetPosition(position);
			// fx.SetRotation(player.GetRotation());
			// fx.SetParent(player);
			// fx.InstantiateView(_viewIds[(int)type]);
		}

		public void SpawnVFX(Entity parent)
		{
			var fx = new Entity("vfx");
			parent.Read<VFXConfig>().Value.Apply(fx);

			fx.Get<LifeTimeLeft>().Value = 4;
			fx.SetPosition(parent.GetPosition());
			
			var _viewId = world.RegisterViewSource(fx.Read<ViewModel>().Value);
			fx.InstantiateView(_viewId);
		}
		
		public void SpawnVFX(Entity parent, Vector3 position)
		{
			var fx = new Entity("vfx");
			parent.Read<VFXConfig>().Value.Apply(fx);

			fx.Get<LifeTimeLeft>().Value = 4;
			fx.SetPosition(position);

			var _viewId = world.RegisterViewSource(fx.Read<ViewModel>().Value);
			fx.InstantiateView(_viewId);
		}
		
		public enum VFXType
		{
			BulletExplosion,
			BulletHitWallVFX,
			MinigunMuzzle,
			MingunShoot,
			PlayerDeath,
			PlayerFire,
			HardShieldVFX,
			PlayerTakeDamage,
			TelerortInVFX,
			TeleportOutVFX,
			ShotgunMuzzle,
			HealVFX,
			SkillBlink,
			SkillReloadVFX,
			SkillHealVFX,
			SkillGunsReloadVFX,
			SkillSlow,
			SkillStunVFX,
			StunEffect,
			SkillMoreAmmoVFX,
			SkillSpeedIncreaseVFX,
			SkillLinearPowerIncreaceVFX,
			QubixDeathVFX,
			SlowGrenadeExplosionVFX,
			SpeedTrailVFX,
			MineExplosionVFX,
			AssaultGrenadeExplosionVFX,
			EMPGrenadeExplosionVFX,
			SkillDashVFX,
			SkillEMPBulletsVFX,
			EMPVFX,
			FreezeGrenadeExplosionVFX
		}
	}
}