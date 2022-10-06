using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.Skills.Systems.Lomix {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class BackBlinkSkillSystem : ISystemFilter {
        
        private SkillsFeature _feature;
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
            
            return Filter.Create("Filter-TeleportPlantSkillSystem")
                .With<BackBlinkAffect>()
                .With<ActivateSkill>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner().Avatar();
            if (avatar.IsAlive() == false) return;

            var nextPos = -avatar.Read<FaceDirection>().Value * 5 + (avatar.Read<PlayerMoveTarget>().Value);

            if (!SceneUtils.IsWalkable(new fp3(nextPos.x, 0, nextPos.z))) return;
            
            _vfx.SpawnVFX(entity.Read<VFXConfig>().Value, avatar.GetPosition());
            
            SceneUtils.ModifyWalkable(avatar.Read<PlayerMoveTarget>().Value, true);
            SceneUtils.ModifyWalkable(new fp3(nextPos.x, 0, nextPos.z), false);

            SoundUtils.PlaySound(avatar, "event:/VFX/TeleportIn");
            avatar.SetPosition(nextPos);
            SoundUtils.PlaySound(avatar, "event:/VFX/TeleportOut");
            avatar.Get<PlayerMoveTarget>().Value = new fp3(nextPos.x, 0, nextPos.z);

            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
        }
    }
}