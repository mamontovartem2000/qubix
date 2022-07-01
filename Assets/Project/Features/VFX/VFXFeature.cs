using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
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
		public MonoBehaviourViewBase[] Views;
		private ViewId[] _viewIds;
		
		protected override void OnConstruct()
		{
			AddSystem<VFXPlayerFollowingSystem>();
			AddSystem<PlayerHoverSystem>();
			AddSystem<TileGlowSystem>();
			
			_viewIds = new ViewId[Views.Length];
			
			for (int i = 0; i < Views.Length; i++)
			{
				_viewIds[i] = world.RegisterViewSource(Views[i]);
			}
		}

		protected override void OnDeconstruct() {}
		
		public void SpawnVFX(VFXType type, Vector3 position, Entity player)
		{
			if(!player.IsAlive()) return;
			
			var fx = new Entity("vfx");
			fx.Get<LifeTimeLeft>().Value = 4;
			fx.Set(new Owner { Value = player.Owner() });
			fx.SetLocalPosition(position);
			fx.SetParent(player);
			fx.InstantiateView(_viewIds[(int)type]);
		}

		public void SpawnVFX(VFXType type, Vector3 position)
		{
			var fx = new Entity("vfx");
			fx.Get<LifeTimeLeft>().Value = 4;
			fx.SetLocalPosition(position);
			fx.InstantiateView(_viewIds[(int)type]);
		}
		
		public enum VFXType
		{
			BulletExplosion,
			BulletWallVFX,
			MinigunMuzzle,
			MingunShoot,
			PlayerDeath,
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
			GrenadeVFX,
			EMPExplosion,
			SkillDash,
			SkillEMPBullets
		}
	}
}