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
            //fullRequest = loadInitialDataForUnity();
            fullRequest = "{\"payload\":\"eyJpc19zeXN0ZW0iOnRydWUsInR5cGUiOiJqb2luX3JlcXVlc3QiLCJyb29tX2lkIjoiNWJGN2tNRXpRV2FlRk9ON3lOVm9GUS8yMiIsInBsYXllcl9pZCI6InBsYXllcjMiLCJwbGF5ZXJfbmlja25hbWUiOiJwbGF5ZXIzIiwicGxheWVyX2ljb24iOiJwbGF5ZXIzLnN2ZyIsImF2YWlsYWJsZV9jaGFyYWN0ZXJzIjpbInJ1c3R5X2pvZSIsInJlZF9yb2RnZXIiXX0\",\"signatures\":[{\"protected\":\"eyJhbGciOiJSUzI1NiIsImtpZCI6IjE2NDk4NjM5MzE4NDJ8bWF0Y2htYWtlciJ9\",\"signature\":\"CNQlrhGMrd2u2tWE5dmejgYE957oaxchMGlCue9iH4VxSyqgqkjUvTEN05d7K23qmfhAAqZDxD6NHZ4Opsn0fOsV5y5wQEFz3RboW8Euz0bFKIukS5B4rWUgdBS7Zjxld9nkmPw5SKgW5YDhme5WgsQcbIZojxKrEpnjb_MBdZI\"}]}";
            string payloadBase64 = CreateFromJSON<JoinRequestData>(fullRequest).payload;
            var payloadInBytes = Convert.FromBase64String(payloadBase64);
            var playerJson = Encoding.UTF8.GetString(payloadInBytes);
            info = CreateFromJSON<GameInfo>(playerJson);
            Debug.Log(info.map_id);
        }

        public static void LoadBrowserInfo22(string fullRequest, out GameInfo info)
        {
            string payloadBase64 = CreateFromJSON<JoinRequestData>(fullRequest).payload;
            var payloadInBytes = Convert.FromBase64String(payloadBase64);
            var playerJson = Encoding.UTF8.GetString(payloadInBytes);
            info = CreateFromJSON<GameInfo>(playerJson);
            Debug.Log(info.map_id);
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
