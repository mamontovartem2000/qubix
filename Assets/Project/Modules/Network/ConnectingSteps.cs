using FlatBuffers;
using FlatMessages;
using System;
using System.Runtime.InteropServices;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Modules.Network
{
    public class ConnectingSteps  : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void ReadyToStart();

        [SerializeField] private TMP_Text _playersList;
        [SerializeField] private InputField _playerId;
        [SerializeField] private InputField _roomId;
        [SerializeField] private Toggle _needCreateRoom;

        private bool _needLoadGameScene; 

        private void Start()
        {
            //ReadyToStart();           
        }

        private void Update()
        {
            if (_needLoadGameScene)
                SceneManager.LoadScene(1);
        }

        // Browser method
        public void ProcessJoinRequest(string request)
        {
            string payloadBase64 = ParceUtils.CreateFromJSON<JoinRequestData>(request).payload;
            var payloadInBytes = Convert.FromBase64String(payloadBase64);
            var playerJson = Encoding.UTF8.GetString(payloadInBytes);
            GameInfo info = ParceUtils.CreateFromJSON<GameInfo>(playerJson);
            NetworkData.Info = info;

            Connect();
            SendJoinRequest(request);
        }

        public void StartManual()
        {
            if (_playerId.text == "")
            {
                Debug.Log("Enter id");
                return;
            }

            NetworkData.PlayerIdInRoom = Int32.Parse(_playerId.text);

            if (_needCreateRoom.isOn)
                StartCoroutine(ManualRoomCreating.CreateRoom(GetManualJoinRequest));
            else
                GetManualJoinRequest(_roomId.text);
        }

        private void GetManualJoinRequest(string roomId)
        {
            _roomId.text = roomId;
            StartCoroutine(ManualRoomCreating.LoadJoinRequest(roomId, ProcessJoinRequest));
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

                    //TODO: Handle Shutdown
            }
        }
    
        private void GetJoinResult(JoinResult joinResult)
        {
            if (joinResult.Value)
            {
                Debug.Log("Join");
            }
            else
            {
                //TODO: Close and clear connect
                Debug.Log($"Join Error: {joinResult.Reason}");
            }
        }

        private void SetTimeRemaining(TimeRemaining timeRemaining)
        {
            Debug.Log("Set Time!");
            NetTimer.Timer.SetTime(timeRemaining.Value / 1000);
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
        }

        private void SetStartGame(Start start)
        {
            Debug.Log("Start");
            NetworkData.Connect.GetMessage -= GetMessage;
            _needLoadGameScene = true;
        }

        private void SendJoinRequest(string fullJoinRequest)
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1);
            var mes = builder.CreateString(fullJoinRequest);
            var request = JoinRequest.CreateJoinRequest(builder, mes);
            var offset = SystemMessage.CreateSystemMessage(builder, (uint)DateTime.Now.Ticks, Payload.JoinRequest, request.Value); //TODO: maybe Utc; Ticks or miliseconds? Check all messages. Maybe delete time in this struñt.
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
            var offset = SystemMessage.CreateSystemMessage(builder, (uint)DateTime.Now.Ticks, Payload.SetCharacter, request.Value);
            builder.Finish(offset.Value);

            var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
            NetworkData.Connect.SendSystemMessage(ms);
        }
    }
}
