using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.Skills.Systems.Solaray {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class CriticalHitSkillSystem : ISystemFilter {
        
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
            
            return Filter.Create("Filter-CriticalHitSystem")
                .With<CriticalHitAffect>()
                .With<ActivateSkill>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner(out var owner).Avatar();
            if (avatar.IsAlive() == false) return;
            ref var rightWeapon = ref avatar.Get<WeaponEntities>().RightWeapon;
            
            rightWeapon.Get<ModifierConfig>().Value = entity.Read<ProjectileConfig>().Value;
            rightWeapon.Get<CriticalHitModifier>().LifeTime = entity.Read<SkillDurationDefault>().Value;

            _vfx.SpawnVFX(entity.Read<VFXConfig>().Value, avatar, entity.Read<SkillDurationDefault>().Value);
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
        }
    }
}