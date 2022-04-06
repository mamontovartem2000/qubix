using ME.ECS;
using Project.Core.Features;
using UnityEngine;

namespace Project.Common.UI_Scripts
{
    public class NewUICore : MonoBehaviour
    {
        [SerializeField] private GlobalEvent _victoryScreenEvent;
        [SerializeField] private GlobalEvent _defeatScreenEvent;
        [SerializeField] private GlobalEvent _drawScreenEvent;

        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _defeatScreen;
        [SerializeField] private GameObject _drawScreen;

        private void Start() 
        {
            _victoryScreenEvent.Subscribe(ToggleWinScreen);
            _defeatScreenEvent.Subscribe(ToggleLoseScreen);
            _drawScreenEvent.Subscribe(ToggleDrawScreen);
        }

        private void ToggleWinScreen(in Entity entity)
        {
            // if(!SceneUtils.CheckLocalPlayer(entity)) return;
            _victoryScreen.SetActive(true);
        }

        private void ToggleLoseScreen(in Entity entity)
        {
            if(!SceneUtils.CheckLocalPlayer(entity)) return;
            _defeatScreen.SetActive(true);
        }
    
        private void ToggleDrawScreen(in Entity entity)
        {
            if(!SceneUtils.CheckLocalPlayer(entity)) return;
            _drawScreen.SetActive(true);
        }

        private void OnDestroy() 
        {
            _victoryScreenEvent.Unsubscribe(ToggleWinScreen);
            _defeatScreenEvent.Unsubscribe(ToggleLoseScreen);
            _drawScreenEvent.Subscribe(ToggleDrawScreen);
        }
    }
}
