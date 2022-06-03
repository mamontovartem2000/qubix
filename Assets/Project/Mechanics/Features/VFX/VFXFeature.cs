using System;
using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Mechanics.Features.VFX.Systems;
using UnityEngine;

namespace Project.Mechanics.Features.VFX
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class VFXFeature : Feature
	{
		public MonoBehaviourViewBase[] Views;
		// public MonoBehaviourViewBase[] Destructibles;
		private ViewId[] _viewIds;//, _destructibleIds; 

		protected override void OnConstruct()
		{
			AddSystem<VFXPlayerFollowingSystem>();
			AddSystem<PlayerHoverSystem>();
			
			_viewIds = new ViewId[Views.Length];
			// _destructibleIds = new ViewId[Destructibles.Length];
			
			for (int i = 0; i < Views.Length; i++)
			{
				_viewIds[i] = world.RegisterViewSource(Views[i]);
			}

			// for (int i = 0; i < Destructibles.Length; i++)
			// {
			// 	_destructibleIds[i] = world.RegisterViewSource(Destructibles[i]);
			// }
		}

		protected override void OnDeconstruct() {}

		public void SpawnVFX(VFXType type, Vector3 position, Entity player, float lifeTime)
		{
			if(!player.IsAlive()) return;
			
			var fx = new Entity("vfx");
			fx.Get<LifeTimeLeft>().Value = 5;
			fx.Set(new LifeTimeLeft{Value = lifeTime});
			fx.Set(new Owner { Value = player.Read<Owner>().Value });
			fx.SetLocalPosition(position);
			fx.SetLocalRotation(player.GetRotation());
			fx.SetParent(player);
			fx.InstantiateView(_viewIds[(int)type-1]);
		}
		public void SpawnVFX(VFXType type, Vector3 position, Entity player)
		{
			if(!player.IsAlive()) return;
			
			var fx = new Entity("vfx");
			fx.Get<LifeTimeLeft>().Value = 5;
			fx.Set(new Owner { Value = player.Read<Owner>().Value });
			fx.SetLocalPosition(position);
			fx.SetParent(player);
			fx.InstantiateView(_viewIds[(int)type-1]);
		}

		public void SpawnVFX(VFXType type, Vector3 position)
		{
			var fx = new Entity("vfx");
			fx.Get<LifeTimeLeft>().Value = 5;
			fx.SetLocalPosition(position);
			fx.InstantiateView(_viewIds[(int)type-1]);
		}
		
		public enum VFXType
		{
			BulletExplosion,
			BulletWallVFX,
			MinigunMuzzle,
			MingunShoot,
			PlayerDeath,
			PlayerDeath2,
			PlayerFire,
			PlayerShield,
			PlayerTakeDamage,
			PlayerTelerortIn,
			PlayerTeleportOut,
			ShotgunMuzzle,
			TakeHealth,
			SkillBlink,
			SkillCurcuitBurts,
			SkillHeal,
			SkillQuickdraw,
			SkillSlow,
			StatusStunEffect,
			SkillStun,
			SkillOffenciveBurst,
			SkillQuickness,
			SkillLinearPower,
			QubixDeath,
			SlowExplosion,
			SpeedTrail,
			MineExplosion,
			FUCK
		}
	}
}