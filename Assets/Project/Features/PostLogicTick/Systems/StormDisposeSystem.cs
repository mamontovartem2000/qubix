using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Features.PostLogicTick.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class StormDisposeSystem : ISystemFilter {
        
        private PostLogicTickFeature feature;
        private Filter _playerFilter;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
            Filter.Create("Player-Filter")
                .With<AvatarTag>()
                .Push(ref _playerFilter);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-StormDisposeSystem")
                .With<Storm>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var player in _playerFilter)
            {
                if ((player.GetPosition() - entity.GetPosition()).sqrMagnitude > entity.Read<ExplodeSquaredRadius>().Value) continue;

                if (player.Owner() == entity.Owner()) continue;
                
                var debuff = new Entity("debuff");
                debuff.Get<Owner>().Value = entity.Owner();
                entity.Read<SecondaryDamage>().Value.Apply(debuff);
                debuff.Set(new CollisionDynamic());
                debuff.Set(new LifeTimeLeft { Value = 0.5f});

                debuff.SetPosition(player.GetPosition());
            }
        }
    }
}