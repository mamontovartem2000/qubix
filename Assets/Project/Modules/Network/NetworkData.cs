using UnityEngine;

namespace Project.Modules.Network
{
    public static class NetworkData
    {
        public static WebSocketConnect Connect = null;
        public static GameInfo Info = null;
        public static int PlayerIdInRoom = 1;
        public static string FullJoinRequest = string.Empty;
        public static uint GameSeed = 1;
        public static PlayerInfo[] PlayersInfo = null;
        public static BuildTypes BuildType = BuildTypes.PC;

        public static void CloseNetwork()
        {
            Connect.CloseClient();
            Info = null;
            PlayerIdInRoom = 1;
            GameSeed = 1;
            FullJoinRequest = string.Empty;
            PlayersInfo = null;
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
