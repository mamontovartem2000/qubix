using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.VFX;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Features.PostLogicTick.Systems {

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class EMPBulletDisposeSystem : ISystemFilter {
        
        private PostLogicTickFeature _feature;
        private VFXFeature _vfx;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this._feature);
            world.GetFeature(out _vfx);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-EMPBulletDisposeSystem")
                .With<ProjectileActive>()
                .With<Collided>()
                .With<EMPModifier>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.TryReadCollided(out var from, out var owner) == false) return;

            if (!owner.Has<PlayerAvatar>()) return;
            
            if (from.Read<TeamTag>().Value == owner.Read<TeamTag>().Value) return;
            
            ref var skills = ref owner.Get<SkillEntities>();
            var avatar = owner.Avatar();
            
            world.GetFeature<EventsFeature>().EMPActive.Execute(owner);

            // if (avatar.Has<EMP>()) 
            // {
            //     avatar.Read<EMP>().VFXEntity.Get<LifeTimeLeft>().Value = entity.Read<EMPModifier>().LifeTime;
            // }
            // else
            // {
            //     avatar.Get<EMP>().VFXEntity = _vfx.SpawnVFX(entity.Read<EMPModifier>().VFXConfig, avatar, entity.Read<EMPModifier>().LifeTime);
            // }

            skills.FirstSkill.Get<EMP>().LifeTime = entity.Read<EMPModifier>().LifeTime;
            skills.SecondSkill.Get<EMP>().LifeTime = entity.Read<EMPModifier>().LifeTime;
            skills.ThirdSkill.Get<EMP>().LifeTime = entity.Read<EMPModifier>().LifeTime;
            skills.FourthSkill.Get<EMP>().LifeTime = entity.Read<EMPModifier>().LifeTime;
        }
    }
}