using ME.ECS;
using Project.Features.Player.Views;
using Project.Features.Projectile.Components;
using UnityEngine;

namespace Project.Features {
    #region usage
    using Components; using Modules; using Systems; using Features; using Markers;
    using Player.Components; using Player.Modules; using Player.Systems; using Player.Markers;
    
    namespace Player.Components {}
    namespace Player.Modules {}
    namespace Player.Systems {}
    namespace Player.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class PlayerFeature : Feature
    {
        public PlayerView PlayerView;

        public GlobalEvent PassLocalPlayer;
        public GlobalEvent HealthChangedEvent;
        public GlobalEvent RespawnEvent;
        public GlobalEvent VictoryEvent;
        public GlobalEvent DefeatEvent;
        
        private ViewId _playerViewID;
        private RPCId _onPlayerConnected;
        private Filter _playerFilter;
        private int _playerIndex;
        private SceneBuilderFeature _builder;

        protected override void OnConstruct()
        {
            _playerViewID = world.RegisterViewSource(PlayerView);

            world.GetFeature(out _builder);

            AddSystem<PlayerMovementSystem>();
            AddSystem<PlayerHealthSystem>();
            AddSystem<ApplyDamageSystem>();
            AddSystem<PlayerPortalSystem>();
            AddSystem<PlayerRespawnSystem>();
            AddSystem<BulletCooldownSystem>();
            AddSystem<RocketCooldownSystem>();
            
            AddModule<PlayerConnectionModule>();

            var net = world.GetModule<NetworkModule>();

            net.RegisterObject(this);
            _onPlayerConnected = net.RegisterRPC(new System.Action<int>(PlayerJoined_RPC).Method);

            Filter.Create("Player Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);
        }

        protected override void OnDeconstruct() {}
        public void OnLocalPlayerConnected(int id)
        {
            _playerIndex = id;
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerConnected, id);
        }
        private void PlayerJoined_RPC(int id)
        {
            var player = new Entity("Player");
            
            player.Set(new PlayerTag {PlayerID = id, FaceDirection = Vector3.forward});
            player.InstantiateView(_playerViewID);
            
            player.Set(new PlayerHealth {Value = 100});
            player.Set(new PlayerScore {Value = 0});

            var playerPosition = _builder.GetRandomSpawnPosition();
            
            player.SetPosition(playerPosition);
            player.Set(new PlayerMoveTarget {Value = playerPosition});
            
            _builder.MoveTo(_builder.PositionToIndex(playerPosition), _builder.PositionToIndex(playerPosition));

            PassLocalPlayer.Execute(player);
        }

        public void RespawnPlayer(int id)
        {
            PlayerJoined_RPC(id);
        }
        
        public Entity GetActivePlayer()
        {
            foreach (var player in _playerFilter)
            {
                if (_playerIndex == player.Read<PlayerTag>().PlayerID)
                {
                    return player;
                }
            }
            
            return Entity.Empty;
        }
    }
}