using ExitGames.Client.Photon.StructWrapping;
using ME.ECS;
using Project.Features.CollisionHandler.Components;
using UnityEngine;

namespace Project.Features.Player.Systems {
    #region usage

    

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
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
        private EventsFeature _events;

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
            entity.Get<ApplyDamage>().ApplyTo.Get<PlayerHealth>().Value -= entity.Get<ApplyDamage>().Damage;
            entity.Get<ApplyDamage>().ApplyTo.Get<LastHit>().Enemy = entity.Read<ApplyDamage>().From;
            
            _events.HealthChanged.Execute(entity.Get<ApplyDamage>().ApplyTo);
        }
    }
}