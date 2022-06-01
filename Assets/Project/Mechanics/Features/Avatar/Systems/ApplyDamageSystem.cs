using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Core.Features.Player;
using UnityEngine;

namespace Project.Mechanics.Features.Avatar.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class ApplyDamageSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private PlayerFeature _feature;

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
            return Filter.Create("Filter-ApplyDamageSystem")
                .With<ApplyDamage>()
                .Without<ForceShieldModifier>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var apply = ref entity.Get<ApplyDamage>();
            var from = apply.ApplyFrom;
            var to = apply.ApplyTo;
            var damage = apply.Damage;
            ref var health = ref to.Get<PlayerHealth>().Value;
            
            if (from.Has<PlayerAvatar>())
            {
                to.Get<Owner>().Value.Set(new DamagedBy {Value = from});
            }

            ref var health = ref to.Get<PlayerHealth>().Value;
            
            if(damage > 0)
                apply.ApplyTo.Set(new PlayerDamaged {Value = 0.1f});

            health -= damage;

            if (to.Read<PlayerHealth>().Value <= 0)
            {
                to.Get<PlayerHealth>().Value = 0;
            }
            else if(to.Read<PlayerHealth>().Value > to.Read<PlayerHealthDefault>().Value)
            {
                to.Get<PlayerHealth>().Value = to.Read<PlayerHealthDefault>().Value;
            }

            world.GetFeature<EventsFeature>().HealthChanged.Execute(to.Read<Owner>().Value);
        }
    }
}