using DG.Tweening;
using ME.ECS;
using Project.Markers;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneDestroyer : MonoBehaviour
{
    private const float WaintingTime = 3f;
    private bool _needDestroyWorld;
    private bool _worldDestroyed;
    private float _leftTime = 0;

    private void Awake()
    {
        SystemMessages.DestroyWorld += SetDestroyFlag;
    }

    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (NetworkData.Connect != null)
            NetworkData.Connect.DispatchWebSocketMessageQueue();
#endif

        if (Worlds.currentWorld == null) return;
        if (_needDestroyWorld == false || _worldDestroyed) return;
        
        _leftTime += Time.deltaTime;
        
        if (_leftTime > WaintingTime)
            Disconnect();
    }

    public void SetDestroyFlag()
    {
        _needDestroyWorld = true;
    }

    private void Disconnect()
    {
        var buildType = NetworkData.BuildType;
        DOTween.KillAll();
        Worlds.currentWorld.AddMarker(new NetworkPlayerDisconnected { ActorID = NetworkData.SlotInRoom });

        DestroyWorld();
        NetworkData.CloseNetwork();
        _needDestroyWorld = false;
        _worldDestroyed = true;
        
        if (buildType == BuildTypes.Front_Hub)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
                    BrowserEvents.WorldDestroyed();
#endif
             Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(0);
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

    private void OnDestroy()
    {
        SetDestroyFlag();
    }
}
