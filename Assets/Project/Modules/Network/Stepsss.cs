using FlatBuffers;
using FlatMessages;
using System;
using System.Text;
using UnityEngine;

namespace Project.Modules.Network
{
    public static class Stepsss
	{
		public static Action<uint> TimeRemaining;
		public static Action LoadMainMenuScene;
		public static Action LoadGameScene;
		public static Action ShowCharacterSelectionWindow;
		public static Action<RoomInfo[]> GetRoomList;

		public static void ProcessJoinRequest(string request)
		{
			string payloadBase64 = NetworkData.CreateFromJSON<JoinRequestData>(request).payload;
			var payloadInBytes = Convert.FromBase64String(payloadBase64);
			var playerJson = Encoding.UTF8.GetString(payloadInBytes);
			GameInfo info = NetworkData.CreateFromJSON<GameInfo>(playerJson);
			NetworkData.Info = info;
			NetworkData.FullJoinRequest = request;

			NetworkData.Connect = new WebSocketConnect(NetworkData.Info.server_url);
			NetworkData.Connect.OnMessage += GetMessage;
			NetworkData.Connect.ConnectSuccessful += SendJoinRequest;
			NetworkData.Connect.ConnectError += ExitGame;
		}

		public static void ProcessJoinRequestWithoutSocket(string request)
		{
			string payloadBase64 = NetworkData.CreateFromJSON<JoinRequestData>(request).payload;
			var payloadInBytes = Convert.FromBase64String(payloadBase64);
			var playerJson = Encoding.UTF8.GetString(payloadInBytes);
			GameInfo info = NetworkData.CreateFromJSON<GameInfo>(playerJson);
			NetworkData.Info = info;
			NetworkData.FullJoinRequest = request;

			SendJoinRequest();
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
			LoadMainMenuScene?.Invoke();
		}

        private static void GetMessage(byte[] bytes)
		{
			byte[] buffer = new byte[bytes.Length - 1];
			Array.Copy(bytes, 1, buffer, 0, buffer.Length);

			SystemMessage data = SystemMessage.GetRootAsSystemMessage(new ByteBuffer(buffer));

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
					Debug.Log("Unknown system message!");
					break;
			}
		}

        private static void ShowRoomList(RoomList roomList)
        {
			Debug.Log("Set Rooms!");

			RoomInfo[] roomsInfo = new RoomInfo[roomList.RoomsLength];

			for (int i = 0; i < roomList.RoomsLength; i++)
			{
				var room = roomList.Rooms(i);

				RoomInfo roomInfo = new RoomInfo() { Id = room.Value.Id, PlayersCount = room.Value.PlayersCount, MaxPlayersCount = room.Value.MaxPlayersCount };
				roomsInfo[i] = roomInfo;
			}

			GetRoomList?.Invoke(roomsInfo);
		}

        private static void ShutdownRoom(Shutdown shutdown)
		{
			Debug.Log("ShutDown");
			LoadMainMenuScene?.Invoke();
		}

		private static void GetJoinResult(JoinResult joinResult)
		{
			if (joinResult.Value)
			{
				NetworkData.PlayerIdInRoom = joinResult.Slot;
				Debug.Log($"Join {joinResult.Slot}");
			}
			else
			{
				//TODO: Show notif for user
				//TODO: Close and clear connect
				Debug.Log($"Join Error: {joinResult.Reason}");
				NetworkData.CloseNetwork();
				LoadMainMenuScene?.Invoke();
			}
		}

		private static void SetTimeRemaining(TimeRemaining timeRemaining)
		{
			Debug.Log("Set Time!");

			if (timeRemaining.State == "starting")
            {
				TimeRemaining?.Invoke(timeRemaining.Value / 1000);
				ShowCharacterSelectionWindow?.Invoke();
			}
		}

		private static void SetPlayerList(PlayerList playerList)
		{
			Debug.Log("Set Players!");

			PlayerInfo[] playersInfo = new PlayerInfo[playerList.PlayersLength];

			for (int i = 0; i < playerList.PlayersLength; i++)
			{
				var player = playerList.Players(i);
				var id = player.Value.Id;
				var slot = player.Value.Slot;
				var nick = player.Value.Nickname;
				var character = player.Value.Character;
				var icon = player.Value.Icon;

				PlayerInfo playerInfo = new PlayerInfo() { Id = id, Slot = slot, Nickname = nick, Character = character, Icon = icon };
				playersInfo[i] = playerInfo;
			}

			NetworkData.PlayersInfo = playersInfo;
		}

		private static void SetStartGame(Start start)
		{
			NetworkData.GameSeed = start.Seed;
			Unsubscibe();
			LoadGameScene?.Invoke();
			Debug.Log("Start");
		}

		private static void SendJoinRequest()
		{
			FlatBufferBuilder builder = new FlatBufferBuilder(1);
			var mes = builder.CreateString(NetworkData.FullJoinRequest);
			var request = JoinRequest.CreateJoinRequest(builder, 0, mes); //TODO: Choose 0 or 1
			var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.JoinRequest, request.Value);
			builder.Finish(offset.Value);

			var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
			NetworkData.Connect.SendSystemMessage(ms);
			Debug.Log("Send JoinRequest");
		}

		public static void ChangeRoomRequest(string roomId)
		{
			FlatBufferBuilder builder = new FlatBufferBuilder(1);
			var id = builder.CreateString(roomId);
			var request = ChangeRoom.CreateChangeRoom(builder, id);
			var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.ChangeRoom, request.Value);
			builder.Finish(offset.Value);

			var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
			NetworkData.Connect.SendSystemMessage(ms);
			Debug.Log("Change Room");
		}

		public static void SetCharacterRequest(string characterName)
		{
			FlatBufferBuilder builder = new FlatBufferBuilder(1);
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
