using ME.ECS;
using Project.Common.Components;
using ME.ECS.DataConfigs;
using UnityEngine;

namespace Project.Mechanics.Features.CollisionHandler.Systems {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Project.Core;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GrenadeExplosionSystem : ISystemFilter {
        
        private CollisionHandlerFeature feature;
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
            
            return Filter.Create("Filter-GrenadeExplosionSystem")
                .With<Grenade>()
                .Push();
            
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            if (entity.GetPosition().y < 0f)
            {
                foreach (Entity player in _playerFilter)
                {
                    if ((player.GetPosition() - entity.GetPosition()).sqrMagnitude > (fp)9) continue;
                    Debug.Log("hit");
                    
                    var debuff = new Entity("debuff");
                    var index = SceneUtils.PositionToIndex(player.GetPosition());
                    var pos = SceneUtils.IndexToPosition(index);
                    debuff.Get<Owner>().Value = entity.Read<Owner>().Value;
                    entity.Read<SecondaryDamage>().Value.Apply(debuff);
                    debuff.Set(new ProjectileActive());
                    debuff.Set(new CollisionDynamic());
                    debuff.SetPosition(pos);
                }
                entity.Destroy();
            }
        }
    }
}