using System;
using FlatMessages;

namespace Project.Modules.Network.UI
{
    public class NetworkEvents
    {
        public static Action LoadMap;
        public static Action LoadMainMenuScene;
        public static Action LoadGameScene;
        public static Action<TimeRemaining> SetTimer;
        public static Action<RoomInfo[]> GetRoomList;
        public static Action DestroyWorld;
    }
}
