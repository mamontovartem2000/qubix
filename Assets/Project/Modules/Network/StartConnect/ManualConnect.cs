using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Modules.Network
{
    public class ManualConnect : ConnectTemplate
	{
		[SerializeField] private GameObject _loginScreen;
		[SerializeField] private TMP_InputField _playerNumber;
		[SerializeField] private TMP_InputField _playerRoomID;
		[SerializeField] private Button _joinReadyButton;
		[SerializeField] private Button _hostReadyButton;
		
		[SerializeField] private TextMeshProUGUI _hostRoomID;
		[SerializeField] private TMP_InputField _hostNick;
		[SerializeField] private TMP_InputField _playerNick;

		private string _nickname;

        private void Start()
        {
			base.InitTemplate(_loginScreen, BuildTypes.ManualConnect);
		}

		// Use by UI button
		public void StartManual()
		{
			if (_hostNick.text == "" && _playerNick.text == "")
			{
				Debug.Log("Enter nickname");
				return;
			}

			_nickname = LoginScreenCore.IsHost ? _hostNick.text : _playerNick.text;

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
			_joinReadyButton.interactable = false;
			_hostReadyButton.interactable = false;
			_hostRoomID.text = roomId;
			StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, _nickname, ConnectionSteps.ConnectWithCreateSocket));
		}	

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
	}
}