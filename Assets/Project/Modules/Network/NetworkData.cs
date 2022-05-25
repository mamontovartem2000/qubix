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
        Front
    }
}
