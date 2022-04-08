using ME.ECS;
using Project.Common.Components;
using Project.Core.Features;
using Project.Core.Features.Player.Components;

namespace Project.Mechanics.Features.CollisionHandler.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class ProjectileCollisionSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private CollisionHandlerFeature _feature;
        private Filter _playerFilter;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            Filter.Create("Players-Filter")
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
            return Filter.Create("Filter-ProjectileCollisionSystem")
                .With<ProjectileActive>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var damage = entity.Read<ProjectileDamage>().Value;
            var from = entity.Read<ProjectileActive>().Player;
            
            foreach (var player in _playerFilter)
            {
                if ((entity.GetPosition() - player.GetPosition()).sqrMagnitude <= SceneUtils.PlayerRadius)
                {
                    if (entity.Read<ProjectileActive>().Player.Read<PlayerTag>().PlayerID == player.Read<PlayerTag>().PlayerID) return;
                    
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {ApplyTo = player, ApplyFrom = from, Damage = damage}, ComponentLifetime.NotifyAllSystems);

                    _feature.SpawnVFX(entity.GetPosition(), _feature.HealID, _feature.DefaultTimer);                    
                    entity.Destroy();
                }
            }   
        }
    }
}