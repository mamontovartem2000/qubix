using ME.ECS;
using Photon.Pun;
using Project.Features.Player.Views;
using Project.Features.Projectile.Components;
using Project.Features.Projectile.Systems;
using Project.Features.SceneBuilder.Components;
using Project.Utilities;
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
        public Material[] Materials;
        
        private ViewId _playerViewID;
        private RPCId _onGameStarted, _onPlayerDisconnected, _onPlayerReady, _onPlayerConnected;
        
        private Filter _playerFilter, _deadFilter;
        
        private int _playerIndex;
        private bool _ready;
        
        private SceneBuilderFeature _builder;
        
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
            AddSystem<LeftWeapomCooldownSystem>();
            AddSystem<RightWeaponCooldownSystem>();
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
        
        //Register and create RPC below
        private void RegisterRPCs(NetworkModule net)
        {
            net.RegisterObject(this);

            _onPlayerDisconnected = net.RegisterRPC(new System.Action<int>(PlayerDisconnected_RPC).Method);
            _onGameStarted = net.RegisterRPC(new System.Action<int>(GameStarted_RPC).Method);
            _onPlayerReady = net.RegisterRPC(new System.Action<int>(PlayerReady_RPC).Method);
            _onPlayerConnected = net.RegisterRPC(new System.Action<int>(PlayerConnected_RPC).Method);
        }
        
        public void OnPlayerReady(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerReady, id);
        }

        public void OnGameStarted()
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onGameStarted, _playerIndex);
        }
        
        public void OnLocalPlayerDisconnected(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerDisconnected, id);
        }

        private void OnPlayerConnected(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerConnected, id);
        }
        
        //Define RPC logic here
        private void GameStarted_RPC(int id)
        {
            Debug.Log($"GameStarted");
            CreatePlayer(id);
        }

        private void PlayerConnected_RPC(int id)
        {
            Debug.Log($"player {id} has index of {_playerIndex}, localplayer: {PhotonNetwork.LocalPlayer.ActorNumber}");
        }
        
        private void PlayerReady_RPC(int id)
        {
            world.GetSharedData<MapComponents>().PlayerStatus[id - 1] = true;
        }
        
        private void PlayerDisconnected_RPC(int id)
        {
            Debug.Log($"Player {id} disconnected");

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
        }
        
        private Entity CreatePlayer(int id)
        {
            var player = new Entity("Player");
            player.InstantiateView(_playerViewID);

            player.Set(new PlayerTag {PlayerID = id, FaceDirection = Vector3.forward, 
                Material = Materials[Utilitiddies.SafeCheckIndexByLength(_playerIndex, Materials.Length)]});

            player.Set(new PlayerHealth {Value = 1});
            player.SetPosition(_builder.GetRandomSpawnPosition());
            player.Set(new PlayerMoveTarget {Value = player.GetPosition()});
            player.Set(new LeftWeapon {Type = WeaponType.Gun, Cooldown = 0.2f, Ammo = 20, MaxAmmo = 20, ReloadTime = 1.2f});
            player.Set(new RightWeapon {Type = WeaponType.Rocket, Cooldown = 0.4f, Count = 10, MaxCount = 10});
            
            _builder.MoveTo(_builder.PositionToIndex(player.GetPosition()), _builder.PositionToIndex(player.GetPosition()));
            world.GetFeature<EventsFeature>().PassLocalPlayer.Execute(player);

            return player;
        }
        
        public void OnLocalPlayerConnected(int id)
        {
            _playerIndex = id;
            OnPlayerConnected(id);
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

        public int GetPlayerID()
        {
            return _playerIndex;
        }

        protected override void OnDeconstruct() {}
    }
}