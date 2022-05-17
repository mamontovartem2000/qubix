using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Modules.Network
{
    public class ConnectingSteps : MonoBehaviour
	{
		[SerializeField] private GameObject _loginScreen;
		[SerializeField] private GameObject _selectionScreen;
		[SerializeField] private TMP_InputField _playerNumber;
		[SerializeField] private TMP_InputField _playerRoomID;
		[SerializeField] private Button _joinReadyButton;
		[SerializeField] private Button _hostReadyButton;
		[SerializeField] private CharacterSelection _select;
		
		[SerializeField] private TextMeshProUGUI _hostRoomID;
		
		[SerializeField] private TMP_InputField _hostNick;
		[SerializeField] private TMP_InputField _playerNick;

		private bool _needLoadGameScene, _needReloadThisScene;
		private string _nickname;

        private void Start()
        {
			Stepsss.LoadMainMenuScene += ReloadMenuScene;
			Stepsss.LoadGameScene += LoadGameScene;
			Stepsss.ShowCharacterSelectionWindow += SwapScreens;
		}

		private void Update()
		{
			if (_needLoadGameScene)
				SceneManager.LoadScene(1, LoadSceneMode.Single);

			if (_needReloadThisScene)
				SceneManager.LoadScene(0, LoadSceneMode.Single);

#if !UNITY_WEBGL || UNITY_EDITOR
			if (NetworkData.Connect != null)
				NetworkData.Connect.DispatchWebSocketMessageQueue();
#endif
		}

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
			StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, _nickname, Stepsss.ProcessJoinRequest));
		}	

        private void ReloadMenuScene()
        {
			_needReloadThisScene = true;
		}

		private void LoadGameScene()
		{
			_needLoadGameScene = true;
		}

		private void SwapScreens()
		{
			_loginScreen.SetActive(false);
			_selectionScreen.SetActive(true);
			
			//TODO: Add random selection
			var rnd = Random.Range(0, 3);
			_select.Select(rnd);
		}

		private void OnDestroy()
		{
			Stepsss.LoadMainMenuScene -= ReloadMenuScene;
			Stepsss.LoadGameScene -= LoadGameScene;
			Stepsss.ShowCharacterSelectionWindow -= SwapScreens;
		}
	}
}