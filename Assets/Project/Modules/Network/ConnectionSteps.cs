using FlatBuffers;
using FlatMessages;
using System;
using System.Text;
using UnityEngine;

namespace Project.Modules.Network
{
    public static class ConnectionSteps
	{
		public static void ConnectWithCreateSocket(string request)
		{
			ProcessJoinRequest(request);

			NetworkData.Connect = new WebSocketConnect(NetworkData.Info.server_url);
			NetworkData.Connect.OnMessage += GetMessage;
			NetworkData.Connect.ConnectSuccessful += SendJoinRequest;
			NetworkData.Connect.ConnectError += ExitGame;
		}

		public static void ConnectWithoutCreateSocket(string request)
		{
			ProcessJoinRequest(request);
			SendJoinRequest();
		}

		private static void ProcessJoinRequest(string request)
		{
			var payloadBase64 = NetworkData.CreateFromJSON<JoinRequestData>(request).payload;
			var payloadInBytes = Convert.FromBase64String(payloadBase64);
			var playerJson = Encoding.UTF8.GetString(payloadInBytes);
			var info = NetworkData.CreateFromJSON<GameInfo>(playerJson);
			NetworkData.Info = info;
			Enum.TryParse(info.game_mode, out GameModes gameMode);
			NetworkData.GameMode = gameMode;
			NetworkData.FullJoinRequest = request;
		}

		public static void CreateSocketConnect(string url)
        {
			NetworkData.Connect = new WebSocketConnect(url);
			NetworkData.Connect.OnMessage += GetMessage;
			NetworkData.Connect.ConnectSuccessful += () => Debug.Log("Socket Opened");
			NetworkData.Connect.ConnectError += ExitGame;
		}

		private static void ExitGame(string obj)
        {
			Debug.Log("Reload Main");
			NetworkEvents.LoadMainMenuScene?.Invoke();
		}

        private static void GetMessage(byte[] bytes)
		{
			var buffer = new byte[bytes.Length - 1];
			Array.Copy(bytes, 1, buffer, 0, buffer.Length);

			if (buffer.Length == 4)
			{
				if (Encoding.UTF8.GetString(buffer) == "ping") 
					return;
			}

			var data = SystemMessage.GetRootAsSystemMessage(new ByteBuffer(buffer));

			switch (data.PayloadType)
			{
				case Payload.NONE:
					Debug.Log("Payload type NONE!");
					break;
				case Payload.JoinResult:
					GetJoinResult(data.PayloadAsJoinResult());
					break;
				case Payload.Start:
					SetStartGame(data.PayloadAsStart());
					break;
				case Payload.PlayerList:
					SetPlayerList(data.PayloadAsPlayerList());
					break;
				case Payload.TimeRemaining:
					SetTimeRemaining(data.PayloadAsTimeRemaining());
					break;
				case Payload.Shutdown:
					ShutdownRoom(data.PayloadAsShutdown());
					break;
				case Payload.RoomList:
					ShowRoomList(data.PayloadAsRoomList());
					break;
				default:
					Debug.Log($"Unknown system message! Payload type: {data.PayloadType}");
					break;
			}
		}

        private static void ShowRoomList(RoomList roomList)
        {
			var roomsInfo = new RoomInfo[roomList.RoomsLength];

			for (var i = 0; i < roomList.RoomsLength; i++)
			{
				var room = roomList.Rooms(i);

				var roomInfo = new RoomInfo() { Id = room.Value.Id, PlayersCount = room.Value.PlayersCount, MaxPlayersCount = room.Value.MaxPlayersCount };
				roomsInfo[i] = roomInfo;
			}

			NetworkEvents.GetRoomList?.Invoke(roomsInfo);
		}

        private static void ShutdownRoom(Shutdown shutdown)
		{
			Debug.Log("ShutDown");
			NetworkEvents.LoadMainMenuScene?.Invoke();
		}

		private static void GetJoinResult(JoinResult joinResult)
		{
			if (joinResult.Value)
			{
				NetworkData.SlotInRoom = joinResult.Slot;
				NetworkData.Team = joinResult.Team;
				NetworkEvents.LoadMap?.Invoke();
				//Debug.Log($"Join slot: {joinResult.Slot}, team {joinResult.Team};");
			}
			else
			{
				//TODO: Show notif for user
				//TODO: Close and clear connect
				Debug.LogError($"Join Error: {joinResult.Reason}");
				NetworkData.CloseNetwork();
				NetworkEvents.LoadMainMenuScene?.Invoke();
			}
		}

		private static void SetTimeRemaining(TimeRemaining timeRemaining)
		{
			NetworkEvents.SetTimer?.Invoke(timeRemaining);
		}

		private static void SetPlayerList(PlayerList playerList)
		{
			var debug = "Set Players: \n";
			var playersInfo = new PlayerInfo[playerList.PlayersLength];

			for (var i = 0; i < playerList.PlayersLength; i++)
			{
				var player = playerList.Players(i);
				var id = player.Value.Id;
				var slot = player.Value.Slot;
				var nick = player.Value.Nickname;
				var character = player.Value.Character;
				var icon = player.Value.Icon;

				var playerInfo = new PlayerInfo() { Id = id, Slot = slot, Nickname = nick, Character = character, Icon = icon };
				playersInfo[i] = playerInfo;
				debug += $"Slot: {slot}, Id: {id}, Character: {character};\n";
			}

			NetworkData.PlayersInfo = playersInfo;
			//Debug.Log(debug);
		}

		private static void SetStartGame(Start start)
		{
			NetworkData.GameSeed = start.Seed;
			Unsubscibe();
			NetworkEvents.LoadGameScene?.Invoke();
			Debug.Log("Start");
		}

		private static void SendJoinRequest()
		{
			var builder = new FlatBufferBuilder(1);
			var mes = builder.CreateString(NetworkData.FullJoinRequest);

			var type = NetworkData.BuildType == BuildTypes.Front_Hub ? 2 : 0;
			var request = JoinRequest.CreateJoinRequest(builder, (sbyte)type, mes);
			var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.JoinRequest, request.Value);
			builder.Finish(offset.Value);

			var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
			NetworkData.Connect.SendSystemMessage(ms);
		}

		public static void ChangeRoomRequest(string roomId)
		{
			var builder = new FlatBufferBuilder(1);
			var id = builder.CreateString(roomId);
			var request = ChangeRoom.CreateChangeRoom(builder, id);
			var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.ChangeRoom, request.Value);
			builder.Finish(offset.Value);

			var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
			NetworkData.Connect.SendSystemMessage(ms);
		}

		public static void SetCharacterRequest(string characterName)
		{
			var builder = new FlatBufferBuilder(1);
			var id = builder.CreateString(NetworkData.Info.player_id);
			var character = builder.CreateString(characterName);
			var request = SetCharacter.CreateSetCharacter(builder, id, character);
			var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.SetCharacter, request.Value);
			builder.Finish(offset.Value);

			var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
			NetworkData.Connect.SendSystemMessage(ms);
		}

		private static void Unsubscibe()
        {
			NetworkData.Connect.OnMessage -= GetMessage;
			NetworkData.Connect.ConnectSuccessful -= SendJoinRequest;
			NetworkData.Connect.ConnectError -= ExitGame;
		} 
	}
}
