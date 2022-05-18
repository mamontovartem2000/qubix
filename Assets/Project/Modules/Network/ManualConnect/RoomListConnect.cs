using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Project.Modules.Network
{
	public class RoomListConnect : MonoBehaviour
	{
		public static Action ShowSelectedRoom;

		[SerializeField] private GameObject _roomListScreen;
		[SerializeField] private CharacterSelection _select;
		[SerializeField] private Loading _loadingImage;
		[SerializeField] private LeaveRoomButton _leaveButton;
		[SerializeField] private RoomPrefab[] _rooms;

		private bool _needLoadGameScene, _needReloadThisScene;
		private bool _roomListLoaded;

		private void Start()
		{
			Stepsss.LoadMainMenuScene += ReloadMenuScene;
			Stepsss.LoadGameScene += LoadGameScene;
			Stepsss.ShowCharacterSelectionWindow += SwapScreens;

			Stepsss.GetRoomList += ShowRooms;
			LeaveRoomButton.LeaveRoomAction += ReloadMenuScene;
			Stepsss.CreateSocketConnect("wss://game.qubixinfinity.io/match");
			RoomPrefab.ChooseRoom += SelectRoom;
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

		private void ShowRooms(RoomInfo[] obj)
		{
			//Debug.Log("Rooms: ");

            for (int i = 0; i < 5; i++)
            {
				//Debug.Log($"{obj[i].Id} {obj[i].PlayersCount} {obj[i].MaxPlayersCount}");
				_rooms[i].UpdateRoomInfo(obj[i], i + 1);
			}

			if (_roomListLoaded == false)
            {
				_roomListLoaded = true;
				ShowRoomList();
			}
		}

		private void SelectRoom()
		{
			ShowSelectedRoom?.Invoke();
			_leaveButton.gameObject.SetActive(true);
		}

		private void ReloadMenuScene()
        {
			_needReloadThisScene = true;
		}

		private void LoadGameScene()
		{
			_needLoadGameScene = true;
		}

		private void ShowRoomList()
		{
			_loadingImage.gameObject.SetActive(false);
			_roomListScreen.SetActive(true);
		}

		private void SwapScreens()
		{
			_roomListScreen.SetActive(false);
			_select.gameObject.SetActive(true);
			_leaveButton.gameObject.SetActive(false);
			var rnd = Random.Range(0, 3);
			_select.Select(rnd);
		}

		private void OnDestroy()
		{
			Stepsss.LoadMainMenuScene -= ReloadMenuScene;
			Stepsss.LoadGameScene -= LoadGameScene;
			Stepsss.ShowCharacterSelectionWindow -= SwapScreens;
			Stepsss.GetRoomList -= ShowRooms;
			RoomPrefab.ChooseRoom -= SelectRoom;
			LeaveRoomButton.LeaveRoomAction -= ReloadMenuScene;
		}
	}
}