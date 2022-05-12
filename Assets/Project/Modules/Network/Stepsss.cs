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

		public static void ProcessJoinRequest(string request)
		{
			string payloadBase64 = NetworkData.CreateFromJSON<JoinRequestData>(request).payload;
			var payloadInBytes = Convert.FromBase64String(payloadBase64);
			var playerJson = Encoding.UTF8.GetString(payloadInBytes);
			GameInfo info = NetworkData.CreateFromJSON<GameInfo>(playerJson);
			NetworkData.Info = info;
			NetworkData.FullJoinRequest = request;

			NetworkData.Connect = new WebSocketConnect();
			NetworkData.Connect.OnMessage += GetMessage;
			NetworkData.Connect.ConnectSuccessful += SendJoinRequest;
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
				default:
					Debug.Log("Unknown system message!");
					break;
			}
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
			}
		}

		private static void SetTimeRemaining(TimeRemaining timeRemaining)
		{
			Debug.Log("Set Time!");
			TimeRemaining?.Invoke(timeRemaining.Value / 1000);

			if (timeRemaining.State == "starting")
				ShowCharacterSelectionWindow?.Invoke();
		}

		private static void SetPlayerList(PlayerList playerList)
		{
			Debug.Log("Set Players!");

			PlayerInfo[] playersInfo = new PlayerInfo[playerList.PlayersLength];

			for (int i = 0; i < playerList.PlayersLength; i++)
			{
				var id = playerList.Players(i).Value.Id;
				var slot = playerList.Players(i).Value.Slot;
				var nick = playerList.Players(i).Value.Nickname;
				var character = playerList.Players(i).Value.Character;
				var icon = playerList.Players(i).Value.Icon;

				PlayerInfo player = new PlayerInfo() { Id = id, Slot = slot, Nickname = nick, Character = character, Icon = icon };
				playersInfo[i] = player;
			}

			NetworkData.PlayersInfo = playersInfo;
		}

		private static void SetStartGame(Start start)
		{
			NetworkData.GameSeed = start.Seed;
			NetworkData.Connect.OnMessage -= GetMessage;
			LoadGameScene?.Invoke();
			Debug.Log("Start");
		}

		private static void SendJoinRequest()
		{
			FlatBufferBuilder builder = new FlatBufferBuilder(1);
			var mes = builder.CreateString(NetworkData.FullJoinRequest);
			var request = JoinRequest.CreateJoinRequest(builder, 0, mes);
			var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.JoinRequest, request.Value);
			builder.Finish(offset.Value);

			var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
			NetworkData.Connect.SendSystemMessage(ms);
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
	}
}
