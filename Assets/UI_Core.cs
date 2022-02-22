using System;
using ME.ECS;
using Photon.Pun;
using Project.Features;
using Project.Features.Player.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Core : MonoBehaviour
{
    public GlobalEvent PassLocalPlayer;
    public GlobalEvent HealthChangedEvent;
    public GlobalEvent MineCollision;

    [SerializeField] private Image _healthbar; 
    [SerializeField] private TextMeshProUGUI _scoreText;
    // [SerializeField] private TextMeshProUGUI _healthTest;
    
    private Entity _player;

    void Start() {
        this.PassLocalPlayer.Subscribe(this.PassLocalPlayerCallback);
        
        this.HealthChangedEvent.Subscribe(this.ChangeHealthCallback);
        this.HealthChangedEvent.Subscribe(this.ChangeScoreCallback);

        this.MineCollision.Subscribe(this.ChangeHealthCallback);
    }

    void OnDestroy() {
        this.PassLocalPlayer.Unsubscribe(this.PassLocalPlayerCallback);
        
        this.HealthChangedEvent.Unsubscribe(this.ChangeHealthCallback);
        this.HealthChangedEvent.Unsubscribe(this.ChangeScoreCallback);
        
        this.MineCollision.Unsubscribe(this.ChangeHealthCallback);
    }

    private void ChangeHealthCallback(in Entity entity)
    {
        if (entity != Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer()) return;
        
            var fill = entity.Read<PlayerHealth>().Value / 100;
            _healthbar.fillAmount = fill;
            _healthbar.color = Color.Lerp(Color.red, Color.green, fill);
    }

    private void ChangeScoreCallback(in Entity entity)
    {
        // if(_player.IsAlive() && _player == entity)
            // _scoreText.SetText(entity.Read<PlayerScore>().Value.ToString());
    }
    
    private void PassLocalPlayerCallback(in Entity entity)
    {
        _player = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer();
        // if (entity.Read<PlayerTag>().PlayerID == PhotonNetwork.LocalPlayer.ActorNumber)
        //     _player = entity;
        // //_healthTest.SetText(entity.Read<PlayerTag>().PlayerID.ToString());
    }
}
