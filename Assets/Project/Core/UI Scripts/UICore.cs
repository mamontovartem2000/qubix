using ME.ECS;
using Project.Core.Features;
using Project.Core.Features.Player.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Common.UI_Scripts
{
    public class UICore : MonoBehaviour
    {
        [Header("Events References")] 
        [SerializeField] private GlobalEvent _victoryScreenEvent;
        [SerializeField] private GlobalEvent _defeatScreenEvent;
        [SerializeField] private GlobalEvent _drawScreenEvent;

        [SerializeField] private GlobalEvent _healthChangedEvent;

        [Header("Health and Score References")]
        [SerializeField] private Image _healthbar;

        [Header("Screens References")]
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _defeatScreen;
        [SerializeField] private GameObject _drawScreen;

        private void Start() 
        {
            _healthChangedEvent.Subscribe(HealthChanged);
            _victoryScreenEvent.Subscribe(ToggleWinScreen);
            _defeatScreenEvent.Subscribe(ToggleLoseScreen);
            _drawScreenEvent.Subscribe(ToggleDrawScreen);
        }

        private void HealthChanged(in Entity entity)
        {
            if(!SceneUtils.CheckLocalPlayer(entity)) return;
            
            var fill = entity.Read<PlayerHealth>().Value / 100;
            _healthbar.fillAmount = fill;
            _healthbar.color = Color.Lerp(Color.red, Color.green, fill);
        }

        private void ToggleWinScreen(in Entity entity)
        {
        
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
            _healthChangedEvent.Unsubscribe(HealthChanged);
            _victoryScreenEvent.Unsubscribe(ToggleWinScreen);
            _defeatScreenEvent.Unsubscribe(ToggleLoseScreen);
            _drawScreenEvent.Subscribe(ToggleDrawScreen);
        }
    }
}
