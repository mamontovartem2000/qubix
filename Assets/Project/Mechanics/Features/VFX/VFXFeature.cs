using System;
using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
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
		private ViewId[] _viewIds; 

		protected override void OnConstruct()
		{
			_viewIds = new ViewId[Views.Length];

			for (int i = 0; i < Views.Length; i++)
			{
				_viewIds[i] = world.RegisterViewSource(Views[i]);
			}
		}

		protected override void OnDeconstruct() {}

		public void SpawnVFX(VFXType type, Vector3 position)
		{
			var fx = new Entity("vfx");
			fx.Get<LifeTimeLeft>().Value = 3;
			fx.SetLocalPosition(position);
			fx.InstantiateView(_viewIds[type.GetHashCode()]);

			// switch (type)
			// {
			// 	case VFXType.BulletExplosion:
			// 		fx.InstantiateView(_viewIds[type.GetHashCode()]);
			// 		break;
			// 	case VFXType.BulletTrail:
			// 		break;
			// 	case VFXType.BulletWallVFX:
			// 		break;
			// 	case VFXType.FlameThrowerVFX:
			// 		break;
			// 	case VFXType.MinigunMuzzle:
			// 		break;
			// 	case VFXType.MingunShoot:
			// 		break;
			// 	case VFXType.Plasmabeam:
			// 		break;
			// 	case VFXType.PlayerDeath:
			// 		break;
			// 	case VFXType.PlayerFire:
			// 		break;
			// 	case VFXType.PlayerShield:
			// 		break;
			// 	case VFXType.PlayerTakeDamage:
			// 		break;
			// 	case VFXType.PlayerTeleport:
			// 		break;
			// 	case VFXType.PlayerTelerortIn:
			// 		break;
			// 	case VFXType.PlayerTeleportOut:
			// 		break;
			// 	case VFXType.RPGTrail:
			// 		break;
			// 	case VFXType.ShotgunMuzzle:
			// 		break;
			// 	case VFXType.SlowWallExplosion:
			// 		break;
			// 	case VFXType.TakeHealth:
			// 		break;
			// }
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
			SkillLinearPower
			
		}
	}
}