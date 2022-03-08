using System.Collections;
using DG.Tweening;
using ME.ECS;
using Project.Features;
using Project.Features.Player.Components;
using UnityEngine;
using UnityEngine.UI;
using Project.Utilities;
using TMPro;

public class UICore : MonoBehaviour
{
    [Header("Events References")]
    [SerializeField] private GlobalEvent _victoryScreenEvent;
    [SerializeField] private GlobalEvent _defeatScreenEvent;

    [SerializeField] private GlobalEvent _healthChangedEvent;
    [SerializeField] private GlobalEvent _scoreChangeEvent;

    [SerializeField] private GlobalEvent _leftWeaponFired;
    [SerializeField] private GlobalEvent _rightWeaponFired;
    [SerializeField] private GlobalEvent _leftWeaponDepleted;
    [SerializeField] private GlobalEvent _rightWeaponDepleted;
    
    [Header("Health and Score References")]
    [SerializeField] private Image _healthbar;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("Screens References")]
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;

    [Header("Weapon References")]
    [SerializeField] private Image _leftWeaponAmmoImage;
    [SerializeField] private Image _rightWeaponAmmoImage;

    [SerializeField] private Image _leftWeaponIcon;
    
    [SerializeField] private TextMeshProUGUI _leftWeaponAmmoText;
    [SerializeField] private TextMeshProUGUI _rightWeaponAmmoText;

    private void Start() 
    {
        _healthChangedEvent.Subscribe(HealthChanged);
        _victoryScreenEvent.Subscribe(ToggleWinScreen);
        _defeatScreenEvent.Subscribe(ToggleLoseScreen);
        _scoreChangeEvent.Subscribe(ChangeScore);
        _leftWeaponFired.Subscribe(ChangeLeftAmmo);
        _rightWeaponFired.Subscribe(ChangeRightAmmo);
        _leftWeaponDepleted.Subscribe(ReloadLeft);
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

    private void ChangeLeftAmmo(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
        var fill = (float)entity.Read<LeftWeapon>().Ammo / entity.Read<LeftWeapon>().MaxAmmo;
        _leftWeaponAmmoImage.fillAmount = fill;
        _leftWeaponAmmoText.SetText(entity.Read<LeftWeapon>().Ammo.ToString());
    }

    private void ChangeRightAmmo(in Entity entity)
    {
        if(!Utilitiddies.CheckLocalPlayer(entity)) return;
        
        var fill = (float)entity.Read<RightWeapon>().Count / entity.Read<RightWeapon>().MaxCount;
        _rightWeaponAmmoImage.fillAmount = fill;
        _rightWeaponAmmoText.SetText(entity.Read<RightWeapon>().Count.ToString());
    }

    private void ReloadLeft(in Entity entity)
    {
        var time = entity.Read<LeftWeaponReload>().Time;

        _leftWeaponIcon.fillAmount = 0;        
        _leftWeaponIcon.DOFillAmount(1, time);
        _leftWeaponAmmoImage.DOFillAmount(1, time);
    }

    private void OnDestroy() 
    {
        _healthChangedEvent.Unsubscribe(HealthChanged);
        _victoryScreenEvent.Unsubscribe(HealthChanged);
        _defeatScreenEvent.Unsubscribe(HealthChanged);;
        _scoreChangeEvent.Unsubscribe(ChangeScore);
        _leftWeaponFired.Unsubscribe(ChangeLeftAmmo);
        _rightWeaponFired.Unsubscribe(ChangeRightAmmo);
        _leftWeaponDepleted.Unsubscribe(ReloadLeft);
    }
}
