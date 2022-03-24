using ME.ECS;
using Project.Features.Components;
using Project.Features.Player.Components;
using Project.Features.Player.Markers;
using Project.Features.SceneBuilder.Components;
using Project.Utilities;
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
    public sealed class GameStartSystem : ISystem, IAdvanceTick, IUpdate 
    {
        public World world { get; set; }
        
        private GameStateFeature _feature;
        private Filter _playerFilter;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this._feature);
            Filter.Create("Player Filter")
                .With<PlayerTag>()
                .WithShared<GamePaused>()
                .Push(ref _playerFilter);
        }
        
        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if(!world.HasSharedData<GamePaused>()) return;
            if(world.HasSharedData<GameFinished>()) return;
            if (!AllPlayersReady()) return;
            
            world.GetFeature<EventsFeature>().AllPlayersReady.Execute(world.GetFeature<PlayerFeature>().GetActivePlayer());
            // world.GetFeature<PlayerFeature>().ForceStart();

            //            if(timer != 0) return;

            
        }
        
        void IUpdate.Update(in float deltaTime) {}

        private bool AllPlayersReady()
        {
            //foreach (var player in world.ReadSharedData<MapComponents>().PlayerStatus)
            //{
            //    if (!player) return false;
            //    // Debug.Log("Not all players are ready");
            //}
            
            // Debug.Log("All players are ready, starting the game");
            return true;
        }
        
    }
    
}