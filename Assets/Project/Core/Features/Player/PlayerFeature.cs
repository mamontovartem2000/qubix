using ME.ECS;
using Project.Common;
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
        public PlayerView PlayerView;
        public Material[] Materials;
        
        private ViewId _playerViewID;
        private RPCId _onGameStarted, _onPlayerConnected, _onPlayerDisconnected;
        
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
            AddSystem<PlayerMovementSystem>();
            AddSystem<PlayerHealthSystem>();
            AddSystem<ApplyDamageSystem>();
            AddSystem<PlayerPortalSystem>();
            AddSystem<PlayerRespawnSystem>();
            AddSystem<HandleInputSystem>();
            // AddSystem<LeftWeaponCooldownSystem>();
            // AddSystem<RightWeaponCooldownSystem>();
        }
        private void CreateFilters()
        {
            Filter.Create("Player Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);

            Filter.Create("dead-filter")
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

            player.Set(new PlayerTag {PlayerID = id, FaceDirection = Vector3.forward, 
                Material = Materials[Utilitiddies.SafeCheckIndexByLength(_playerIndex, Materials.Length)]});

            player.Set(new PlayerHealth {Value = 100});
            player.SetPosition(_builder.GetRandomSpawnPosition());
            player.Set(new PlayerMoveTarget {Value = player.GetPosition()});
            player.Set(new PlayerMovementSpeed {Value = 4f});
            //player.Set(new LeftWeapon {Type = WeaponType.Gun, Cooldown = 0.2f, Ammo = 20, MaxAmmo = 20, ReloadTime = 1.2f});
            
            _builder.MoveTo(_builder.PositionToIndex(player.GetPosition()), _builder.PositionToIndex(player.GetPosition()));
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
            // GameStarted_RPC(id);
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
