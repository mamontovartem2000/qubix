using ME.ECS;
using UnityEngine;
using UnityEngine.UI;

// using TMPro;

namespace Project.Common.UI_Scripts
{
    public class MainMenuCanvasController : MonoBehaviour
    {
        [SerializeField] private GlobalEvent _activateEvent;
        [SerializeField] private GlobalEvent _deactivateEvent;

        [SerializeField] private Image[] _images;
        // [SerializeField] private TextMeshProUGUI[] _texts;
        [SerializeField] private GameObject _display;

        private void Start()
        {
            _activateEvent.Subscribe(ActivateMenu);
            _deactivateEvent.Subscribe(DeactivateMenu);
        }


        private void DeactivateMenu(in Entity entity)
        {
            // if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
            foreach (var image in _images)
            {
                // image.DOFade(0f, 0.5f).SetEase(Ease.Linear);
            }

            // foreach (var text in _texts)
            {
                // text.DOFade(0f, 0.5f).SetEase(Ease.Linear);
            }
        }

        // public void SetPlayerReady()
        // {
        //     var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
        //     Worlds.current.AddMarker(new PlayerReadyMarker {ActorID = id});
        // }

        private void ActivateMenu(in Entity entity)
        {
            // if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
            // gameObject.SetActive(true);
            //
            // if (entity.Has<Initialized>())
            // {
            //     _display.transform.position = entity.GetPosition() + new Vector3(0f, 1.5f, -1f);
            // }
        }

        private void OnDestroy()
        {
            _deactivateEvent.Unsubscribe(DeactivateMenu);
            _activateEvent.Unsubscribe(ActivateMenu);
        }
    }
}
