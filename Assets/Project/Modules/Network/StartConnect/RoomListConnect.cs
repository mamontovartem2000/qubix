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

		private void Start()
		{
			base.InitTemplate(_roomListScreen, BuildTypes.RoomsConnect);
			Stepsss.GetRoomList += ShowRooms;
			RoomPrefab.JoinRoom += SelectRoom;
			Stepsss.CreateSocketConnect("wss://game.qubixinfinity.io/match");
		}

        private void ShowRooms(RoomInfo[] obj)
		{
			string text = "Rooms: \n";

            for (int i = 0; i < 6; i++)
            {
				text += $"{obj[i].Id} {obj[i].PlayersCount} {obj[i].MaxPlayersCount}\n";
				_rooms[i].UpdateRoomInfo(obj[i], i + 1, _rooms.Length);
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
				StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, "Player" + rnd, Stepsss.ConnectWithoutCreateSocket));
			}
            else
            {
				Stepsss.ChangeRoomRequest(roomId);
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
            Stepsss.GetRoomList -= ShowRooms;
            RoomPrefab.JoinRoom -= SelectRoom;
        }
    }
}