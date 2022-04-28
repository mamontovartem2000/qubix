using ME.ECS;
using ME.ECS.DataConfigs;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Core.Features.Player.Modules;
using Project.Core.Features.Player.Systems;
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
		public DataConfig BullerConfig;
		public DataConfig GoldHunterConfig;
		public DataConfig PowerfConfig;

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

			if (world.GetModule<NetworkModule>().FakeConnect)
			{
				GoldHunterConfig.Apply(player);
				player.Set(new PlayerTag {PlayerLocalID = id, PlayerServerID = "yeet"});
			}
			else
			{
				player.Set(new PlayerTag {PlayerLocalID = id, PlayerServerID = NetworkData.Info.player_id});	
				
				switch (NetworkData.PlayersInfo[id].Character)
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

		public void OnGameStarted(int id)
		{
			var net = world.GetModule<NetworkModule>();
			net.RPC(this, _onGameStarted, id);
		}

		private void GameStarted_RPC(int id)
		{
			world.GetFeature<EventsFeature>().OnTimeSynced.Execute();
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

		protected override void OnDeconstruct()
		{
		}
	}
}