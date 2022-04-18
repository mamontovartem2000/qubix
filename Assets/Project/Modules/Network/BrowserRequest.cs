using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


namespace Project.Modules.Network
{
    public static class BrowserRequest
    {
        [DllImport("__Internal")]
        public static extern string loadInitialDataForUnity();

        public static void LoadBrowserInfo(out string fullRequest, out GameInfo info)
        {
            fullRequest = loadInitialDataForUnity();
            string payloadBase64 = CreateFromJSON<JoinRequestData>(fullRequest).payload;
            var payloadInBytes = Convert.FromBase64String(payloadBase64);
            var playerJson = Encoding.UTF8.GetString(payloadInBytes);
            info = CreateFromJSON<GameInfo>(playerJson);
        }

        public static void LoadBrowserInfo22(string fullRequest, out GameInfo info)
        {
            string payloadBase64 = CreateFromJSON<JoinRequestData>(fullRequest).payload;
            var payloadInBytes = Convert.FromBase64String(payloadBase64);
            var playerJson = Encoding.UTF8.GetString(payloadInBytes);
            info = CreateFromJSON<GameInfo>(playerJson);
        }

        public static IEnumerator Loadroom(Action<string> callback)
        {
            string url = "http://13.250.155.40:80/match/create_room";
            Reqqq req = new Reqqq() { map_id  = 15, player_scheme = new int[] { 2 }, lifetime = 60 * 5 };
            string json = JsonUtility.ToJson(req);

            WWWForm formData = new WWWForm();

            UnityWebRequest request = UnityWebRequest.Post(url, formData);

            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(json);

            UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

            request.uploadHandler = uploadHandler;

            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

            yield return request.SendWebRequest();


            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Request Error: " + request.error);              
            }
            else
            {
                Room info = CreateFromJSON<Room>(request.downloadHandler.text);
                callback(info.id);
            }
        }

        public static IEnumerator Loadreq(string roomid, Action<string> callback)
        {
            string url = "http://13.250.155.40:80/match/test/join_request";
            Player req = new Player() { room_id = roomid, player_id = "sergo" + NetworkData.OrderId }; //TODO: Тут капец временное решение
            string json = JsonUtility.ToJson(req);

            WWWForm formData = new WWWForm();

            UnityWebRequest request = UnityWebRequest.Post(url, formData);

            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(json);

            UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

            request.uploadHandler = uploadHandler;

            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

            yield return request.SendWebRequest();


            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Request Error: " + request.error);
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }


        private static T CreateFromJSON<T>(string jsonString) //TODO: Перенести в утилиты
        {
            return JsonUtility.FromJson<T>(jsonString);
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
        public string room_id;
        public string player_id;
        public int map_id;
        public string game_mode;
        public int[] multiplayer_schema;
        public string type;
        public string player_nickname;
        public string player_icon;
        public string server_url;
        public string[] available_characters;
    }
}
