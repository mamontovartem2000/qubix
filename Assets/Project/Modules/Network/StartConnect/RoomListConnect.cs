using UnityEngine;

namespace Project.Modules.Network
{
    public class RoomListConnect : ConnectTemplate
	{
		[SerializeField] private GameObject _roomListScreen;
		[SerializeField] private GameObject _loadingPanel;
		[SerializeField] private RoomPrefab[] _rooms;

		private bool _roomListLoaded;
		private bool _roomSelected;
		private const int MaxRoomCount = 4; 

		private void Start()
		{
			base.InitTemplate(_roomListScreen, BuildTypes.RoomsConnect);
			NetworkEvents.GetRoomList += ShowRooms;
			RoomPrefab.JoinRoom += SelectRoom;
			ConnectionSteps.CreateSocketConnect("wss://game.qubixinfinity.io/match");
			// ConnectionSteps.CreateSocketConnect("ws://192.168.32.85:8001"); // Local connect for Ilusha
		}

		private void ShowRooms(RoomInfo[] obj)
		{
			string text = "Rooms: \n";

            for (int i = 0; i < MaxRoomCount; i++)
            {
				text += $"{obj[i].Id} {obj[i].PlayersCount} {obj[i].MaxPlayersCount}\n";
				_rooms[i].UpdateRoomInfo(obj[i], i + 1, MaxRoomCount);
			}

			if (_roomListLoaded == false)
            {
				_roomListLoaded = true;
				ShowRoomList();
			}

			Debug.Log(text);
		}

		private void SelectRoom(string roomId)
		{
			if (_roomSelected == false)
            {
				_roomSelected = true;
				var rnd = Random.Range(0, 100000);
				StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, "Player" + rnd, ConnectionSteps.ConnectWithoutCreateSocket));
			}
            else
            {
				ConnectionSteps.ChangeRoomRequest(roomId);
            }
		}

		private void ShowRoomList()
		{
			_loadingPanel.SetActive(false);
			_roomListScreen.SetActive(true);
		}

        protected override void OnDestroy()
        {
			base.OnDestroy();
			NetworkEvents.GetRoomList -= ShowRooms;
            RoomPrefab.JoinRoom -= SelectRoom;
        }
    }
}