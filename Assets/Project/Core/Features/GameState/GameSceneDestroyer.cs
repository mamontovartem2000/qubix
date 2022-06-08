using DG.Tweening;
using ME.ECS;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneDestroyer : MonoBehaviour
{
    private const float WaintingTime = 3f;
    private bool _needDestroyWorld;
    private bool _worldDestroyed;
    private float _leftTime = 0;
    private float _lastReceiveTime = 0f;

    private void Awake()
    {
        SystemMessages.GetSystemMessage += ResetLastReceiveTime;
        NetworkEvents.EndGame += SetDestroyFlag;
    }

    private void Update()
    {
        //if (NetworkData.Connected)
        //    CheckReceiveTime();

#if !UNITY_WEBGL || UNITY_EDITOR
        if (NetworkData.Connected)
            NetworkData.Connect.DispatchWebSocketMessageQueue();
#endif

        if (Worlds.currentWorld == null) return;
        if (_needDestroyWorld == false || _worldDestroyed) return;

        _leftTime += Time.deltaTime;

        if (_leftTime > WaintingTime)
            Disconnect();
    }

    private void CheckReceiveTime()
    {
        _lastReceiveTime += Time.deltaTime;

        if (_lastReceiveTime >= 10f)
        {
            //NetworkData.CloseConnect(true);
            Debug.Log("Disconnect last");
        }

        if (_lastReceiveTime >= 3f)
        {

        }
    }

    public void SetDestroyFlag()
    {
        _needDestroyWorld = true;
    }

    //TODO: Use this method for Front, not SetDestroyFlag
    public void TryDisconnect()
    {
        if (_worldDestroyed == false)
            Disconnect();
    }

    private void Disconnect()
    {
        Debug.Log("Start Disconnect");

        var buildType = NetworkData.BuildType;
        DOTween.KillAll();
        //Worlds.currentWorld.AddMarker(new NetworkPlayerDisconnected { ActorID = NetworkData.SlotInRoom });

        var go = FindObjectOfType<InitializerBase>();
        if (go != null)
        {
            DestroyImmediate(go.gameObject);
            Worlds.currentWorld = null;
        }

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

    private void ResetLastReceiveTime()
    {
        _lastReceiveTime = 0f;
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
        TryDisconnect();
    }
}
