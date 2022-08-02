using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;

namespace Project.Features.GameState.Systems 
{
#region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class RunTimerSystem : ISystemFilter 
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
            if (entity.Read<GameTimer>().Value > 0)
            {
                entity.Get<GameTimer>().Value -= deltaTime;
                world.GetFeature<EventsFeature>().TimerTick.Execute(entity);
            }
            else
            {
                world.SetSharedData(new EndOfGameStage(), ComponentLifetime.NotifyAllSystems);
            }
        }
    }
}