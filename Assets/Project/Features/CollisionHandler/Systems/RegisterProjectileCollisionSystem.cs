using ME.ECS;
using Project.Features.Player.Components;
using Project.Features.Projectile.Components;
using UnityEngine;

namespace Project.Features.CollisionHandler.Systems {
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
    public sealed class RegisterProjectileCollisionSystem : ISystemFilter
    {
        public World world { get; set; }

        private Filter _playerFilter;
        private CollisionHandlerFeature feature;

        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this.feature);
        
            Filter.Create("PlayerFilter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);
        }
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-DetectCollisionSystem")
                .With<ProjectileTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var player in _playerFilter)
            {
                var playerId = player.Read<PlayerTag>().PlayerID;
                var projectileId = entity.Read<ProjectileTag>().ActorID;
                var playerPos = player.GetPosition();

                if ((entity.GetPosition() - playerPos).sqrMagnitude <= 1f)
                {
                    if (playerId == projectileId) return;

                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {Damage = entity.Read<ProjectileDamage>().Value, ApplyTo = player},ComponentLifetime.NotifyAllSystems);
                    entity.Set(new ProjectileShouldDie(), ComponentLifetime.NotifyAllSystems);

                    // collision.Set(new CollisionTag {Collision = entity, Player = player}, ComponentLifetime.NotifyAllSystems);
                    // player.Set(new CollisionTag {Collision = entity}, ComponentLifetime.NotifyAllSystems);
                }
            }
        }
    }
}