using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.PostLogicTick.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class PortalDisposeSystem : ISystemFilter
	{
		public World world { get; set; }

		private PostLogicTickFeature _feature;
		private VFXFeature _vfx;

		private Filter _portalFilter;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			world.GetFeature(out _vfx);

			Filter.Create("portal-Filter")
				.With<PortalDispenserTag>()
				.With<Spawned>()
				.Push(ref _portalFilter);
		}

		void ISystemBase.OnDeconstruct()
		{
		}
#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;
		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }

		Filter ISystemFilter.CreateFilter()
		{
			return Filter.Create("Filter-PortalDisposeSystem")
				.With<PortalTag>()
				.With<Collided>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			if (entity.TryReadCollided(out var from, out var owner) == false) return;
			var player = owner.Avatar();

			if (player == Entity.Empty || player.Has<AvoidTeleport>())
			{
				entity.Remove<Collided>();
				return;
			}

			var pos = SceneUtils.GetRandomPortal(player.GetPosition());
			SceneUtils.ModifyWalkable(player.GetPosition(), true);
			SceneUtils.ModifyWalkable(pos, false);
			
			player.Get<AvoidTeleport>().Value = GameConsts.Main.AVOID_TELEPORT_SECONDS;

			player.SetPosition(pos);
			player.Get<PlayerMoveTarget>().Value = pos;
			entity.Remove<Collided>();

			// _vfx.SpawnVFX(VFXFeature.VFXType.TelerortInVFX, entity.GetPosition());
			// _vfx.SpawnVFX(VFXFeature.VFXType.TeleportOutVFX, pos);
		}
	}
}