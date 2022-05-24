using ME.ECS;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Project.Common.Components;

public class GameSceneDestroyer : MonoBehaviour
{
    private const float WaintingTime = 2f;

    private float _leftTime = 0;

    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (NetworkData.Connect != null)
            NetworkData.Connect.DispatchWebSocketMessageQueue();
#endif

        if (Worlds.currentWorld == null) return;
        if (!Worlds.currentWorld.HasSharedData<GameFinished>()) return;
        
        _leftTime += Time.deltaTime;
        
        if (_leftTime > WaintingTime)
            Disconnect();
    }

    private void Disconnect()
    {
        var buildType = NetworkData.BuildType;
        
        DOTween.KillAll();
        DestroyWorld();
        NetworkData.CloseNetwork();

        if (buildType != BuildTypes.Front)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
#if UNITY_WEBGL && !UNITY_EDITOR
                    BrowserEvents.WorldDestroyed();
#endif
        }
    }

    private void DestroyWorld()
    {
        var go = FindObjectOfType<InitializerBase>();

        if (go != null)
        {
            DestroyImmediate(go.gameObject);
            Worlds.currentWorld = null;
        }

        Debug.Log("World destroyed");
    }
}
