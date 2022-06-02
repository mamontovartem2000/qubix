using ME.ECS;
using Project.Common.Components;
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
    public PlayerTab[] PlayerInTab;
    private void Start() 
    {
        _tabulationScreenOnEvent.Subscribe(TabulationScreenON);
        _tabulationScreenOffEvent.Subscribe(TabulationScreenOFF);
        _tabulationScreenAddPlayerEvent.Subscribe(TabulationScreenAddPlayer);
        _tabulationScreenNumbersChangedEvent.Subscribe(TabulationScreenNumbersChanged);
    }

    private void TabulationScreenNumbersChanged(in Entity entity)
    {
        PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].death.text = entity.Read<PlayerScore>().Deaths.ToString();
        PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].kill.text = entity.Read<PlayerScore>().Kills.ToString();
    }
    private void TabulationScreenAddPlayer(in Entity entity)
    {
        PlayerInTab[entity.Read<PlayerTag>().PlayerLocalID].nickname.text = entity.Read<PlayerTag>().Nickname;
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
        public GameObject playerBlock;
        public TextMeshProUGUI nickname;
        public TextMeshProUGUI kill;
        public TextMeshProUGUI death;
    }
    
    private void OnDestroy() 
    {
        _tabulationScreenOnEvent.Unsubscribe(TabulationScreenON);
        _tabulationScreenOffEvent.Unsubscribe(TabulationScreenOFF);
        _tabulationScreenAddPlayerEvent.Unsubscribe(TabulationScreenAddPlayer);
        _tabulationScreenNumbersChangedEvent.Unsubscribe(TabulationScreenNumbersChanged);
    }
}
