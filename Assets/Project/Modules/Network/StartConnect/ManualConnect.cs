using TMPro;
using UnityEngine;

namespace Project.Modules.Network.StartConnect
{
    public class ManualConnect : ConnectTemplate
	{
		[SerializeField] private GameObject _loginScreen;
		[SerializeField] private TMP_InputField _playerNumber;
		[SerializeField] private TMP_InputField _playerRoomID;
		
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
				var num = int.Parse(_playerNumber.text);
				StartCoroutine(ManualRoomCreating.CreateRoom(num, GetManualJoinRequest));
			}
			else if (_playerRoomID.text != "")
				GetManualJoinRequest(_playerRoomID.text);
		}

		private void GetManualJoinRequest(string roomId)
		{
			_hostRoomID.text = roomId;
			StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, _nickname, ConnectionSteps.ConnectWithCreateSocket));
		}
	}
}