using System;
using ME.ECS;
using Project.Common.Components;
using Project.Input.InputHandler.Markers;
using Project.Input.InputHandler.Modules;
using UnityEngine;
using TMPro;

public class TabulationShowScript : MonoBehaviour
{
    [SerializeField] private GameObject _tabulationScreen;
    [SerializeField] private GlobalEvent _tabulationScreenAddPlayerEvent;
    [SerializeField] private GlobalEvent _tabulationScreenNumbersChangedEvent;
    public PlayerTab[] PlayerInTab;
    
    private void Start()
    {
        HandlePlayerInput.Tabulation += TabulationScreenSwitch;
        _tabulationScreenAddPlayerEvent.Subscribe(TabulationScreenAddPlayer);
        _tabulationScreenNumbersChangedEvent.Subscribe(TabulationScreenNumbersChanged);
    }
    
    private void TabulationScreenNumbersChanged(in Entity entity)
    {
        PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].death.text = entity.Read<PlayerScore>().Deaths.ToString();
        PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].kill.text = entity.Read<PlayerScore>().Kills.ToString();
        PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].dealtDamage.text = (Mathf.Round(entity.Read<PlayerScore>().DealtDamage)).ToString();

        ChangeTabPosition(entity);
    }
    
    private void TabulationScreenAddPlayer(in Entity entity)
    {
        var player = PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID];
        player.kill.text = "0";
        player.death.text = "0";
        player.dealtDamage.text = "0";
        player.idInTab = entity.Read<PlayerTag>().PlayerLocalID;
        player.nickname.text = entity.Read<PlayerTag>().Nickname;
    }
    
    private void TabulationScreenSwitch(InputState input)
    {
        switch (input)
        {
            case InputState.Pressed:
            {
                _tabulationScreen.SetActive(true);
                break;
            }
            case InputState.Released:
            {
                _tabulationScreen.SetActive(false);
                break;
            }
        }
    }

    [Serializable]
    public struct PlayerTab
    {
        public Transform playerBlock;
        public TextMeshProUGUI nickname;
        public TextMeshProUGUI kill;
        public TextMeshProUGUI death;
        public TextMeshProUGUI dealtDamage;
        public int idInTab;
    }

    private int GetEntityIdInTab(Entity entity)
    {
        return PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].idInTab;
    }

    private int GetPlayerById(int id)
    {
        for (var i = 0; i < PlayerInTab.Length; i++)
        {
            if (PlayerInTab[i].idInTab == id)
            {
                return i;
            }
        }
        return 0;
    }
    
    private void ChangeTabPosition(in Entity entity)
    {
        while (true)
        {
            if (GetEntityIdInTab(entity) <= 0) break;

            ref var player = ref PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID];
            ref var upperPlayer = ref PlayerInTab[GetPlayerById(PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].idInTab - 1)];

            if (int.Parse(player.kill.text) > int.Parse(upperPlayer.kill.text))
            {
                (player.playerBlock.position, upperPlayer.playerBlock.position) = 
                    (upperPlayer.playerBlock.position, player.playerBlock.position);
                (player.idInTab, upperPlayer.idInTab) = (upperPlayer.idInTab, player.idInTab);
            }
            else if (int.Parse(player.dealtDamage.text) > int.Parse(upperPlayer.dealtDamage.text) && 
                     int.Parse(player.kill.text) == int.Parse(upperPlayer.kill.text))
            {
                (player.playerBlock.position, upperPlayer.playerBlock.position) = 
                    (upperPlayer.playerBlock.position, player.playerBlock.position);
                (player.idInTab, upperPlayer.idInTab) = (upperPlayer.idInTab, player.idInTab);
            }
            else break;

        }
    }

    private void OnDestroy() 
    {
        HandlePlayerInput.Tabulation -= TabulationScreenSwitch;
        _tabulationScreenAddPlayerEvent.Unsubscribe(TabulationScreenAddPlayer);
        _tabulationScreenNumbersChangedEvent.Unsubscribe(TabulationScreenNumbersChanged);
    }
}
