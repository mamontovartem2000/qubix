using NativeWebSocket;
using System;
using System.Linq;
using UnityEngine;

namespace Project.Modules.Network
{
    public class WebSocketConnect
    {
        public Action ConnectSuccessful;
        public Action<string> ConnectError;
        public Action<byte[]> OnMessage;

        private WebSocket _socket;

        public WebSocketConnect(string url)
        {
            CreateConnect(url);
        }

        private async void CreateConnect(string url)
        {
            try
            {
                _socket = new WebSocket(url);
            }
            catch (Exception exception)
            {
                // Invalid URI: The format of the URI could not be determined.
                Debug.Log("Create " + exception);

                ConnectError?.Invoke(exception.Message);
                return;
            }

            _socket.OnOpen += () => ConnectSuccessful?.Invoke();
            _socket.OnMessage += (e) => OnMessage?.Invoke(e);

            _socket.OnError += (e) =>
            {
                // If there is no Internet connection:
                // Unable to connect to the remote server
                Debug.LogError("Error! " + e);
                ConnectError?.Invoke(e);
            };

            _socket.OnClose += (e) =>
            {
                Debug.Log("Close! " + e);
            };

            await _socket.Connect();
        }

        public void SendMessage(byte[] message)
        {
            try
            {
                var type = new byte[1] { 0 };
                var result = type.Concat(message).ToArray();
                _socket.Send(result);
            }
            catch
            {
                Debug.Log("Send error");
            }
        }

        public void SendSystemMessage(byte[] message)
        {
            try
            {
                var type = new byte[1] { 1 };
                var result = type.Concat(message).ToArray();
                _socket.Send(result);
            }
            catch
            {
                Debug.Log("SendSystem error");
            }
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        public void DispatchWebSocketMessageQueue()
        {
            if (_socket.State == WebSocketState.Open)
                _socket.DispatchMessageQueue();
        }
#endif

        public async void CloseClient()
        {
            if (_socket != null)
            {
                await _socket.Close();
            }
        }

        ~WebSocketConnect()
        {
            CloseClient();
        }
    }
}