using System;
using DG.Tweening;
using ME.ECS;
using Project.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingScreenController : MonoBehaviour
{
    [SerializeField] private GlobalEvent _deactivateEvent;
    
    [SerializeField] private Image _background;
    [SerializeField] private Image _popup;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _deactivateEvent.Subscribe(Deactivate);
    }

    private void Deactivate(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;

        _background.DOFade(0.0f, 1f).SetEase(Ease.Linear);
        _popup.DOFade(0f, 1f).SetEase(Ease.Linear);
        _text.DOFade(0, 1f).SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        _deactivateEvent.Unsubscribe(Deactivate);
    }
}
