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
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var apply = ref entity.Get<ApplyDamage>();
            var from = apply.ApplyFrom;
            var to = apply.ApplyTo;
            var damage = apply.Damage;

            Debug.Log(from.ToSmallString());
            
            if (from.Has<PlayerAvatar>())
            {
                to.Get<Owner>().Value.Set(new DamagedBy {Value = from});
            }

            ref var health = ref to.Get<PlayerHealth>().Value;

            if(apply.ApplyFrom.Has<StunModifier>())
                apply.ApplyTo.Set(new Stun{Value = 1});

            if (apply.ApplyTo.Has<ForceShieldModifier>() && apply.ApplyTo.Get<ForceShieldModifier>().Value - damage >= 0)
            {
                apply.ApplyTo.Get<ForceShieldModifier>().Value -= damage;
            }
            else if (apply.ApplyTo.Has<ForceShieldModifier>() && apply.ApplyTo.Get<ForceShieldModifier>().Value - damage < 0)
            {
                apply.ApplyTo.Get<ForceShieldModifier>().Value -= damage;
                damage = -apply.ApplyTo.Get<ForceShieldModifier>().Value;
                health -= damage;
            }
            else health -= damage;

            if (apply.ApplyTo.Get<PlayerHealth>().Value < 0)
            {
                apply.ApplyTo.Get<PlayerHealth>().Value = 0;
            }
            else if(apply.ApplyTo.Get<PlayerHealth>().Value > to.Read<PlayerHealthDefault>().Value)
            {
                apply.ApplyTo.Get<PlayerHealth>().Value = to.Read<PlayerHealthDefault>().Value;
            }
            
            world.GetFeature<EventsFeature>().HealthChanged.Execute(apply.ApplyTo.Read<Owner>().Value);
        }
    }
}