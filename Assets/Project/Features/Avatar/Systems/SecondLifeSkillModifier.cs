using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;
using UnityEngine;

namespace Project.Features.Avatar.Systems {

    #pragma warning disable
#pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SecondLifeReset : ISystemFilter {
        
        private AvatarFeature feature;
        private VFXFeature _vfx;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
            world.GetFeature(out _vfx);

        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-SecondLifeSkillModifier")
                .With<PlayerTag>()
                .With<PlayerDead>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Avatar();
            if (!entity.Read<SkillEntities>().SecondSkill.Has<SecondLifeAffect>()) return;
            if (entity.Read<SkillEntities>().SecondSkill.Has<Cooldown>()) return;

            entity.Remove<PlayerDead>();
            avatar.Get<PlayerHealth>().Value = 30f;
            _vfx.SpawnVFX(entity.Read<SkillEntities>().SecondSkill.Read<VFXConfig>().Value, avatar, 2f);
            avatar.Set(new SecondLifeModifier());
        }
    }
}