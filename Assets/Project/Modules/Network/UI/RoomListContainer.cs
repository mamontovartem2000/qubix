using UnityEngine;

namespace Project.Modules.Network.UI
{
    public class RoomListContainer : MonoBehaviour
    {
        [SerializeField] private RoomPrefab[] _rooms;

        private const int MaxRoomCount = 4; 
    
        public void UpdateRoomsDisplay(RoomInfo[] rooms)
        {
            var text = "Rooms: \n";

            for (var i = 0; i < MaxRoomCount; i++)
            {
                text += $"{rooms[i].Id} {rooms[i].PlayersCount} {rooms[i].MaxPlayersCount}\n";
                _rooms[i].UpdateRoomInfo(rooms[i], i + 1, MaxRoomCount);
            }
            
            //Debug.Log(text);
        }
    }
}
