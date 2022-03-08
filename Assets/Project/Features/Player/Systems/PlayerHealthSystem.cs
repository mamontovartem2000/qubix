using ME.ECS;
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
    public sealed class PlayerHealthSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private PlayerFeature _feature;
        private SceneBuilderFeature _builder;
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out _feature);
            world.GetFeature(out _builder);
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
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var currentHealth = entity.Read<PlayerHealth>().Value;

            if (currentHealth > 0) return;

            if (entity.Has<LastHit>() && entity.Get<LastHit>().Enemy.IsAlive())
            {
                entity.Get<LastHit>().Enemy.Get<PlayerScore>().Value += 5;
                world.GetFeature<EventsFeature>().ScoreChanged.Execute(entity.Read<LastHit>().Enemy);
            }
            
            var deadBody = new Entity("deadBody");
            deadBody.Set(new DeadBody {ActorID = entity.Read<PlayerTag>().PlayerID, Time = 5.5f});
            entity.Get<PlayerScore>().Value -= 5;

            deadBody.SetAs<PlayerScore>(entity);
            
            _builder.MoveTo(_builder.PositionToIndex(entity.GetPosition()), 0);
            world.GetFeature<EventsFeature>().Respawn.Run(entity);
            
            entity.Destroy();
        }
    }
}