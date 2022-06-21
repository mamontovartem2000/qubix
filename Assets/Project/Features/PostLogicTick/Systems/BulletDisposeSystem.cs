using ME.ECS;
using Project.Common.Components;
using Project.Features.VFX;
using Project.Modules.Network;

namespace Project.Features.PostLogicTick.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class BulletDisposeSystem : ISystemFilter
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
			return Filter.Create("Filter-BulletDisposeSystem")
				.With<ProjectileActive>()
				.With<Collided>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			if (entity.TryReadCollided(out var from, out var owner) == false) return;
			ref readonly var damage = ref entity.Read<ProjectileDamage>().Value;

			var pos = owner.GetPosition();

			if (owner.Has<PlayerAvatar>())
			{
				var player = owner.Avatar();
				pos = player.GetPosition();
				
				if (NetworkData.FriendlyFireCheck(from.Read<PlayerTag>().Team, owner.Read<PlayerTag>().Team))
				{
					var collision = new Entity("collision");
					collision.Get<LifeTimeLeft>().Value = 2;

					if (entity.Has<StunModifier>())
					{
						_vfx.SpawnVFX(VFXFeature.VFXType.SkillStun, pos, player, entity.Read<StunModifier>().Value);
						player.Set(new Stun { Value = entity.Read<StunModifier>().Value });
					}
					collision.Set(new ApplyDamage { ApplyTo = player, ApplyFrom = from, Damage = damage }, ComponentLifetime.NotifyAllSystems);
				}
			}

			if (owner.Has<DestructibleTag>())
			{
				owner.Get<PlayerHealth>().Value -= damage;
			}
			
			_vfx.SpawnVFX(VFXFeature.VFXType.BulletWallVFX, pos);

			entity.Destroy();
		}
	}
}