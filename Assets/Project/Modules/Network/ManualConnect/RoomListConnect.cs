using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Modules.Network
{
    public class RoomListConnect : MonoBehaviour
	{
		[SerializeField] private GameObject _roomListScreen;
		[SerializeField] private GameObject _loadingPanel;
		[SerializeField] private CharacterSelection _select;
		[SerializeField] private RoomPrefab[] _rooms;

		private bool _needLoadGameScene, _needReloadThisScene;
		private bool _roomListLoaded;
		private bool _roomSelected;

		private void Start()
		{
			Stepsss.LoadMainMenuScene += ReloadMenuScene;
			Stepsss.LoadGameScene += LoadGameScene;
			WaitingRoomTimer.ShowCharacterSelectionWindow += SwapScreens;

			Stepsss.GetRoomList += ShowRooms;
			RoomPrefab.JoinRoom += SelectRoom;
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

		private void ShowRooms(RoomInfo[] obj)
		{
			Debug.Log("Rooms: ");

            for (int i = 0; i < obj.Length; i++)
            {
				Debug.Log($"{obj[i].Id} {obj[i].PlayersCount} {obj[i].MaxPlayersCount}");
				_rooms[i].UpdateRoomInfo(obj[i], i + 1);
			}

			if (_roomListLoaded == false)
            {
				_roomListLoaded = true;
				ShowRoomList();
			}
		}

		private void SelectRoom(string roomId)
		{
			if (_roomSelected == false)
            {
				_roomSelected = true;
				var rnd = UnityEngine.Random.Range(0f, 1f);
				StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, "Player" + rnd, Stepsss.ProcessJoinRequestWithoutSocket));
			}
            else
            {
				Stepsss.ChangeRoomRequest(roomId);
            }

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
			_loadingPanel.SetActive(false);
			_roomListScreen.SetActive(true);
		}

		private void SwapScreens()
		{
			_roomListScreen.SetActive(false);
			_select.gameObject.SetActive(true);
			var rnd = UnityEngine.Random.Range(0, 3);
			_select.Select(rnd);
		}

		private void OnDestroy()
		{
			Stepsss.LoadMainMenuScene -= ReloadMenuScene;
			Stepsss.LoadGameScene -= LoadGameScene;
			WaitingRoomTimer.ShowCharacterSelectionWindow -= SwapScreens;
			Stepsss.GetRoomList -= ShowRooms;
		}
	}
}