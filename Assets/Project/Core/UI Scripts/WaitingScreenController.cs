using ME.ECS;
using UnityEngine;
using UnityEngine.UI;

// using TMPro;

namespace Project.Common.UI_Scripts
{
    public class WaitingScreenController : MonoBehaviour
    {
        [SerializeField] private GlobalEvent _deactivateEvent;
    
        [SerializeField] private Image _background;
        [SerializeField] private Image _popup;
        // [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _deactivateEvent.Subscribe(Deactivate);
        }

        private void Deactivate(in Entity entity)
        {
            // _background.DOFade(0.0f, 0.3f).SetEase(Ease.Linear);
            // _popup.DOFade(0f, 0.3f).SetEase(Ease.Linear);
            // _text.DOFade(0, 0.3f).SetEase(Ease.Linear).OnComplete(TurnOff);
        }

        private void TurnOff()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _deactivateEvent.Unsubscribe(Deactivate);
        }
    }
}
