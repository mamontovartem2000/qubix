using Project.Modules.Network.UI;
using UnityEngine;

namespace Project.Modules.Network.StartConnect
{
    public class RoomListConnect : ConnectTemplate
	{
		[SerializeField] private RoomListContainer _roomListContainer;
		[SerializeField] private GameObject _loadingPanel;

		private bool _roomListLoaded;
		private bool _roomSelected;

		private void Start()
		{
			base.InitTemplate(_roomListContainer.gameObject, BuildTypes.RoomsConnect);
			NetworkEvents.GetRoomList += HandleRoomsList;
			RoomPrefab.JoinRoom += SelectRoom;
			ConnectionSteps.CreateSocketConnect(URL.SocketConnect);
		}
		
		private void HandleRoomsList(RoomInfo[] rooms)
		{
			_roomListContainer.UpdateRoomsDisplay(rooms);
			
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
			_roomListContainer.gameObject.SetActive(true);
		}

        protected override void OnDestroy()
        {
			base.OnDestroy();
			NetworkEvents.GetRoomList -= HandleRoomsList;
            RoomPrefab.JoinRoom -= SelectRoom;
        }
    }
}