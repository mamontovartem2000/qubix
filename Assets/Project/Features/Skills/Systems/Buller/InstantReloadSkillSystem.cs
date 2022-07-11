using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.Skills.Systems.Buller
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class InstantReloadSkillSystem : ISystemFilter
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
            return Filter.Create("Filter-InstantReloadSkillSystem")
                .With<InstantReloadAffect>()
                .With<ActivateSkill>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner().Avatar();
            if (avatar.IsAlive() == false) return;
            
            ref var weapon = ref avatar.Get<WeaponEntities>();

            weapon.LeftWeapon.Get<AmmoCapacity>().Value = weapon.LeftWeapon.Read<AmmoCapacityDefault>().Value;
            weapon.RightWeapon.Get<ReloadTime>().Value = Consts.Skills.INSTANT_RELOAD_TIME;
            
            world.GetFeature<EventsFeature>().RightWeaponDepleted.Execute(entity.Get<Owner>().Value);
            
            SoundUtils.PlaySound(avatar, "event:/Skills/Buller/Quickdraw");
            
			entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
            
            _vfx.SpawnVFX(entity.Read<VFXConfig>().Value, avatar.GetPosition());
        }
    }
}