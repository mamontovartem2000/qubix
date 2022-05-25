using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Mechanics.Features.VFX;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Mechanics.Features.PostLogicTick.Systems
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
			ref var owner = ref entity.Get<Collided>().ApplyTo;
			ref var from = ref entity.Get<Collided>().ApplyFrom;
			ref readonly var damage = ref entity.Read<ProjectileDamage>().Value;

			var pos = owner.GetPosition();

			if (owner.Has<PlayerAvatar>())
			{
				ref var player = ref owner.Get<PlayerAvatar>().Value;
				pos = player.GetPosition();

				if (owner.Read<PlayerTag>().Team != from.Read<PlayerTag>().Team || NetworkData.Team == string.Empty)
				{
					var collision = new Entity("collision");
					if (entity.Has<StunModifier>())
					{
						player.Set(new Stun { Value = entity.Read<StunModifier>().Value });
					}
					collision.Set(new ApplyDamage { ApplyTo = player, ApplyFrom = from, Damage = damage }, ComponentLifetime.NotifyAllSystems);
				}
			}
			
			_vfx.SpawnVFX(VFXFeature.VFXType.BulletWallVFX, pos);
			entity.Destroy();
		}
	}
}