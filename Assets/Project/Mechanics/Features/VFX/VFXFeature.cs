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
		public MonoBehaviourViewBase PlayerDeathVFX;
		public MonoBehaviourViewBase AutomaticMuzzleVFX;
		public MonoBehaviourViewBase ExplosionVFX;
		public MonoBehaviourViewBase HealthPickUpVFX;
		public MonoBehaviourViewBase PlayerImpactVFX;
		public MonoBehaviourViewBase PlayerDamagedStatusVFX;
		public MonoBehaviourViewBase TeleportVFX;
		public MonoBehaviourViewBase BulletWallVFX;

		private ViewId Death, Impact, Damage, Heal, Teleport, Explosion, Muzzle, BulletWall;
		
		protected override void OnConstruct()
		{
			Death = world.RegisterViewSource(PlayerDeathVFX);
			Impact = world.RegisterViewSource(PlayerImpactVFX);
			Damage = world.RegisterViewSource(PlayerDamagedStatusVFX);
			Heal = world.RegisterViewSource(HealthPickUpVFX);
			Teleport = world.RegisterViewSource(TeleportVFX);
			Explosion = world.RegisterViewSource(ExplosionVFX);
			Muzzle = world.RegisterViewSource(AutomaticMuzzleVFX);
			BulletWall = world.RegisterViewSource(BulletWallVFX);
		}

		protected override void OnDeconstruct() {}

		public void SpawnVFX(Entity parent, Vector3 direction)
		{
			var fx = new Entity("vfx");
			fx.SetParent(parent);
			fx.SetLocalPosition(new Vector3(0.20f,0.15f, 0.35f));
			fx.SetRotation(Quaternion.Euler(direction));
			fx.InstantiateView(Muzzle);
			fx.Get<LifeTimeLeft>().Value = 0.2f;
		}

		public void SpawnVFX(VFXType type, Vector3 position)
		{
			var fx = new Entity("vfx");
			fx.Get<LifeTimeLeft>().Value = 3;
			fx.SetLocalPosition(position);
			
			switch (type)
			{
				case VFXType.Death:
				{
					fx.InstantiateView(Death);
					break;
				}
				case VFXType.Impact:
				{
					fx.InstantiateView(Impact);
					break;
				}
				case VFXType.Damage:
				{
					fx.InstantiateView(Damage);
					break;
				}
				case VFXType.Heal:
				{
					fx.InstantiateView(Heal);
					break;
				}
				case VFXType.Teleport:
				{
					fx.InstantiateView(Teleport);
					break;
				}
				case VFXType.Explosion:
				{
					fx.InstantiateView(Explosion);
					break;
				}
				case VFXType.Muzzle:
				{
					fx.InstantiateView(Muzzle);
					fx.Get<LifeTimeLeft>().Value = 0.1f;					
					break;
				}
				case VFXType.BulletWall:
				{
					fx.InstantiateView(BulletWall);
					break;
				}
			}
		}
		
		public enum VFXType
		{
			Death, Impact, Damage, Heal, Teleport, Explosion, Muzzle, BulletWall
		}
	}
}