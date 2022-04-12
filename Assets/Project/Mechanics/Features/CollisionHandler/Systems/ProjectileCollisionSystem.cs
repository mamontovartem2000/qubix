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
        private Filter _projectileFilter;
        private Filter _meleeFilter;
        private Filter _linearFilter;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            Filter.Create("Projectile-Filter")
                .With<ProjectileActive>()
                .Push(ref _projectileFilter);
            
            Filter.Create("Melee-Filter")
                // .With<Melee>()
                .With<MeleeActive>()
                .Push(ref _meleeFilter);
           
            Filter.Create("Linear-Filter")
                .With<LinearActive>()
                .Push(ref _linearFilter);
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
                .With<PlayerTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var projectile in _projectileFilter)
            {
                if ((entity.GetPosition() - projectile.GetPosition()).sqrMagnitude <= SceneUtils.PlayerRadius)
                {
                    var damage = projectile.Read<ProjectileDamage>().Value;
                    var from = projectile.Read<ProjectileActive>().Player;
                    
                    if (projectile.Read<ProjectileActive>().Player.Read<PlayerTag>().PlayerID == entity.Read<PlayerTag>().PlayerID) return;
                    
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {ApplyTo = entity, ApplyFrom = from, Damage = damage}, ComponentLifetime.NotifyAllSystems);

                    projectile.Destroy();
                }
            }

            foreach (var melee in _meleeFilter)
            {
                if ((entity.GetPosition() - melee.GetPosition()).sqrMagnitude <= SceneUtils.PlayerRadius)
                {
                    var damage = melee.Read<ProjectileDamage>().Value;
                    var from = melee.Read<MeleeActive>().Player;
                    
                    if (from.Read<PlayerTag>().PlayerID == entity.Read<PlayerTag>().PlayerID) return;
                    
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {ApplyTo = entity, ApplyFrom = from, Damage = damage}, ComponentLifetime.NotifyAllSystems);
                }
            }
            
            foreach (var linear in _linearFilter)
            {
                if ((entity.GetPosition() - linear.GetPosition()).sqrMagnitude <= SceneUtils.PlayerRadius)
                {
                    var damage = linear.Read<ProjectileDamage>().Value;
                    var from = linear.Read<LinearActive>().Player;
                    
                    if (linear.Read<LinearActive>().Player.Read<PlayerTag>().PlayerID == entity.Read<PlayerTag>().PlayerID) return;
                    
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage {ApplyTo = entity, ApplyFrom = from, Damage = damage}, ComponentLifetime.NotifyAllSystems);
                }
            }
        }
    }
}