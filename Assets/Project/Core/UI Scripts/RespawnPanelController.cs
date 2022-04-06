using System.Collections;
using ME.ECS;
using UnityEngine;
using UnityEngine.UI;

// using TMPro;

namespace Project.Common.UI_Scripts
{
    public class RespawnPanelController : MonoBehaviour
    {
        [SerializeField] private GlobalEvent _activateEvent;
        [SerializeField] private GlobalEvent _deactivateEvent;
    
        [SerializeField] private Image _background;
        [SerializeField] private Image _popup;
        // [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _activateEvent.Subscribe(Activate);
            _deactivateEvent.Subscribe(Deactivate);
        }

        private void Deactivate(in Entity entity)
        {
            // if(!Utilitiddies.CheckLocalPlayer(entity)) return;

            // _background.DOFade(0, 0.5f).SetEase(Ease.Linear);
            // _popup.DOFade(0, 0.25f).SetEase(Ease.Linear);
            // _text.DOFade(0, 0.25f).SetEase(Ease.Linear);
        }
 
        private void Activate(in Entity entity)
        {
            // if(!Utilitiddies.CheckLocalPlayer(entity)) return;

            // Debug.Log($"Activate, player: {Utilitiddies.CheckLocalPlayer(entity)}");

            // _background.DOFade(0.5f, 0.5f).SetEase(Ease.Linear);
            // _popup.DOFade(1f, 1f).SetEase(Ease.Linear);
            // _text.DOFade(1, 1f).SetEase(Ease.Linear);
            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            // _text.SetText($"Respawn in 5...");
            yield return new WaitForSecondsRealtime(1f);
            // _text.SetText($"Respawn in 4...");
            yield return new WaitForSecondsRealtime(1f);
            //_text.SetText($"Respawn in 3...");
            yield return new WaitForSecondsRealtime(1f);
            //_text.SetText($"Respawn in 2...");
            yield return new WaitForSecondsRealtime(1f);
            //_text.SetText($"Respawn in 1...");
            yield return new WaitForSecondsRealtime(1f);
            //_text.SetText($"Respawning...");
        }

        private void OnDestroy()
        {
            _activateEvent.Unsubscribe(Activate);
            _deactivateEvent.Unsubscribe(Deactivate);
        }
    }
}
