using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Common.Components;
using Project.Common.Events;
using Project.Features.Player.Modules;
using Project.Features.Player.Systems;
using Project.Markers;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Features.Player
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PlayerFeature : Feature
    {
        public PlayerInput PlayerInput;
        public DataConfig BullerConfig;
        public DataConfig GoldHunterConfig;
        public DataConfig PowerfConfig;
        public DataConfig SilenConfig;
        public DataConfig SolarayConfig;
        public DataConfig BloodlovConfig;
        public DataConfig LomixConfig;

        private RPCId _onPlayerConnected, _onPlayerDisconnected;
        private Filter _playerFilter;

        protected override void OnConstruct()
        {
            AddModule<PlayerConnectionModule>();
            AddSystem<HandleInputSystem>();
            AddSystem<LifetimeStats>();

            PlayerInput = new PlayerInput();
            PlayerInput.Enable();

            Filter.Create("Player-Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);

            var net = world.GetModule<NetworkModule>();
            net.RegisterObject(this);

            _onPlayerConnected = net.RegisterRPC(new System.Action<NetworkSetActivePlayer>(PlayerConnected_RPC).Method);
            _onPlayerDisconnected = net.RegisterRPC(new System.Action<int>(PlayerDisconnected_RPC).Method);
        }

        public void OnLocalPlayerConnected(NetworkSetActivePlayer marker)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerConnected, marker);
        }

        private void PlayerConnected_RPC(NetworkSetActivePlayer nsap)
        {
            var localId = nsap.ActorLocalID;
            var player = new Entity("player_" + localId);
            player.Set(new PlayerTag {PlayerLocalID = localId, PlayerServerID = nsap.ServerID, Nickname = nsap.Nickname});
            player.Set(new TeamTag {Value = nsap.Team});

            world.GetFeature<EventsFeature>().TabulationAddPlayer.Execute(player);
            
            if (NetworkData.PlayersInfo == null && NetworkData.Info.nfts_metadata == null) // Fake case
            {
                GoldHunterConfig.Apply(player);
                return;
            }

            var character = string.Empty;
            
            if (NetworkData.Info.nfts_metadata != null)
            {
                foreach (var nft in NetworkData.Info.nfts_metadata)
                {
                    if (nft.player_id != nsap.ServerID) continue;
                    
                    character = nft.name;
                    break;
                }
            }
            else
            {
                character = NetworkData.PlayersInfo[localId].Character;
            }

            NetworkData.LocalCharacter = character;
            
            switch (character)
            {
                case "Buller":
                {
                    BullerConfig.Apply(player);
                    break;
                }
                
                case "GoldHunter":
                {
                    GoldHunterConfig.Apply(player);
                    break;
                }
                
                case "Powerf":
                {
                    PowerfConfig.Apply(player);
                    break;
                }
                
                case "Silen":
                {
                    SilenConfig.Apply(player);
                    break;
                }
                
                case "Solaray":
                {
                    SolarayConfig.Apply(player);
                    break;
                }
                
                case "Bloodlov":
                {
                    BloodlovConfig.Apply(player);
                    break;
                }
                
                case "Lomix":
                {
                    LomixConfig.Apply(player);
                    break;
                }
                
                default:
                    Debug.Log("Unknown avatar!");
                    break;
            }
        }

        public void OnLocalPlayerDisconnected(int id)
        {
            var net = world.GetModule<NetworkModule>();
            net.RPC(this, _onPlayerDisconnected, id);
        }

        private void PlayerDisconnected_RPC(int id)
        {
            Debug.Log("Destroy Avatar");
            ref var avatar = ref GetPlayerByID(id).Get<PlayerAvatar>().Value;
            ref var weapons = ref avatar.Get<WeaponEntities>();

            weapons.LeftWeapon.Destroy();
            weapons.RightWeapon.Destroy();
            avatar.Destroy();
        }

        public Entity GetPlayerByID(int id)
        {
            foreach (var player in _playerFilter)
            {
                if (player.Read<PlayerTag>().PlayerLocalID == id)
                {
                    return player;
                }
            }

            return Entity.Empty;
        }

        protected override void OnDeconstruct() { }
    }
}