using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Core.Features.GameState.Components;
using Project.Core.Features.Player.Components;
namespace Project.Core.Features.GameState.Systems 
{
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class GameFinishedSystem : ISystem, IAdvanceTick, IUpdate 
    {
        public World world { get; set; }
        
        private GameStateFeature _feature;
        private Filter _playerFilter;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
            Filter.Create("Player Filter")
                .With<PlayerTag>()
                .WithShared<GameFinished>()
                .Push(ref _playerFilter);
        }
        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if(!world.HasSharedData<GameFinished>()) return;

                Entity winner = default;
                
                var maxKills = int.MinValue;
                var minDeaths = int.MinValue;
                
                var winCounter = 0;
                
                foreach (var player in _playerFilter)
                {
                    var kills = player.Read<PlayerScore>().Kills;
                    
                    if (kills > maxKills)
                    {
                        maxKills = kills;
                        winner = player;
                    }
                }
                
                foreach (var player in _playerFilter)
                {
                    var kills = player.Read<PlayerScore>().Kills;
                    
                    if (kills == maxKills && player != winner)
                    {
                        winCounter++;
                    }
                }
                
                if (winCounter == 0)
                {
                    foreach (var player in _playerFilter)
                    {
                        if (player == winner)
                        {
                            // player.Set(new EndGame {Winner = true});
                            world.GetFeature<EventsFeature>().Victory.Execute(player);
                        }
                        else
                        {
                            // player.Set(new EndGame {Winner = false});
                            world.GetFeature<EventsFeature>().Defeat.Execute(player);
                        }
                    }
                }
                else
                {
                    foreach (var player in _playerFilter)
                    {
                        world.GetFeature<EventsFeature>().Draw.Execute(player);
                    }
                }
        }
        
        void IUpdate.Update(in float deltaTime) {}
    }
}