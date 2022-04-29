using FlatBuffers;
using FlatMessages;
using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Project.Modules.Network
{
	public class ConnectingSteps : MonoBehaviour
	{
		[DllImport("__Internal")]
		private static extern void ReadyToStart();

		[SerializeField] private GameObject _loginScreen;
		[SerializeField] private GameObject _selectionScreen;
		[SerializeField] private WaitingRoomTimer _timer;
		[SerializeField] private GameObject _timerHolder;
		[SerializeField] private TMP_InputField _playerNumber;
		[SerializeField] private TMP_InputField _playerRoomID;
		
		[SerializeField] private TextMeshProUGUI _hostRoomID;
		
		[SerializeField] private TMP_InputField _hostNick;
		[SerializeField] private TMP_InputField _playerNick;
		// [SerializeField] private Toggle _needCreateRoom;

		private bool _needLoadGameScene, _needReloadThisScene;
		private string _nickname;

		private void Start()
		{
			//ReadyToStart();
			CharacterSelectionScript.OnPlayerSelected += SetCharacterRequest;
		}

		private void Update()
		{
			if (_needLoadGameScene)
				SceneManager.LoadScene(1, LoadSceneMode.Single);

			if (_needReloadThisScene)
				SceneManager.LoadScene(0, LoadSceneMode.Single);

#if !UNITY_WEBGL || UNITY_EDITOR
			if (NetworkData.Connect != null && NetworkData.Connect.Socket.State == NativeWebSocket.WebSocketState.Open)
				NetworkData.Connect.Socket.DispatchMessageQueue();
#endif
		}

		// Browser method
		public void ProcessJoinRequest(string request)
		{
			string payloadBase64 = ParceUtils.CreateFromJSON<JoinRequestData>(request).payload;
			var payloadInBytes = Convert.FromBase64String(payloadBase64);
			var playerJson = Encoding.UTF8.GetString(payloadInBytes);
			GameInfo info = ParceUtils.CreateFromJSON<GameInfo>(playerJson);
			NetworkData.Info = info;
			NetworkData.FullJoinRequest = request;

			NetworkData.Connect = new WebSocketConnect();
			NetworkData.Connect.GetMessage += GetMessage;
			NetworkData.Connect.StartJoining += SendJoinRequest;
		}

		public void StartManual()
		{
			if (_hostNick.text == "" && _playerNick.text == "")
			{
				Debug.Log("Enter nickname");
				return;
			}
			// 	if (LoginScreenCore.IsHost)
			// {
			// 	if (_hostNick.text == "")
			// 	{
			// 		Debug.Log("Enter nickname");
			// 		return;
			// 	}			
			// }
			// else
			// {
			// 	if (_playerNick.text == "")
			// 	{
			// 		Debug.Log("Enter nickname");
			// 		return;
			// 	}
			// }

			_nickname = LoginScreenCore.IsHost ? _hostNick.text : _playerNick.text;

			Debug.Log(_nickname);

			_timerHolder.gameObject.SetActive(true);

			if (LoginScreenCore.IsHost)
			{
				int num = Int32.Parse(_playerNumber.text);
				StartCoroutine(ManualRoomCreating.CreateRoom(num, GetManualJoinRequest));
			}
			else if (_playerRoomID.text != "")
				GetManualJoinRequest(_playerRoomID.text);
		}

		private void GetManualJoinRequest(string roomId)
		{
			_hostRoomID.text = roomId;
			StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, _nickname, ProcessJoinRequest));
		}

		private void GetMessage(byte[] bytes)
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

        private void ShutdownRoom(Shutdown shutdown)
        {
			Debug.Log("ShutDown");
			_needReloadThisScene = true;
		}

		private void GetJoinResult(JoinResult joinResult)
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

		private void SetTimeRemaining(TimeRemaining timeRemaining)
		{
			Debug.Log("Set Time!");
			_timer.SetTime(timeRemaining.Value / 1000);

			if (timeRemaining.State == "starting")
				SwapScreens();
		}

		private void SwapScreens()
		{
			_loginScreen.SetActive(false);
			_selectionScreen.SetActive(true);
			CharacterSelectionScript.FirePlayerSelected("GoldHunter");
		}

		private void SetPlayerList(PlayerList playerList)
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

				PlayerInfo player = new PlayerInfo() {Id = id, Slot = slot, Nickname = nick, Character = character, Icon = icon};
				playersInfo[i] = player;
			}

			NetworkData.PlayersInfo = playersInfo;
		}

		private void SetStartGame(Start start)
		{
			NetworkData.GameSeed = start.Seed;
			NetworkData.Connect.GetMessage -= GetMessage;
			_needLoadGameScene = true;
			Debug.Log("Start");
		}

		private void SendJoinRequest()
		{
			FlatBufferBuilder builder = new FlatBufferBuilder(1);
			var mes = builder.CreateString(NetworkData.FullJoinRequest);
			var request = JoinRequest.CreateJoinRequest(builder, 0, mes);
			var offset = SystemMessage.CreateSystemMessage(builder, SystemMessages.GetTime(), Payload.JoinRequest, request.Value); //TODO: maybe Utc; Ticks or miliseconds? Check all messages. Maybe delete time in this stru�t.
			builder.Finish(offset.Value);

			var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
			NetworkData.Connect.SendSystemMessage(ms);
		}

		public void SetCharacterRequest(string characterName)
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