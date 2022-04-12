using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player.Modules;
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
        private RPCId _onPlayerConnected, _onPlayerDisconnected;
        private Filter _playerFilter;

        protected override void OnConstruct()
        {
            AddModule<PlayerConnectionModule>();
            Filter.Create("Player-Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);

            var net = world.GetModule<NetworkModule>();
            net.RegisterObject(this);

            _onPlayerConnected = net.RegisterRPC(new System.Action<int>(PlayerConnected_RPC).Method);
            _onPlayerDisconnected = net.RegisterRPC(new System.Action<int>(PlayerDisconnected_RPC).Method);
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
            player.Set(new PlayerScore {Kills = 0, Deaths = 0});
            
            player.Set(new NeedAvatar());
        }

        public void OnLocalPlayerDisconnected(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerDisconnected, id);
        }

        private void PlayerDisconnected_RPC(int id)
        {
        }
        
        public Entity GetActivePlayer(int index)
        {
            foreach (var player in _playerFilter)
            {
                if (index == player.Read<PlayerTag>().PlayerID)
                {
                    return player;
                }
            }

            return Entity.Empty;
        }
        
        public Entity GetPlayerByID(int id)
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