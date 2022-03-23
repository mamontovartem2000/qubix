using ME.ECS;
using Project.Features.Components;
using Project.Features.Projectile.Components;

namespace Project.Features.Player.Systems
{
    #region usage



#pragma warning disable
    using Components;
    using Markers;
    using Modules;
    using Project.Components;
    using Project.Markers;
    using Project.Modules;
    using Project.Systems;
    using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class PlayerHealthSystem : ISystemFilter 
    {
        public World world { get; set; }
        private SceneBuilderFeature _scene;
        private CollisionHandlerFeature _coll;
        
        void ISystemBase.OnConstruct() {
            
            world.GetFeature(out _scene);
            world.GetFeature(out _coll);
        }

        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-PlayerHealthSystem")
                .With<PlayerHealth>()
                .WithoutShared<GameFinished>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var currentHealth = entity.Read<PlayerHealth>().Value;

            if (currentHealth > 0) return;

            if (entity.Has<LastHit>())
            {
                Entity enemy = entity.Read<LastHit>().Enemy;

                if (enemy.IsAlive())
                {
                    enemy.Get<PlayerScore>().Kills += 1;
                    world.GetFeature<EventsFeature>().PlayerKill.Execute(enemy);
                }           
            }
            
            _coll.SpawnVFX(entity.GetPosition(), _coll._deathID, _coll._deathTimer); //TODO: Вынести анимации и подключать через ивенты.
            
            var deadBody = new Entity("deadBody");
            deadBody.Set(new DeadBody {ActorID = entity.Read<PlayerTag>().PlayerID, Time = 5.5f});
            entity.Get<PlayerScore>().Deaths += 1;
            deadBody.SetAs<PlayerScore>(entity);
            
            _scene.MoveTo(_scene.PositionToIndex(entity.Read<PlayerMoveTarget>().Value), 0);
            world.GetFeature<EventsFeature>().PlayerDeath.Run(entity);      
            entity.Destroy();
        }
    }
}