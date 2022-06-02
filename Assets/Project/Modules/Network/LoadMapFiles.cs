using Project.Modules.Network;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadMapFiles : MonoBehaviour
{
    private void Awake()
    {
        Stepsss.LoadMapFiles += StartLoadMaps;
    }

    private void StartLoadMaps()
    {
        string fileName = ((Maps)NetworkData.Info.map_id).ToString();
        string fileName2 = fileName + "_obj";

        StartCoroutine(GetMapFile(fileName, fileName2));
    }

    public static IEnumerator GetMapFile(string fileName, string fileName2)
    {
        UnityWebRequest request = null, request2 = null;

        if (fileName != string.Empty)
        {
            string url = $"https://d3rsf7561wj274.cloudfront.net/temp_maps/{fileName}.txt";
            request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
        }

        if (fileName2 != string.Empty)
        {
            string url2 = $"https://d3rsf7561wj274.cloudfront.net/temp_maps/{fileName2}.txt";
            request2 = UnityWebRequest.Get(url2);
            yield return request2.SendWebRequest();
        }

        NetworkData.FloorMap = request.downloadHandler.text;
        NetworkData.ObjectsMap = request2.downloadHandler.text;

        Debug.Log("Maps Loaded");
        //TODO: Need handle exceptions
    }
}
