using UnityEngine;

namespace Project.Modules.Network
{
    public static class NetworkData
    {
        public static WebSocketConnect Connect;
        public static bool Connected;
        public static GameInfo Info;
        public static int SlotInRoom;
        public static string FullJoinRequest;
        public static uint GameSeed;
        public static PlayerInfo[] PlayersInfo;
        public static TeamTypes Team;
        public static GameModes GameMode;
        public static BuildTypes BuildType;
        public static string FloorMap;
        public static string ObjectsMap;

        public static void CloseNetwork()
        {
            Connect.CloseSocket();
            Connect = null;
        }

        public static void SetFakeSettings()
        {
            Info = new GameInfo() { server_url = "url", player_nickname = "Dev player", map_id = 1, game_mode = "deathmatch", player_id = "qwerty" };
            SlotInRoom = 1;
            GameSeed = 1;
            FullJoinRequest = string.Empty;
            PlayersInfo = null;
            Team = TeamTypes.Null;
            BuildType = BuildTypes.ManualConnect;
        }

        public static bool FriendlyFireCheck(TeamTypes firstPlayer, TeamTypes secondPlayer)
        {
            if (GameMode == GameModes.deathmatch)
                return true;

            if (GameMode == GameModes.teambattle && firstPlayer != secondPlayer)
                return true;
            else
                return false;
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
        teambattle
    }

    public enum TeamTypes
    {
        Null,
        red,
        blue
    }

    public struct PlayerStats
    {
        public uint Kills;
        public uint Deaths;
        public string PlayerId;
        public TeamTypes Team;
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
