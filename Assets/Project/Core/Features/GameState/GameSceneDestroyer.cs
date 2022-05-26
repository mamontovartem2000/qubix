using DG.Tweening;
using ME.ECS;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneDestroyer : MonoBehaviour
{
    private const float WaintingTime = 3f;
    private bool _needDestroyWorld;
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
        if (_needDestroyWorld == false) return;
        
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
        DestroyWorld();
        NetworkData.CloseNetwork();
        _needDestroyWorld = false;

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
