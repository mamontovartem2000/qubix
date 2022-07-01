using ME.ECS;
using Project.Common.Components;
using Project.Features.VFX;
using UnityEngine;

namespace Project.Features.Skills.Systems.Silen {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class DashSystem : ISystemFilter {
        
        private SkillsFeature feature;
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
            
            return Filter.Create("Filter-DashSystem")
                .With<DashAffect>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner().Avatar();
            if (avatar.IsAlive() == false) return;
            
            avatar.SetPosition(avatar.Read<PlayerMoveTarget>().Value);
            avatar.Remove<Slowness>();
            avatar.Set(new DashModifier());
            
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;

            _vfx.SpawnVFX(VFXFeature.VFXType.SkillDash, avatar.GetPosition(), avatar);
        }
    
    }
    
}