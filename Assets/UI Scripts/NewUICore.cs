using ME.ECS;
using Photon.Pun;
using Project.Core.Features.Player;
using UnityEngine;
using TMPro;

namespace UI_Scripts
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
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(PhotonNetwork.LocalPlayer.ActorNumber)) return;
            _victoryScreen.SetActive(true);
        }

        private void ToggleLoseScreen(in Entity entity)
        {
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(PhotonNetwork.LocalPlayer.ActorNumber)) return;
            _defeatScreen.SetActive(true);
        }
    
        private void ToggleDrawScreen(in Entity entity)
        {
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(PhotonNetwork.LocalPlayer.ActorNumber)) return;
            _drawScreen.SetActive(true);
        }

        private void OnDestroy() 
        {
            _victoryScreenEvent.Unsubscribe(ToggleWinScreen);
            _defeatScreenEvent.Unsubscribe(ToggleLoseScreen);
            _drawScreenEvent.Subscribe(ToggleDrawScreen);
        }

        private void Update()
        {
            // ShowStats();
        }

        public TextMeshProUGUI ServerAdress;
        public TextMeshProUGUI CurrentCluster;
        public TextMeshProUGUI CloudRegion;
        public TextMeshProUGUI RoomName;
        public TextMeshProUGUI MaxPlayers;
        public TextMeshProUGUI ActorNumber;

        private void ShowStats()
        {
            ServerAdress.SetText("Server Adress: " + PhotonNetwork.ServerAddress);
            CurrentCluster.SetText("Current Cluster: " + PhotonNetwork.CurrentCluster);
            CloudRegion.SetText("Cloud Region: " + PhotonNetwork.CloudRegion);
            RoomName.SetText("Room Name: " + PhotonNetwork.CurrentRoom.Name);
            MaxPlayers.SetText("Max Players: " + PhotonNetwork.CurrentRoom.MaxPlayers);
            ActorNumber.SetText("Actor Number: " + PhotonNetwork.LocalPlayer.ActorNumber);
        }
    }
}
