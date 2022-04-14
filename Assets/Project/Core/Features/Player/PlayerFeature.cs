using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Core.Features.Player.Modules;
using Project.Core.Features.Player.Systems;
using Project.Modules;

namespace Project.Core.Features.Player
{
    #region usage

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

    public sealed class PlayerFeature : Feature
    {
        public DataConfig PlayerConfig;
        
        private RPCId _onPlayerConnected, _onPlayerDisconnected, _onGameStarted;
        private Filter _playerFilter;

        protected override void OnConstruct()
        {
            AddModule<PlayerConnectionModule>();
            Filter.Create("Player-Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);

            var net = world.GetModule<NetworkModule>();
            net.RegisterObject(this);

            AddSystem<HandleInputSystem>();

            _onPlayerConnected = net.RegisterRPC(new System.Action<int>(PlayerConnected_RPC).Method);
            _onPlayerDisconnected = net.RegisterRPC(new System.Action<int>(PlayerDisconnected_RPC).Method);
            _onGameStarted = net.RegisterRPC(new System.Action<int>(GameStarted_RPC).Method);
        }

        public void OnLocalPlayerConnected(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerConnected, id);
        }

        private void PlayerConnected_RPC(int id)
        {
            var player = new Entity("player_" + id);
            player.Set(new PlayerTag {PlayerID = id});
            PlayerConfig.Apply(player);
            
        }

        public void OnLocalPlayerDisconnected(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerDisconnected, id);
        }

        private void PlayerDisconnected_RPC(int id)
        {
        }

        public void OnGameStarted(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onGameStarted, id);
        }

        private void GameStarted_RPC(int id)
        {
            world.GetFeature<EventsFeature>().OnTimeSynced.Execute();
        }
        
        public Entity GetPlayer(int id)
        {
            foreach (var player in _playerFilter)
            {
                if (player.Read<PlayerTag>().PlayerID == id)
                {
                    return player;
                }
            }
            
            return Entity.Empty;
        }

        protected override void OnDeconstruct() {}
    }
}