using ME.ECS;
using Project.Common.Components;
using Project.Features.VFX;

namespace Project.Features.Skills.Systems.GoldHunter
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class BlinkSkillSystem : ISystemFilter
    {
        public World world { get; set; }

        private SkillsFeature _feature;
        private VFXFeature _vfx;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _vfx);
        }

        void ISystemBase.OnDeconstruct() { }
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-DashSkillSystem")
                .With<BlinkAffect>()
                .With<ActivateSkill>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner().Avatar();
            if (avatar.IsAlive() == false) return;

            var nextPos = avatar.Read<FaceDirection>().Value * 4 + (avatar.Read<PlayerMoveTarget>().Value);

            if (!SceneUtils.IsWalkable(new fp3(nextPos.x, 0, nextPos.z))) return;
            
            // SceneUtils.Move(entity.Read<Owner>().Value.Read<PlayerAvatar>().Value.Read<PlayerMoveTarget>().Value, new fp3(nextPos.x, 0, nextPos.z));
            SceneUtils.ModifyWalkable(avatar.Read<PlayerMoveTarget>().Value, true);
            SceneUtils.ModifyWalkable(new fp3(nextPos.x, 0, nextPos.z), false);

            SoundUtils.PlaySound(avatar, "event:/VFX/TeleportIn");
            avatar.SetPosition(nextPos);
            SoundUtils.PlaySound(avatar, "event:/VFX/TeleportOut");
            avatar.Get<PlayerMoveTarget>().Value = new fp3(nextPos.x, 0, nextPos.z);

            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
            
            _vfx.SpawnVFX(VFXFeature.VFXType.PlayerTelerortIn, avatar.GetPosition(), avatar);
        }

    }
}