using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Core.Features.Events;
using Project.Core.Features.GameState.Components;
using Project.Core.Features.Player.Components;
using Project.Core.Features.Player.Modules;
using Project.Core.Features.Player.Systems;
using Project.Core.Features.Player.Views;
using Project.Core.Features.SceneBuilder;
using Project.Modules;
using UnityEngine;

namespace Project.Core.Features.Player {
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion

    public sealed class PlayerFeature : Feature
    {
        [SerializeField] private DataConfig _defaultPlayerConfig;

        public PlayerView PlayerView;
        
        private ViewId _playerViewID;
        private RPCId _onGameStarted, _onPlayerDisconnected;     
        private Filter _playerFilter, _deadFilter;       
        private int _playerIndex;
        
        private SceneBuilderFeature _builder;
        private EventsFeature _events;
        
        protected override void OnConstruct()
        {
            GetFeatures();
            AddSystems();
            AddModules();
            CreateFilters();

            RegisterRPCs(world.GetModule<NetworkModule>());

            _playerViewID = world.RegisterViewSource(PlayerView);
        }

        private void GetFeatures()
        {
            world.GetFeature(out _builder);
            world.GetFeature(out _events);
        }

        private void AddModules()
        {
            AddModule<PlayerConnectionModule>();
        }
        private void AddSystems()
        {
            AddSystem<PlayerRotationSystem>();      
            AddSystem<PlayerMovementSystem>();
            AddSystem<PlayerHealthSystem>();
            AddSystem<ApplyDamageSystem>();
            AddSystem<PlayerRespawnSystem>();
            AddSystem<HandleInputSystem>();         

        }

        private void CreateFilters()
        {
            Filter.Create("Players Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);

            Filter.Create("Dead Filter")
                .With<DeadBody>()
                .Push(ref _deadFilter);
        }

        private void RegisterRPCs(NetworkModule net)
        {
            net.RegisterObject(this);

            _onPlayerDisconnected = net.RegisterRPC(new System.Action<int>(PlayerDisconnected_RPC).Method);
            _onGameStarted = net.RegisterRPC(new System.Action<int>(GameStarted_RPC).Method);
        }

        private void GameStarted_RPC(int id)
        {
            Debug.Log($"GameStarted with player {id}");
            CreatePlayer(id);
        }

        private Entity CreatePlayer(int id)
        {
            var player = new Entity("Player");
            player.InstantiateView(_playerViewID);

            _defaultPlayerConfig.Apply(in player);
            player.Get<PlayerTag>().PlayerID = id;
            player.SetPosition(SceneUtils.GetRandomSpawnPosition());
            player.Get<PlayerMoveTarget>().Value = player.GetPosition();
         
            _builder.TakeTheCell(player.GetPosition());
            _events.OnTimeSynced.Execute(player);
            _events.PassLocalPlayer.Execute(player);

            world.RemoveSharedData<GamePaused>();
            
            if (!_builder.TimerEntity.Has<GameTimer>())
            {
                _builder.TimerEntity.Set(new GameTimer {Value = 150f});
            }

            return player;
        }
        
        public void OnLocalPlayerConnected(int id)
        {
            _playerIndex = id;
            Debug.Log($"PlayerConnected with player {id}");
        }

        public void OnLocalPlayerDisconnected(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerDisconnected, id);
        }

        public void OnGameStarted()
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onGameStarted, _playerIndex);
        }

        private void PlayerDisconnected_RPC(int id)
        {
            Debug.Log($"PlayerDisconnected with player {id}");

            var toDestroy = GetPlayerByID(id);

            foreach (var deadBody in _deadFilter)
            {
                if (deadBody.Read<DeadBody>().ActorID == id)
                {
                    deadBody.Destroy();
                }    
            }
            
            if (toDestroy != Entity.Empty)
            {
                toDestroy.Destroy();
            }
            else
            {
                Debug.Log("empty entity");                
            }
        }

        public Entity RespawnPlayer(int id)
        {
            return CreatePlayer(id);
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
