using ME.ECS;
using Project.Features;
using Project.Features.Player.Components;
using UnityEngine;
using UnityEngine.UI;
using Project.Utilities;
using TMPro;

public class UICore : MonoBehaviour
{
    [SerializeField] private GlobalEvent _healthChangedEvent;
    [SerializeField] private GlobalEvent _victoryScreenEvent;
    [SerializeField] private GlobalEvent _defeatScreenEvent;
    [SerializeField] private GlobalEvent _scoreChangeEvent;

    [SerializeField] private Image _healthbar;
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;

    private void Start() 
    {
        _healthChangedEvent.Subscribe(HealthChanged);
        _victoryScreenEvent.Subscribe(ToggleWinScreen);
        _defeatScreenEvent.Subscribe(ToggleLoseScreen);
        _scoreChangeEvent.Subscribe(ChangeScore);
    }

    private void HealthChanged(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
        var fill = entity.Read<PlayerHealth>().Value / 100;
        _healthbar.fillAmount = fill;
        _healthbar.color = Color.Lerp(Color.red, Color.green, fill);
    }

    private void ChangeScore(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
        _scoreText.SetText(entity.Read<PlayerScore>().Value.ToString());
    }

    private void ToggleWinScreen(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
        _victoryScreen.SetActive(true);
    }

    private void ToggleLoseScreen(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
        _defeatScreen.SetActive(true);
    }
    
    private void OnDestroy() 
    {
        _healthChangedEvent.Unsubscribe(HealthChanged);
        _victoryScreenEvent.Unsubscribe(HealthChanged);
        _defeatScreenEvent.Unsubscribe(HealthChanged);;
        _scoreChangeEvent.Unsubscribe(ChangeScore);
    }

    
}
