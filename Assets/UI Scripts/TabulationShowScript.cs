using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Core.Features.Player;
using Project.Modules.Network;
using UnityEngine;
using TMPro;

public class TabulationShowScript : MonoBehaviour
{
    public GameObject TabulationScreen;
    [SerializeField] private GlobalEvent _tabulationScreenOnEvent;
    [SerializeField] private GlobalEvent _tabulationScreenOffEvent;
    [SerializeField] private GlobalEvent _tabulationScreenAddPlayerEvent;
    [SerializeField] private GlobalEvent _tabulationScreenNumbersChangedEvent;
    [SerializeField] private GlobalEvent _tabulationScreenNewPlayerStats;
    public PlayerTab[] PlayerInTab;
    private void Start() 
    {
        _tabulationScreenOnEvent.Subscribe(TabulationScreenON);
        _tabulationScreenOffEvent.Subscribe(TabulationScreenOFF);
        _tabulationScreenAddPlayerEvent.Subscribe(TabulationScreenAddPlayer);
        _tabulationScreenNumbersChangedEvent.Subscribe(TabulationScreenNumbersChanged);
        _tabulationScreenNewPlayerStats.Subscribe(ChangeTabPosition);
    }
    
    private void TabulationScreenNumbersChanged(in Entity entity)
    {
        PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].death.text = entity.Read<PlayerScore>().Deaths.ToString();
        PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].kill.text = entity.Read<PlayerScore>().Kills.ToString();
        
    }
    
    private void TabulationScreenAddPlayer(in Entity entity)
    {
        var player = PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID];
        player.kill.text = "0";
        player.death.text = "0";
        player.idInTab = entity.Read<PlayerTag>().PlayerLocalID;
        player.nickname.text = entity.Read<PlayerTag>().Nickname;
        
        if (entity.Read<PlayerTag>().Team == TeamTypes.blue)
        {
            player.nickname.color = Color.blue;
        }
        else if (entity.Read<PlayerTag>().Team == TeamTypes.red)
        {
            player.nickname.color = Color.red;
        }
    }
    
    private void TabulationScreenON(in Entity entity)
    {
        if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
        
        TabulationScreen.SetActive(true);
    }
    
    private void TabulationScreenOFF(in Entity entity)
    {
        if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
        
        TabulationScreen.SetActive(false);
    }
    
    [System.Serializable]
    public struct PlayerTab
    {
        public TextMeshProUGUI nickname;
        public TextMeshProUGUI kill;
        public TextMeshProUGUI death;
        public int idInTab;
        public Transform playerBlock;
    }
    
    public int GetEntityIdInTab(Entity entity)
    {
        return PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].idInTab;
    }
    
    public int GetPlayerById(int id)
    {
        for (int i = 0; i < PlayerInTab.Length; i++)
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

            if (int.Parse(player.kill.text) < int.Parse(upperPlayer.kill.text))  break;

            (player.playerBlock.position, upperPlayer.playerBlock.position) = 
                (upperPlayer.playerBlock.position, player.playerBlock.position);
            (player.idInTab, upperPlayer.idInTab) = (upperPlayer.idInTab, player.idInTab);
            Debug.Log("swapped");
        }
    }
    
    private void OnDestroy() 
    {
        _tabulationScreenOnEvent.Unsubscribe(TabulationScreenON);
        _tabulationScreenOffEvent.Unsubscribe(TabulationScreenOFF);
        _tabulationScreenAddPlayerEvent.Unsubscribe(TabulationScreenAddPlayer);
        _tabulationScreenNumbersChangedEvent.Unsubscribe(TabulationScreenNumbersChanged);
        _tabulationScreenNewPlayerStats.Unsubscribe(ChangeTabPosition);

    }
}
