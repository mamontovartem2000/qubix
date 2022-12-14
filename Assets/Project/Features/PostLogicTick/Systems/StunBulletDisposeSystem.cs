using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;
using Project.Modules.Network;

namespace Project.Features.PostLogicTick.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class StunBulletDisposeSystem : ISystemFilter
    {
        private PostLogicTickFeature _feature;
        private VFXFeature _vfx;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);
        }

        void ISystemBase.OnDeconstruct()
        {
        }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-StunBulletDisposeSystem")
                .With<ProjectileActive>()
                .With<Collided>()
                .With<StunModifier>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.TryReadCollided(out var from, out var owner) == false) return;

            if (!owner.Has<PlayerAvatar>()) return;
            
            if (from.Read<TeamTag>().Value == owner.Read<TeamTag>().Value) return;
            
            var avatar = owner.Avatar();
            
            // if (avatar.Has<Stun>())
            // {
            //     avatar.Read<Stun>().VFXEntity.Get<LifeTimeLeft>().Value = entity.Read<StunModifier>().LifeTime;
            // }
            // else
            // {
            //     avatar.Get<Stun>().VFXEntity = _vfx.SpawnVFX(entity.Read<StunModifier>().VFXConfig, avatar, entity.Read<StunModifier>().LifeTime);
            // }
            
            avatar.Get<Stun>().LifeTime = entity.Read<StunModifier>().LifeTime;
            avatar.Read<WeaponEntities>().LeftWeapon.Remove<LeftWeaponShot>();
            avatar.Read<WeaponEntities>().LeftWeapon.Remove<LinearActive>();
            avatar.Read<WeaponEntities>().RightWeapon.Remove<RightWeaponShot>();
            avatar.Remove<DashModifier>();
        }
    }
}