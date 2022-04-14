using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features;
using Project.Core.Features.Player.Components;
using UnityEngine;

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
        private Filter _damageSourceFilter;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            Filter.Create("Projectile-Filter")
                .With<DamageSource>()
                // .With<ProjectileActive>()
                .Push(ref _damageSourceFilter);
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
                .With<AvatarTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var source in _damageSourceFilter)
            {
                if ((entity.GetPosition() - source.GetPosition()).sqrMagnitude <= SceneUtils.PlayerRadiusSQR)
                {
                    var damage = source.Read<ProjectileDamage>().Value;
                    var from = source.Get<Owner>().Value;

                    if (from.Read<PlayerTag>().PlayerID != entity.Read<Owner>().Value.Read<PlayerTag>().PlayerID)
                    {
                        var collision = new Entity("collision");
                        collision.Set(new ApplyDamage {ApplyTo = entity, ApplyFrom = from, Damage = damage}, ComponentLifetime.NotifyAllSystems);
                
                        if(source.Has<ProjectileActive>())
                            source.Destroy();
                    }
                }
            }
        }
    }
}