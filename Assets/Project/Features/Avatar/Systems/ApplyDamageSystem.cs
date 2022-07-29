using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Common.Utilities;
using Project.Features.Player;
using Project.Features.VFX;
using UnityEngine;

namespace Project.Features.Avatar.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class ApplyDamageSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private AvatarFeature _feature;        
        private VFXFeature _vfx;


        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _vfx);

        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-ApplyDamageSystem")
                .With<ApplyDamage>()
                .WithoutShared<GameFinished>()
                .Push();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var apply = ref entity.Get<ApplyDamage>();
            var from = apply.ApplyFrom;
            var to = apply.ApplyTo;
            var damage = apply.Damage;
            ref var health = ref to.Get<PlayerHealth>().Value;

            if (to.Has<ForceShieldModifier>()) return;
            
            if (from.Has<PlayerAvatar>())
            {
                if (from.Read<TeamTag>().Value == to.Owner().Read<TeamTag>().Value) return;
                
                to.Owner().Set(new DamagedBy {Value = from});
                from.Avatar().Get<PrivateSoundPath>().Value = "event:/Weapons/HitMarker";
                Worlds.current.GetFeature<EventsFeature>().PlaySoundPrivate.Execute(from.Avatar());
            }

            // if (health/apply.ApplyTo.Read<PlayerHealthDefault>().Value < 0.65f) // TODO: hueta move this
            // {
            //     
            //     to.Get<PlayerDamagedCounter>().Value += 0.1f;
            // }
            
            // to.Set(new PlayerDamaged { Value = 1f });
            
            health -= damage;
            // _vfx.SpawnVFX(to.Owner().Read<VFXConfig>().SecondaryValue, to, 2);

            var dealtDamage = damage;

            if (health <= 0)
            {
                to.Owner().Set(new PlayerDead { DeathPosition = to.Read<PlayerMoveTarget>().Value }, ComponentLifetime.NotifyAllSystemsBelow);
                dealtDamage += health;
                health = 0;
            }

            from.Get<PlayerScore>().DealtDamage += dealtDamage;
            
            world.GetFeature<EventsFeature>().TabulationScreenNumbersChanged.Execute(from);
            world.GetFeature<EventsFeature>().TabulationScreenNewPlayerStats.Execute(from);

            world.GetFeature<EventsFeature>().HealthChanged.Execute(to.Owner());
        }
    }
}