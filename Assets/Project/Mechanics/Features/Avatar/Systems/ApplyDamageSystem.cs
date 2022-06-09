﻿using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Core.Features.Player;
using Unity.Mathematics;
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
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var apply = ref entity.Get<ApplyDamage>();
            var from = apply.ApplyFrom;
            var to = apply.ApplyTo;
            var damage = apply.Damage;
            ref var health = ref to.Get<PlayerHealth>().Value;
            
            if (to.Has<ForceShieldModifier>() && damage > 0) return;
            
            if (from.Has<PlayerAvatar>())
            {
                to.Get<Owner>().Value.Set(new DamagedBy {Value = from});
            }

            if (damage > 0)
            {
                if (health/apply.ApplyTo.Read<PlayerHealthDefault>().Value < 0.65)
                {
                    apply.ApplyTo.Set(new PlayerDamaged {Value = 1f});
                    apply.ApplyTo.Get<PlayerDamagedCounter>().Value += 0.1f;
                }
            }
            
            health -= damage;
            
            if (to.Read<PlayerHealth>().Value <= 0)
            {
                damage += to.Read<PlayerHealth>().Value;
                to.Get<PlayerHealth>().Value = 0;
            }
            else if(to.Read<PlayerHealth>().Value - damage > to.Read<PlayerHealthDefault>().Value)
            {
                to.Get<PlayerHealth>().Value = to.Read<PlayerHealthDefault>().Value;
            }

            if (damage > 0)
            {
                from.Get<PlayerScore>().DealtDamage += damage;
                world.GetFeature<EventsFeature>().TabulationScreenNumbersChanged.Execute(from);
                world.GetFeature<EventsFeature>().TabulationScreenNewPlayerStats.Execute(from);
            }
            
            world.GetFeature<EventsFeature>().HealthChanged.Execute(to.Read<Owner>().Value);
        }
    }
}