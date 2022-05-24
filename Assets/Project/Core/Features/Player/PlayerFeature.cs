using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Common.Components;
using Project.Common.Views;
using Project.Core.Features.Player.Modules;
using Project.Core.Features.Player.Systems;
using Project.Markers;
using Project.Modules.Network;

namespace Project.Core.Features.Player
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

		private RPCId _onPlayerConnected, _onPlayerDisconnected;
		private Filter _playerFilter;

		protected override void OnConstruct()
		{
			AddModule<PlayerConnectionModule>();
			// AddSystem<InputSafeCheckSystem>();
			AddSystem<HandleInputSystem>();
			
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
			player.Set(new PlayerTag {PlayerLocalID = localId, PlayerServerID = nsap.ServerID, Nickname = nsap.Nickname, Team = nsap.Team });

			if (world.GetModule<NetworkModule>().FakeConnect)
			{
				BullerConfig.Apply(player);
				return;
			}

			switch (NetworkData.PlayersInfo[localId].Character)
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
			}
		}

		public void OnLocalPlayerDisconnected(int id)
		{
			var net = world.GetModule<NetworkModule>();
			net.RPC(this, _onPlayerDisconnected, id);
		}

		private void PlayerDisconnected_RPC(int id)
		{
			ref var avtr = ref GetPlayerByID(id).Get<PlayerAvatar>().Value;
			ref var weps = ref avtr.Get<WeaponEntities>();
			
			weps.LeftWeapon.Destroy();
			weps.RightWeapon.Destroy();
			avtr.Destroy();
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

		protected override void OnDeconstruct() {}
	}
}