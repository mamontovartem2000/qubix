using DG.Tweening;
using ME.ECS;
using Project.Core.Features.Player;
using Project.Modules.Network;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class NewRespawnPanel : MonoBehaviour
    {
        [SerializeField] private GlobalEvent _activateEvent;
        [SerializeField] private GlobalEvent _deactivateEvent;
    
        [SerializeField] private Image _background;
        [SerializeField] private Image _popup;
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _activateEvent.Subscribe(Activate);
            _deactivateEvent.Subscribe(Deactivate);
        }

        private void Deactivate(in Entity entity)
        {
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(NetworkData.OrderId)) return;

            _background.DOFade(0, 0.5f).SetEase(Ease.Linear);
            _popup.DOFade(0, 0.25f).SetEase(Ease.Linear);
            _text.DOFade(0, 0.25f).SetEase(Ease.Linear);
        }
 
        private void Activate(in Entity entity)
        {
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(NetworkData.OrderId)) return;

            _background.DOFade(0.5f, 0.5f).SetEase(Ease.Linear);
            _popup.DOFade(1f, 1f).SetEase(Ease.Linear);
            _text.DOFade(1, 1f).SetEase(Ease.Linear);
            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            _text.SetText($"Respawn in 5...");
            yield return new WaitForSecondsRealtime(1f);
            _text.SetText($"Respawn in 4...");
            yield return new WaitForSecondsRealtime(1f);
            _text.SetText($"Respawn in 3...");
            yield return new WaitForSecondsRealtime(1f);
            _text.SetText($"Respawn in 2...");
            yield return new WaitForSecondsRealtime(1f);
            _text.SetText($"Respawn in 1...");
            yield return new WaitForSecondsRealtime(1f);
            _text.SetText($"Respawning...");
            yield return new WaitForSecondsRealtime(1f);
        }

        private void OnDestroy()
        {
            _activateEvent.Unsubscribe(Activate);
            _deactivateEvent.Unsubscribe(Deactivate);
        }
    }
}
