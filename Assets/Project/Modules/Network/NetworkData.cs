namespace Project.Modules.Network
{
    public static class NetworkData
    {
        public static WebSocketConnect Connect;
        public static GameInfo Info;
        public static int PlayerIdInRoom = 1;
        public static string FullJoinRequest = string.Empty;
        public static uint GameSeed = 1;
        public static PlayerInfo[] PlayersInfo;

        public static void CloseNetwork()
        {
            Connect.CloseClient();
            Info = null;
            PlayerIdInRoom = 0;
            GameSeed = 1;
            FullJoinRequest = string.Empty;
            PlayersInfo = null;
        }
    }
}
