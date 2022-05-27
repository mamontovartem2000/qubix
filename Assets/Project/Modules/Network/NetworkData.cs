using UnityEngine;

namespace Project.Modules.Network
{
    public static class NetworkData
    {
        public static WebSocketConnect Connect = null;
        public static GameInfo Info = null;
        public static int SlotInRoom = 1;
        public static string FullJoinRequest = string.Empty;
        public static uint GameSeed = 1;
        public static PlayerInfo[] PlayersInfo = null;
        public static string Team = string.Empty;
        public static BuildTypes BuildType = BuildTypes.PC;


        public static void CloseNetwork()
        {
            //Info = new GameInfo() { server_url = "url", room_id = "id777", player_icon = "www.png", player_nickname = "Dev player", map_id = 1, game_mode = "deathmatch", multiplayer_schema = null, available_characters = null, player_id = "qwerty" };
            Info = null;
            Connect.CloseClient();
            Info = null;
            SlotInRoom = 1;
            GameSeed = 1;
            FullJoinRequest = string.Empty;
            PlayersInfo = null; //TODO: Make Info with defaut 
            Team = string.Empty;
            BuildType = BuildTypes.PC;
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
}
