using ME.ECS;
using Project.Core.Features.GameState.Components;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameSceneDestroyer : MonoBehaviour
{
    private const float WaintingTime = 5f;

    private float _leftTime = 0;

    private void Update()
    {
        if (Worlds.currentWorld == null) return;
        if (!Worlds.currentWorld.HasSharedData<GameFinished>()) return;

        _leftTime += Time.deltaTime;

        if (_leftTime > WaintingTime)
            Disconnect();
    }

    private void Disconnect()
    {
        DOTween.KillAll();
        DestroyWorld();
        NetworkData.CloseNetwork();
        SceneManager.LoadScene(0);
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
