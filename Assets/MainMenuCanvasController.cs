using DG.Tweening;
using ME.ECS;
using Project.Features;
using Project.Features.Player.Components;
using Project.Features.Player.Markers;
using Project.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GlobalEvent _activateEvent;
    [SerializeField] private GlobalEvent _deactivateEvent;

    [SerializeField] private Image[] _images;
    [SerializeField] private TextMeshProUGUI[] _texts;
    [SerializeField] private GameObject _display;

    private void Start()
    {
        _activateEvent.Subscribe(ActivateMenu);
        _deactivateEvent.Subscribe(DeactivateMenu);
    }

    public void SelectSkin(int index)
    {
        var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
        Worlds.current.AddMarker(new SelectColorMarker {ActorID = id, ColorID = index});
    }

    private void DeactivateMenu(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
        foreach (var image in _images)
        {
            image.DOFade(0f, 0.5f).SetEase(Ease.Linear);
        }

        foreach (var text in _texts)
        {
            text.DOFade(0f, 0.5f).SetEase(Ease.Linear);
        }
    }

    public void SetPlayerReady()
    {
        var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
        Worlds.current.AddMarker(new PlayerReadyMarker {ActorID = id});
        
        Debug.Log($"pressed, id {id}");
    }

    private void ActivateMenu(in Entity entity)
    {

        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _deactivateEvent.Unsubscribe(DeactivateMenu);
        _activateEvent.Unsubscribe(ActivateMenu);
    }
}
