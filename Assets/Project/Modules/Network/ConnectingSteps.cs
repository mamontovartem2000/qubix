using FlatBuffers;
using SerializedMessages;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Modules.Network
{
    public class ConnectingSteps  : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playersList;
        [SerializeField] private TextMeshProUGUI _roomId;

        private bool master = false;

        private bool da = false; 

        private void Start()
        {
            if (master)
                StartGame1();
            else
                aaaaa("otMfqkxjSRiCpPzOzj5Tzg/2");

            //Connect();
            //SendJoinRequest();
        }

        private void Update()
        {
            if (da)
                SceneManager.LoadScene(1);

        }

        private void StartGame()
        {
            BrowserRequest.LoadBrowserInfo(out string request, out GameInfo info);
            NetworkData.Info = info;
            NetworkData.FullJoinRequest = request;
        }

        private void StartGame1()
        {
            StartCoroutine(BrowserRequest.Loadroom(aaaaa));
        }

        private void aaaaa(string aaa)
        {
            _roomId.text = aaa;
            StartCoroutine(BrowserRequest.Loadreq(aaa, bbbbb));
        }

        private void bbbbb(string aaa)
        {
            BrowserRequest.LoadBrowserInfo22(aaa, out GameInfo info);
            NetworkData.Info = info;
            NetworkData.FullJoinRequest = aaa;
            Connect();
            SendJoinRequest();
        }

        private void Connect() //TODO: async connect?
        {
            NetworkData.Connect = new WebSocketConnect(NetworkData.Info.server_url);
            NetworkData.Connect.GetMessage += GetMessage;
        }

       

        private void GetMessage(byte[] bytes)
        {
            byte[] buffer = new byte[bytes.Length - 1];
            Array.Copy(bytes, 1, buffer, 0, buffer.Length);

            SystemMessage data = SystemMessage.GetRootAsSystemMessage(new ByteBuffer(buffer));

            switch (data.PayloadType)
            {
                case Payload.NONE:
                    Debug.Log("Payload type NONE!");
                    break;
                case Payload.JoinResult:
                    GetJoinResult(data.PayloadAsJoinResult());
                    break;
                case Payload.Start:
                    SetStartGame(data.PayloadAsStart());
                    break;
                case Payload.PlayerList:
                    SetPlayerList(data.PayloadAsPlayerList());
                    break;
                case Payload.TimeRemaining:
                    SetTimeRemaining(data.PayloadAsTimeRemaining());
                    break;
                default:
                    Debug.Log("Unknown system message!");
                    break;
            }
        }

        

        private void GetJoinResult(JoinResult joinResult)
        {
            if (joinResult.Value)
            {
                Debug.Log("Join");
                //Invoke(nameof(SetCharacterRequest), 3f);
            }
            else
            {
                //TODO: Очистить коннект
                Debug.Log($"Join Error: {joinResult.Reason}");
            }
        }

        private void SetTimeRemaining(TimeRemaining timeRemaining)
        {
            Debug.Log("Set Time!");
            NetTimer.Timer.SetTime(timeRemaining.Value);
        }

        private void SetPlayerList(PlayerList playerList)
        {
            Debug.Log("Set Players!");
            string info = "Players:\n";

            for (int i = 0; i < playerList.PlayersLength; i++)
            {
                var player = playerList.Players(i).Value;
                info += $"Id: {player.Id}, Character: {player.Character}\n";
            }

            _playersList.SetText(info);
            //_playersList.text = info;
        }

        private void SetStartGame(Start start)
        {
            Debug.Log("Start");
            NetworkData.Connect.GetMessage -= GetMessage;
            da = true;
        }

        private void SendJoinRequest()
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1);
            var mes = builder.CreateString(NetworkData.FullJoinRequest);
            var request = JoinRequest.CreateJoinRequest(builder, mes);
            var offset = SystemMessage.CreateSystemMessage(builder, (uint)DateTime.Now.Ticks, Payload.JoinRequest, request.Value); //TODO: Мб Utc; Точно тики?
            builder.Finish(offset.Value);

            var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(ms);
        }

        public void SetCharacterRequest()
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1);
            var id = builder.CreateString(NetworkData.Info.player_id);
            var character = builder.CreateString(NetworkData.Info.available_characters[0]);
            var request = SetCharacter.CreateSetCharacter(builder, id, character);
            var offset = SystemMessage.CreateSystemMessage(builder, (uint)DateTime.Now.Ticks, Payload.SetCharacter, request.Value); //TODO: Мб Utc; Точно тики?
            builder.Finish(offset.Value);

            var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(ms);
        }
    }
}
