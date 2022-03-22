using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Core.Features.Player.Components;
using UnityEngine;

namespace Project.Core.Features.Player.Systems {
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
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
            entity.Get< ApplyDamage>().ApplyTo.Get<PlayerHealth>().Value -= entity.Get<ApplyDamage>().Damage;
            entity.Get<ApplyDamage>().ApplyTo.Get<LastHit>().Enemy = entity.Read<ApplyDamage>().From;

            entity.Set(new ApplyDamage(), ComponentLifetime.NotifyAllSystems);
            
            var booooool = entity.Has<ApplyDamage>();
            Debug.Log(booooool); // false
            
            entity.Set(new ApplyDamage(), ComponentLifetime.NotifyAllSystemsBelow);

            
            world.GetFeature<EventsFeature>().HealthChanged.Execute(entity.Get<ApplyDamage>().ApplyTo);
        }
    }
}