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
        public static TeamTypes Team;
        public static BuildTypes BuildType;

        public static void CloseNetwork()
        {
            Connect.CloseClient();
            Connect = null;
        }

        public static void SetFakeSettings()
        {
            Info = new GameInfo() { server_url = "url", player_nickname = "Dev player", map_id = 1, game_mode = GameTypes.deathmatch, player_id = "qwerty" };
            SlotInRoom = 1;
            GameSeed = 1;
            FullJoinRequest = string.Empty;
            PlayersInfo = null;
            Team = TeamTypes.Null;
            BuildType = BuildTypes.PC;
        }

        public static bool FriendlyFireCheck(TeamTypes firstPlayer, TeamTypes secondPlayer)
        {
            if (Info.game_mode == GameTypes.deathmatch)
                return true;

            if (Info.game_mode == GameTypes.teambattle && firstPlayer != secondPlayer)
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
        PC,
        Front_Hub,
        Front_Room
    }

    public enum GameTypes
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
}
