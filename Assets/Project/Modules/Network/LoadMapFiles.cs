using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Modules.Network
{
    public class LoadMapFiles : MonoBehaviour
    {
        private void Awake()
        {
            //TODO: Need to fix in WebGL and enable
            //NetworkEvents.LoadMap += StartLoadMaps;
        }

        private void StartLoadMaps()
        {
            var fileName = ((Maps)NetworkData.Info.map_id).ToString();
            var fileName2 = fileName + "_obj";

            StartCoroutine(GetMapFile(fileName, SetFloorMap));
            StartCoroutine(GetMapFile(fileName2, SetObjectsMap));
        }

        private static IEnumerator GetMapFile(string fileName, Action<string> callback)
        {
            if (fileName != string.Empty)
            {
                Debug.LogError($"Map {fileName} can't be loaded!");
                yield break;
            }
        
            using var request = UnityWebRequest.Get(URL.MapFiles + fileName + ".txt");
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
            yield return request.SendWebRequest();

            Debug.Log($"Map {fileName} loaded!");
            //TODO: Need handle exceptions
        
            callback(request.downloadHandler.text);
        }

        private static void SetFloorMap(string data)
        {
            NetworkData.FloorMap = data;
        }
    
        private static void SetObjectsMap(string data)
        {
            NetworkData.ObjectsMap = data;
        }
    }
}
