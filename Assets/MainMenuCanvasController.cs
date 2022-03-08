using ME.ECS;
using Project.Features;
using Project.Markers;
using Project.Utilities;
using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GlobalEvent _deactivateEvent;
    [SerializeField] private GameObject _button;
    private void Start()
    {
        _deactivateEvent.Subscribe(DeactivateMenu);
    }

    public void SetPlayerReady()
    {
        Worlds.current.AddMarker(new NetworkPlayerReady {ActorID = Worlds.current.GetFeature<PlayerFeature>().GetPlayerID()});
        _button.SetActive(false);
    }

    private void DeactivateMenu(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _deactivateEvent.Unsubscribe(DeactivateMenu);
    }
}
