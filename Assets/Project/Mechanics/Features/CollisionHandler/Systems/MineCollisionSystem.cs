using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Mechanics.Features.VFX;

namespace Project.Mechanics.Features.CollisionHandler.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class MineCollisionSystem : ISystemFilter
	{
		public World world { get; set; }

		private CollisionHandlerFeature _feature;
		private VFXFeature _vfx;

		private Filter _trapFilter;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			world.GetFeature(out _vfx);

			Filter.Create("trap-filter")
				.With<MineTag>()
				.Push(ref _trapFilter);
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
			return Filter.Create("Filter-RegisterTrapCollisionSystem")
				.With<AvatarTag>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
		{
			foreach (var collectible in _trapFilter)
			{
				if ((entity.GetPosition() - collectible.GetPosition()).sqrMagnitude <= SceneUtils.ItemRadius)
				{
					if (entity.Get<Owner>().Value.Has<DamagedBy>())
						entity.Get<Owner>().Value.Remove<DamagedBy>();

					var collision = new Entity("collision");
					collision.Set(new ApplyDamage {ApplyTo = entity, Damage = 25f}, ComponentLifetime.NotifyAllSystems);

					_vfx.SpawnVFX(VFXFeature.VFXType.Explosion, collectible.GetPosition());
					collectible.Destroy();
				}
			}
		}
	}
}