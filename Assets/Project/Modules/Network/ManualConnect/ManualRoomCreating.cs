using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Modules.Network
{
    public static class ManualRoomCreating
    {
        public static IEnumerator CreateRoom(int playerNumber, Action<string> callback)
        {
            // var roomRequest = new CreateRoomRequest()
            // {
            //     map_id = 1, player_scheme = new int[] {playerNumber, playerNumber}, lifetime = 60 * 5,
            //     game_mode = GameModes.teambattle.ToString()
            // }; // teambattle

            var roomRequest = new CreateRoomRequest()
            {
                map_id = 1, player_scheme = new int[] {playerNumber}, lifetime = 60 * 5,
                game_mode = GameModes.deathmatch.ToString()
            }; // deathmatch
            
            var json = JsonUtility.ToJson(roomRequest);

            using var request = UnityWebRequest.Post(URL.CreateRoom, new WWWForm());
            var postBytes = System.Text.Encoding.UTF8.GetBytes(json);
            var uploadHandler = new UploadHandlerRaw(postBytes);
            request.uploadHandler = uploadHandler;
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

            yield return request.SendWebRequest();

            var info = NetworkData.CreateFromJSON<Room>(request.downloadHandler.text);
            callback(info.id);
        }

        public static IEnumerator LoadJoinRequest(string roomId, string playerId, Action<string> callback)
        {
            var req = new Player() { room_id = roomId, player_id = playerId };
            var json = JsonUtility.ToJson(req);

            using var request = UnityWebRequest.Post(URL.JoinRequest, new WWWForm());
            var postBytes = System.Text.Encoding.UTF8.GetBytes(json);
            var uploadHandler = new UploadHandlerRaw(postBytes);
            request.uploadHandler = uploadHandler;
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

            yield return request.SendWebRequest();

            callback(request.downloadHandler.text);
        }
    }

    [Serializable]
    public class CreateRoomRequest
    {
        public int map_id;
        public int[] player_scheme;
        public int lifetime;
        public string game_mode;
    }

    [Serializable]
    public class Player
    {
        public string room_id;
        public string player_id;
    }

    [Serializable]
    public class Room
    {
        public string id;
        public int start_wait;
        public string[] players;
        public string state;
        public int create_at;
    }

    [Serializable]
    public class JoinRequestData
    {
        public string payload;
    }

    [Serializable]
    public class GameInfo
    {
        public string server_url;
        //public string room_id;
        //public string player_icon;
        public string player_nickname;
        public int map_id;
        public string game_mode;
        //public int[] multiplayer_schema;
        //public string[] available_characters;
        public string player_id;
        public Metadata[] nfts_metadata;
    }

    [Serializable]
    public class Metadata
    {
        public NftMetadata metadata;
    }

    [Serializable]
    public class NftMetadata
    {
        public string description;
        public string external_url;
        public string image;
        public string player_id;
        public string name;
        public NftAttribute[] attributes;
    }
    
    [Serializable]
    public class NftAttribute
    {
        public string trait_type;
        public int value;
        public int max_value;
    }
}
