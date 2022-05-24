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
            string url = "https://game.qubixinfinity.io/match/create_room";
            Reqqq req = new Reqqq() { map_id  = 1, player_scheme = new int[] { playerNumber, 1 }, lifetime = 60 * 5 };
            string json = JsonUtility.ToJson(req);

            WWWForm formData = new WWWForm();

            UnityWebRequest request = UnityWebRequest.Post(url, formData);

            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(json);

            UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

            request.uploadHandler = uploadHandler;

            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

            yield return request.SendWebRequest();

            Room info = NetworkData.CreateFromJSON<Room>(request.downloadHandler.text);
            callback(info.id);
        }

        public static IEnumerator LoadJoinRequest(string roomid, string nick, Action<string> callback)
        {
            string url = "https://game.qubixinfinity.io/match/test/join_request";
            Player req = new Player() { room_id = roomid, player_id = nick };
            string json = JsonUtility.ToJson(req);

            WWWForm formData = new WWWForm();

            UnityWebRequest request = UnityWebRequest.Post(url, formData);

            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(json);

            UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

            request.uploadHandler = uploadHandler;

            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

            yield return request.SendWebRequest();

            callback(request.downloadHandler.text);
        }
    }

    [Serializable]
    public class Reqqq
    {
        public int map_id;
        public int[] player_scheme;
        public int lifetime;
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
        public string room_id;
        public string player_icon;
        public string player_nickname;
        public int map_id;
        public string game_mode;
        public int[] multiplayer_schema;
        public string[] available_characters;
        public string player_id;
    }
}
