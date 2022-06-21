using ME.ECS;
using Project.Features.Player;
using Project.Modules.Network;
using UnityEngine;

namespace UI_Scripts
{
    public class NewUICore : MonoBehaviour
    {
        [SerializeField] private GlobalEvent _victoryScreenEvent;
        [SerializeField] private GlobalEvent _defeatScreenEvent;

        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _defeatScreen;

        private void Start() 
        {
            _victoryScreenEvent.Subscribe(ToggleWinScreen);
            _defeatScreenEvent.Subscribe(ToggleLoseScreen);
        }

        private void ToggleWinScreen(in Entity entity)
        {
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
            _victoryScreen.SetActive(true);
        }

        private void ToggleLoseScreen(in Entity entity)
        {
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
            _defeatScreen.SetActive(true);
        }
    
        private void OnDestroy() 
        {
            _victoryScreenEvent.Unsubscribe(ToggleWinScreen);
            _defeatScreenEvent.Unsubscribe(ToggleLoseScreen);
        }
    }
}