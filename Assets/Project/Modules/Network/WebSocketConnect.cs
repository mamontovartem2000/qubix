using NativeWebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Modules.Network
{
    public class WebSocketConnect
    {
        public Action ConnectSuccessful;
        public Action<string> ConnectError;
        public Action<string> ConnectWarning;
        public Action<byte[]> OnMessage;

        private WebSocket _socket;
        private string _serverUrl;
        private bool _connection;

        public WebSocketConnect(string url)
        {
            _serverUrl = url;
            CreateNewConnection();
        }

        public WebSocketConnect()
        {
            _serverUrl = NetworkData.Info.server_url;
            CreateNewConnection();
        }

        private async void CreateNewConnection()
        {
            _connection = true;

            while (_connection)
            {
                if (_socket == null || _socket.State == WebSocketState.Closed)
                {
                    await Task.Run(() => CloseSocket());
                    await Task.Run(() => ConnectWebSocket());
                }
                else if (_socket != null && _socket.State == WebSocketState.Open)
                {
                    NetworkData.Connected = true;
                    _socket.OnMessage += (e) => OnMessage?.Invoke(e);
                    ConnectSuccessful?.Invoke();
                    return;
                }

                await Task.Delay(500);
                Debug.Log("Check");
            }
        }

        private void ConnectWebSocket()
        {
            try
            {
                _socket = new WebSocket(_serverUrl);
            }
            catch (Exception exception)
            {
                // Invalid URI: The format of the URI could not be determined.
                Debug.Log("Create socket exception: " + exception);
                _connection = false;
                ConnectError?.Invoke(exception.Message);
                //TODO: Нормальная обработка error
                return;
            }
           
            _socket.OnError += (e) =>
            {
                // If there is no Internet connection: "Unable to connect to the remote server"
                Debug.Log("Socket error! " + e);
                ConnectWarning?.Invoke(e);
                //TODO: Отслеживать на уровне Ui. Принимать эти ивенты и выводить сообщение об отсутсвии инета.
            };

            _socket.OnClose += (e) =>
            {
                NetworkData.Connected = false;
                Debug.Log("Close socket! " + e);
            };

            _socket.Connect();
        }

        public void SendMessage(byte[] message)
        {
            try
            {
                byte[] type = new byte[1] { 0 };
                byte[] result = type.Concat(message).ToArray();
                _socket.Send(result);
            }
            catch { Debug.Log("Send error"); }
        }

        public void SendSystemMessage(byte[] message)
        {
            try
            {
                byte[] type = new byte[1] { 1 };
                byte[] result = type.Concat(message).ToArray();
                _socket.Send(result);
            }
            catch { Debug.Log("SendSystem error"); }
        }

        public void DispatchWebSocketMessageQueue()
        {
            if (_socket.State == WebSocketState.Open)
                _socket.DispatchMessageQueue();
        }

        public async void CloseSocket()
        {
            if (_socket != null)
            {
                await _socket.Close();
                _socket = null;
            }
        }

        ~WebSocketConnect()
        {
            CloseSocket();
        }
    }
}