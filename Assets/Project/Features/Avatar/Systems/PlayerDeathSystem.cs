using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.Avatar.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PlayerDeathSystem : ISystemFilter 
    {
        public World world { get; set; }
        private VFXFeature _vfx;
        private Filter _ownerFilter;
        
        void ISystemBase.OnConstruct() 
        {
            world.GetFeature(out _vfx);
            Filter.Create("Filer-Owner")
                .With<PlayerTag>()
                .Push(ref _ownerFilter);
        }

        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-PlayerHealthSystem")
                .With<PlayerTag>()
                .With<PlayerDead>()
                .WithoutShared<GameFinished>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Avatar();
            
            if (entity.Has<DamagedBy>())
            {
                ref var enemy = ref entity.Get<DamagedBy>().Value;
                enemy.Get<PlayerScore>().Kills += 1;
                world.GetFeature<EventsFeature>().PlayerKill.Execute(enemy);
                world.GetFeature<EventsFeature>().TabulationScreenNumbersChanged.Execute(enemy);
                world.GetFeature<EventsFeature>().TabulationScreenNewPlayerStats.Execute(enemy);
                entity.Remove<DamagedBy>();
            }
            
            entity.Get<PlayerScore>().Deaths += 1;
            
            world.GetFeature<EventsFeature>().TabulationScreenNumbersChanged.Execute(entity);
            world.GetFeature<EventsFeature>().PlayerDeath.Execute(entity);      

            SceneUtils.ModifyWalkable(avatar.Read<PlayerMoveTarget>().Value, true);
            
            _vfx.SpawnVFX(entity.Read<VFXConfig>().Value, avatar.GetPosition());
            entity.Remove<PlayerAvatar>();
            avatar.Destroy();
        }
    }
}