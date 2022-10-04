namespace Project.Modules.Network
{
    public static class URL
    {
        public const string CreateRoom = "https://dev.match.qubixinfinity.io/match/create_room";

        public const string JoinRequest = "https://dev.match.qubixinfinity.io/match/test/join_request";
        //public const string JoinRequest = "https://flagmode.qubixinfinity.io/match/test/join_request";
        //public const string JoinRequest = "http://localhost:8001/test/join_request"; // Local connect for Ilusha

        public const string SocketConnect = "wss://dev.match.qubixinfinity.io/match";
        //public const string SocketConnect = "wss://flagmode.qubixinfinity.io/match";
        //public const string SocketConnect = "ws://localhost:8001"; // Local connect for Ilusha
        
        public const string MapFiles = "https://d3rsf7561wj274.cloudfront.net/temp_maps/";
    }
}
