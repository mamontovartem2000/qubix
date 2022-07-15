using UnityEngine;

namespace Project.Modules.Network
{
    public static class NetworkData
    {
        public static WebSocketConnect Connect;
        public static GameInfo Info;
        public static int SlotInRoom;
        public static string FullJoinRequest;
        public static uint GameSeed;
        public static PlayerInfo[] PlayersInfo;
        public static int Team;
        public static GameModes GameMode;
        public static BuildTypes BuildType;
        public static string FloorMap;
        public static string ObjectsMap;

        public static void CloseNetwork()
        {
            if (Connect != null)
            {
                Connect.CloseClient();
                Connect = null;
            }
        }

        public static void SetFakeSettings()
        {
            Info = new GameInfo() { server_url = "url", player_nickname = "Dev player", map_id = 1, game_mode = "deathmatch", player_id = "qwerty" };
            SlotInRoom = 1;
            GameSeed = 1;
            FullJoinRequest = string.Empty;
            PlayersInfo = null;
            Team = 0;
            BuildType = BuildTypes.ManualConnect;
        }

        public static T CreateFromJSON<T>(string jsonString)
        {
            return JsonUtility.FromJson<T>(jsonString);
        }   
    }

    public enum BuildTypes
    {
        ManualConnect,
        Front_Hub,
        RoomsConnect
    }

    public enum GameModes
    {
        deathmatch,
        teambattle,
        flagCapture
    }

    public struct PlayerStats
    {
        public uint Kills;
        public uint Deaths;
        public string PlayerId;
        public int Team;
    }

    public struct PlayerInfo
    {
        public string Id;
        public int Slot;
        public string Nickname;
        public string Character;
        public string Icon;
    }

    public struct RoomInfo
    {
        public string Id;
        public uint PlayersCount;
        public uint MaxPlayersCount;
    }
}
