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
			if (entity.TryReadCollided(out var from, out var owner) == false) return;
			ref var player = ref owner.Get<PlayerAvatar>().Value;

			var collision = new Entity("collision");
			collision.Get<LifeTimeLeft>().Value = 2;
			
			ref readonly var damage = ref entity.Read<MineDamage>().Value;
			collision.Set(new ApplyDamage {ApplyTo = player, ApplyFrom = from, Damage = damage}, ComponentLifetime.NotifyAllSystems);
			var sound = new Entity("lomixBombSound");
			sound.SetPosition(entity.GetPosition());
			sound.Get<LifeTimeLeft>().Value = 3;
			SoundUtils.PlaySound(sound, "event:/Explosions/Grenade");
			SceneUtils.ModifyFree(entity.GetPosition(), true);
			_vfx.SpawnVFX(entity.Read<VFXConfig>().Value, entity.GetPosition());

			entity.Destroy();
		}
	}
}