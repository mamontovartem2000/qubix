using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Project.Modules.Network
{
    public class FekConnect : MonoBehaviour
	{
		[SerializeField] private GameObject _roomListScreen;
		[SerializeField] private GameObject _selectionScreen;
		[SerializeField] private CharacterSelection _select;
		
		private bool _needLoadGameScene, _needReloadThisScene;
		private string _nickname;
		private bool _gameModeSelected = false;


		private void Start()
        {
			Stepsss.LoadMainMenuScene += ReloadMenuScene;
			Stepsss.LoadGameScene += LoadGameScene;
			Stepsss.ShowCharacterSelectionWindow += SwapScreens;

			Stepsss.GetRoomList += ShowRooms;
			Stepsss.CreateSocketConnect("wss://game.qubixinfinity.io/match");
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

		public void CreateRoom(int playerNumber)
        {
			if (_gameModeSelected) return;

			Debug.Log("click " + playerNumber);
			_gameModeSelected = true;
		}

		public void LeaveRoom()
		{
			if (_gameModeSelected == false) return;

			Debug.Log("Leave");
			_gameModeSelected = false;
		}

		private void ShowRooms(RoomInfo[] obj)
		{
			throw new NotImplementedException();
		}

		//public void StartManual()
		//{
		//	if (_hostNick.text == "" && _playerNick.text == "")
		//	{
		//		Debug.Log("Enter nickname");
		//		return;
		//	}

		//	_nickname = LoginScreenCore.IsHost ? _hostNick.text : _playerNick.text;

		//	if (LoginScreenCore.IsHost)
		//	{
		//		int num = Int32.Parse(_playerNumber.text);
		//		StartCoroutine(ManualRoomCreating.CreateRoom(num, GetManualJoinRequest));
		//	}
		//	else if (_playerRoomID.text != "")
		//		GetManualJoinRequest(_playerRoomID.text);
		//}

		//private void GetManualJoinRequest(string roomId)
		//{
		//	_joinReadyButton.interactable = false;
		//	_hostReadyButton.interactable = false;
		//	_hostRoomID.text = roomId;
		//	StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, _nickname, Stepsss.ProcessJoinRequest));
		//}	

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
			_roomListScreen.SetActive(false);
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