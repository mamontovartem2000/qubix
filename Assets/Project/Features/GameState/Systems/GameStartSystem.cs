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
            
            if (AllPlayersReady())
            {
                world.GetFeature<EventsFeature>().AllPlayersReady.Execute(world.GetFeature<PlayerFeature>().GetActivePlayer());
                world.RemoveSharedData<GamePaused>();

                foreach (var player in _playerFilter)
                {
                    player.Remove<PlayerDisplay>();
                }
                
                world.AddMarker(new GameStartedMarker());
            }
            
//            if(timer != 0) return;

            // Entity winner = default;
            //
            // var maxScore = int.MinValue;
            // var winCounter = 0;
            //
            // foreach (var player in _playerFilter)
            // {
            //     var score = player.Read<PlayerScore>().Value;
            //     
            //     if (score > maxScore)
            //     {
            //         maxScore = score;
            //         winner = player;
            //     }
            // }
            //
            // foreach (var player in _playerFilter)
            // {
            //     var score = player.Read<PlayerScore>().Value;
            //     
            //     if (score == maxScore && player != winner)
            //     {
            //         winCounter++;
            //     }
            //
            //     if (score < maxScore)
            //     {
            //         //add to loser list
            //     }
            // }
            //
            // if (winCounter == 0)
            // {
            //     if (Utilitiddies.CheckLocalPlayer(winner))
            //     {
            //         //show win screen
            //         world.SetSharedData(new GamePaused());
            //     }
            //     else
            //     {
            //         //show lose screen
            //
            //     }
            // }
            // else
            // {
            //     //TODO: add more time, bool _last = true;
            //     //delete losers entity
            //     //if(_last) draw screen
            // }
        }
        
        void IUpdate.Update(in float deltaTime) {}

        private bool AllPlayersReady()
        {
            foreach (var player in world.ReadSharedData<MapComponents>().PlayerStatus)
            {
                if (!player) return false;
                Debug.Log("Not all players are ready");
            }
            
            Debug.Log("All players are ready, starting the game");
            return true;
        }
        
    }
    
}