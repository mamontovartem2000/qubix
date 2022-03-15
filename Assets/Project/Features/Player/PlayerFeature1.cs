//
// using System;
// using ME.ECS;
// using Photon.Pun;
// using Project.Features.Player.Views;
// using Project.Features.Projectile.Components;
// using Project.Features.Projectile.Systems;
// using Project.Features.SceneBuilder.Components;
// using Project.Utilities;
// using UnityEngine;
//
// namespace Project.Features
// {
//     #region usage
//
//     using Components;
//     using Modules;
//     using Systems;
//     using Features;
//     using Markers;
//     using Player.Components;
//     using Player.Modules;
//     using Player.Systems;
//     using Player.Markers;
//
//     namespace Player.Components
//     {
//     }
//
//     namespace Player.Modules
//     {
//     }
//
//     namespace Player.Systems
//     {
//     }
//
//     namespace Player.Markers
//     {
//     }
//
// #if ECS_COMPILE_IL2CPP_OPTIONS
//     [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
// #endif
//
//     #endregion
//
//     public sealed class PlayerFeature : Feature
//     {
//         public PlayerView PlayerView;
//         public Material[] Materials;
//
//         private ViewId _playerViewID;
//         private RPCId _onGameStarted, _onPlayerDisconnected, _onSelectColor, _onPlayerReady, _onGameStartedComplete;
//
//         private Filter _playerFilter, _deadFilter;
//
//         private int _playerIndex;
//
//         private SceneBuilderFeature _builder;
//
//         protected override void OnConstruct()
//         {
//             GetFeatures();
//             AddSystems();
//             AddModules();
//             CreateFilters();
//
//             RegisterRPCs(world.GetModule<NetworkModule>());
//
//             _playerViewID = world.RegisterViewSource(PlayerView);
//         }
//
//         private void GetFeatures()
//         {
//             world.GetFeature(out _builder);
//         }
//
//         private void AddModules()
//         {
//             AddModule<PlayerConnectionModule>();
//         }
//
//         private void AddSystems()
//         {
//             AddSystem<PlayerMovementSystem>();
//             AddSystem<PlayerHealthSystem>();
//             AddSystem<ApplyDamageSystem>();
//             AddSystem<PlayerPortalSystem>();
//             AddSystem<PlayerRespawnSystem>();
//             AddSystem<LeftWeaponCooldownSystem>();
//             AddSystem<RightWeaponCooldownSystem>();
//             AddSystem<RightWeaponReloadSystem>();
//         }
//
//         private void CreateFilters()
//         {
//             Filter.Create("Player Filter")
//                 .With<PlayerTag>()
//                 .Push(ref _playerFilter);
//
//             Filter.Create("dead-filter")
//                 .With<DeadBody>()
//                 .Push(ref _deadFilter);
//         }
//
//         //Register and create RPC below
//         private void RegisterRPCs(NetworkModule net)
//         {
//             net.RegisterObject(this);
//
//             _onPlayerDisconnected = net.RegisterRPC(new System.Action<int>(PlayerDisconnected_RPC).Method);
//             _onGameStarted = net.RegisterRPC(new System.Action<int>(GameStarted_RPC).Method);
//             _onSelectColor = net.RegisterRPC(new System.Action<int, int>(SelectColor_RPC).Method);
//             _onPlayerReady = net.RegisterRPC(new System.Action<int>(PlayerReady_RPC).Method);
//             _onGameStartedComplete = net.RegisterRPC(new System.Action(StartGame_RPC).Method);
//         }
//
//         public void OnGameStarted(int id)
//         {
//             var net = world.GetModule<NetworkModule>();
//             net.RPC(this, _onGameStarted, id);
//         }
//
//         public void OnLocalPlayerDisconnected(int id)
//         {
//             var net = world.GetModule<NetworkModule>();
//             net.RPC(this, _onPlayerDisconnected, id);
//         }
//
//         public void OnSelectColor(int actorID, int colorID)
//         {
//             var net = world.GetModule<NetworkModule>();
//             net.RPC(this, _onSelectColor, actorID, colorID);
//         }
//
//         public void OnPlayerReady(int id)
//         {
//             var net = world.GetModule<NetworkModule>();
//             net.RPC(this, _onPlayerReady, id);
//         }
//
//         public void OnGameStartedComplete()
//         {
//             var net = world.GetModule<NetworkModule>();
//             net.RPC(this, _onGameStartedComplete);
//         }
//
//         //Define RPC logic here
//         // private void GameStarted_RPC(int id)
//         // {
//         //     if (GetPlayerByID(id).IsAlive()) return;
//         //
//         //     var seed = PhotonNetwork.MasterClient.UserId;
//         //     this.world.SetSeed(MathUtils.GetHash(seed));
//         //     _builder.ChangeColorGlowingMaterial();
//         //
//         //     Debug.Log($"GameStarted");
//         //     CreatePlayer(id);
//         //
//         //     // world.GetFeature<EventsFeature>().OnTimeSynced.Execute(GetPlayerByID(id));
//         //     world.SetSharedData(new GamePaused());
//         // }
//
//         private void PlayerDisconnected_RPC(int id)
//         {
//             Debug.Log($"Player {id} disconnected");
//
//             var toDestroy = GetPlayerByID(id);
//
//             foreach (var deadBody in _deadFilter)
//             {
//                 if (deadBody.Read<DeadBody>().ActorID == id)
//                 {
//                     deadBody.Destroy();
//                 }
//             }
//
//             if (toDestroy != Entity.Empty)
//             {
//                 toDestroy.Destroy();
//             }
//         }
//
//         private void SelectColor_RPC(int actorID, int colorID)
//         {
//             if (actorID != GetPlayerByID(actorID).Get<PlayerTag>().PlayerID) return;
//             GetPlayerByID(actorID).Get<PlayerTag>().Material = Materials[_playerIndex * colorID];
//         }
//
//         private bool _initialised = false;
//
//         private void GameStarted_RPC(int id)
//         {
//             Debug.Log($"GameStarted with player {id}");
//             CreatePlayer(id);
//         }
//
//         private Entity CreatePlayer(int id)
//         {
//             var player = new Entity("Player");
//             player.InstantiateView(_playerViewID);
//
//             player.Set(new PlayerTag {PlayerID = id, FaceDirection = Vector3.forward, 
//                 Material = Materials[Utilitiddies.SafeCheckIndexByLength(_playerIndex, Materials.Length)]});
//
//             player.Set(new PlayerHealth {Value = 1});
//             player.SetPosition(_builder.GetRandomSpawnPosition());
//             player.Set(new PlayerMoveTarget {Value = player.GetPosition()});
//             player.Set(new LeftWeapon {Type = WeaponType.Gun, Cooldown = 0.2f, Ammo = 20, MaxAmmo = 20, ReloadTime = 1.2f});
//
//             _builder.MoveTo(_builder.PositionToIndex(player.GetPosition()), _builder.PositionToIndex(player.GetPosition()));
//             
//             world.GetFeature<EventsFeature>().PassLocalPlayer.Execute(player);
//             world.GetFeature<EventsFeature>().OnTimeSynced.Execute(player);
//
//             if (!_initialised)
//             {
//                 _initialised = true;
//                 
//                 world.RemoveSharedData<GamePaused>();
//                 _builder.TimerEntity.Set(new GameTimer {Value = 150f});
//             }
//
//             return player;
//         }
//         
//         // private Entity CreatePlayer(int id)
//         // {
//         //     var player = new Entity("Player");
//         //     player.InstantiateView(_playerViewID);
//         //
//         //     player.Set(new PlayerTag {PlayerID = id, FaceDirection = Vector3.forward,Material = Materials[1 * id]});
//         //     player.Set(new PlayerHealth {Value = 1});
//         //     
//         //     if (_initialised)
//         //     {
//         //         player.SetPosition(_builder.GetRandomSpawnPosition());
//         //         player.Set(new PlayerMoveTarget {Value = player.GetPosition()});
//         //         player.Set(new LeftWeapon {Type = WeaponType.Gun, Cooldown = 0.2f, Ammo = 20, MaxAmmo = 20, ReloadTime = 1.2f});
//         //         _builder.MoveTo(_builder.PositionToIndex(player.GetPosition()), _builder.PositionToIndex(player.GetPosition()));
//         //     }
//         //     else
//         //     {
//         //         player.SetPosition(new Vector3(-20f, -20f, -20f) * id);
//         //         
//         //         player.Set(new PlayerDisplay());
//         //         player.Set(new Initialized());
//         //         world.GetFeature<EventsFeature>().OnTimeSynced.Execute(player);
//         //         _initialised = true;
//         //     }
//         //
//         //     player.Set(new LeftWeapon {Type = WeaponType.Gun, Cooldown = 0.2f, Ammo = 20, MaxAmmo = 20, ReloadTime = 1.2f});
//         //     world.GetFeature<EventsFeature>().PassLocalPlayer.Execute(player);
//         //     player.Set(new PlayerMoveTarget {Value = player.GetPosition()});
//         //     return player;
//         // }
//
//         public void OnLocalPlayerConnected(int id)
//         {
//             _playerIndex = id;
//         }
//
//         public Entity RespawnPlayer(int id)
//         {
//             return CreatePlayer(id);
//         }
//
//         private void PlayerReady_RPC(int id)
//         {
//             if (id != GetPlayerByID(id).Read<PlayerTag>().PlayerID) return;
//             world.GetSharedData<MapComponents>().PlayerStatus[id - 1] = true;
//         }
//
//         public Entity GetActivePlayer()
//         {
//             foreach (var player in _playerFilter)
//             {
//                 if (_playerIndex == player.Read<PlayerTag>().PlayerID)
//                 {
//                     return player;
//                 }
//             }
//
//             return Entity.Empty;
//         }
//
//         public Entity GetPlayerByID(int id)
//         {
//             foreach (var player in _playerFilter)
//             {
//                 if (player.Read<PlayerTag>().PlayerID == id)
//                 {
//                     return player;
//                 }
//             }
//
//             return Entity.Empty;
//         }
//
//
//         private void StartGame_RPC()
//         {
//             var activePlayer = GetActivePlayer();
//
//             activePlayer.SetPosition(_builder.GetRandomSpawnPosition());
//             _builder.MoveTo(_builder.PositionToIndex(activePlayer.GetPosition()), _builder.PositionToIndex(activePlayer.GetPosition()));
//             activePlayer.Get<PlayerMoveTarget>().Value = activePlayer.GetPosition();
//             activePlayer.Remove<PlayerDisplay>();
//             world.GetFeature<EventsFeature>().OnGameStarted.Execute(activePlayer);
//             world.RemoveSharedData<GamePaused>();
//             _builder.TimerEntity.Set(new GameTimer {Value = 5f});
//         }
//
//         public void ForceStart()
//         {
//             StartGame_RPC();
//         }
//
//         protected override void OnDeconstruct()
//         {
//         }
//     }
// }