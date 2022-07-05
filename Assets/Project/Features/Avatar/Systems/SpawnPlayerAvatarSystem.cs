using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;

namespace Project.Features.Avatar.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class SpawnPlayerAvatarSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private AvatarFeature _feature;
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
        }

        void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-SpawnPlayerAvatarSystem")
                .With<PlayerTag>()
                .Without<PlayerAvatar>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var time = ref entity.Get<RespawnTime>().Value;
            time -= deltaTime;
            
            if (time <= 0)
            { 
                entity.Get<PlayerAvatar>().Value = _feature.SpawnPlayerAvatar(entity);
                entity.Get<RespawnTime>().Value = Consts.Main.RESPAWN_TIME;
            }
            
            if(entity.Has<SkillEntities>()) return;
            
            _feature.SpawnSkills(entity);
        }
    }
}