using ME.ECS;
using Project.Core.Features.Player;
using Project.Modules.Network;
using TMPro;
using UnityEngine;

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
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(NetworkData.PlayerIdInRoom)) return;
            _victoryScreen.SetActive(true);
        }

        private void ToggleLoseScreen(in Entity entity)
        {
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(NetworkData.PlayerIdInRoom)) return;
            _defeatScreen.SetActive(true);
        }
    
        private void ToggleDrawScreen(in Entity entity)
        {
            if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(NetworkData.PlayerIdInRoom)) return;
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
            //ShowStats();
            //SetTexts();
        }

        private void SetTexts()
        {
            for (int i = 0; i < _strings.Length; i++)
            {
                Lines[i].SetText(_strings[i]);
            };
        }

        public TextMeshProUGUI[] Lines;
        private string[] _strings = new string[12];
        //private void ShowStats()
        //{
        //    _strings[0] = PhotonNetwork.NetworkingClient.CurrentCluster;
        //    _strings[1] = PhotonNetwork.NetworkingClient.CurrentLobby.ToString();
        //    _strings[2] = PhotonNetwork.NetworkingClient.CurrentRoom.ToString();
        //    _strings[3] = PhotonNetwork.NetworkingClient.Server.ToString();
        //    _strings[4] = PhotonNetwork.NetworkingClient.DisconnectedCause.ToString();
        //    _strings[5] = PhotonNetwork.NetworkingClient.IsConnected.ToString();
        //    _strings[6] = PhotonNetwork.NetworkingClient.RegionHandler.ToString();
        //    _strings[7] = PhotonNetwork.NetworkingClient.GameServerAddress;
        //    _strings[8] = PhotonNetwork.NetworkingClient.MatchMakingCallbackTargets.ToString();
        //    _strings[9] = PhotonNetwork.NetworkingClient.PlayersInRoomsCount.ToString();
        //    _strings[10] = PhotonNetwork.NetworkingClient.PlayersOnMasterCount.ToString();
        //    _strings[11] = PhotonNetwork.NetworkingClient.SummaryToCache;
        //}
    }
}


// _strings[0] = PhotonNetwork.CurrentCluster; //NetworkingClient.CurrentCluster;
// _strings[1] = PhotonNetwork.CurrentLobby.ToString(); //NetworkingClient.CurrentLobby.ToString();
// _strings[2] = PhotonNetwork.CurrentRoom.ToString(); //NetworkingClient.CurrentRoom.ToString();
// _strings[3] = PhotonNetwork.Server.ToString(); //NetworkingClient.Server.ToString();
// // _strings[4] = PhotonNetwork //NetworkingClient.DisconnectedCause.ToString();
// _strings[5] = PhotonNetwork.IsConnected.ToString(); //NetworkingClient.IsConnected.ToString();
// _strings[6] = PhotonNetwork.CloudRegion; //NetworkingClient.RegionHandler.ToString();
// _strings[7] = PhotonNetwork.ServerAddress; //NetworkingClient.GameServerAddress;
// // _strings[8] = PhotonNetwork //NetworkingClient.MatchMakingCallbackTargets.ToString();
// _strings[9] = PhotonNetwork.CountOfPlayers.ToString(); //NetworkingClient.PlayersInRoomsCount.ToString();
// _strings[10] = PhotonNetwork.CountOfPlayersOnMaster.ToString(); //NetworkingClient.PlayersOnMasterCount.ToString();
// // _strings[11] = PhotonNetwork //NetworkingClient.SummaryToCache;
