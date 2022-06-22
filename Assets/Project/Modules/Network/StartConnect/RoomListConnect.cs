using Project.Modules.Network.ManualConnect;
using Project.Modules.Network.UI;
using UnityEngine;

namespace Project.Modules.Network.StartConnect
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
			ConnectionSteps.CreateSocketConnect("wss://dev.match.qubixinfinity.io/match");
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
				var rnd = Random.Range(0f, 1f);
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