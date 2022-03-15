using ME.ECS;
using Project.Features.Components;
using UnityEngine;

namespace Project.Features.GameState.Systems 
{
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
    public sealed class GameRunSystem : ISystemFilter 
    {
        private GameStateFeature _feature;
        public World world { get; set; }
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
            return Filter.Create("Filter-GameRunSystem")
                .With<GameTimer>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Get<GameTimer>().Value - deltaTime > 0)
            {
                entity.Get<GameTimer>().Value -= deltaTime;
            }
            else
            {
                world.GetFeature<EventsFeature>().OnGameFinished.Execute();
                world.SetSharedData(new GameFinished {Timer = 2f});
                world.SetSharedData(new GamePaused());
            }
        }
    }
}