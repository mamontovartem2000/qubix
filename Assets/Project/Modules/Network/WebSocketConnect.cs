using System.Linq;
using NativeWebSocket;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.Events;

public class WebSocketConnect
{
    public UnityAction<byte[]> GetMessage;
    public UnityAction StartJoining;
    public WebSocket Socket { get; private set; }

    public WebSocketConnect()
    {
        CreateConnect();
    }

    private async void CreateConnect()
    {
        Socket = new WebSocket(NetworkData.Info.server_url);
        Socket.OnMessage += (e) => GetMessage?.Invoke(e);
        Socket.OnOpen += () => StartJoining?.Invoke();

        Socket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        Socket.OnClose += (e) =>
        {
            //TODO: можно проверять по типук
            Debug.Log("Close! " + e);
            CloseClient();
            CreateConnect();
        };

        await Socket.Connect();
    }

    public void SendMessage(byte[] message)
    {
        try
        {
            byte[] type = new byte[1] { 0 };
            byte[] result = type.Concat(message).ToArray();
            Socket.Send(result);
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
            byte[] type = new byte[1] { 1 };
            byte[] result = type.Concat(message).ToArray();
            Socket.Send(result);
        }
        catch
        {
            Debug.Log("SendSystem error");
        }
    }

    public async void CloseClient()
    {
        if (Socket != null)
        {
            await Socket.Close();
        }
    }

    ~WebSocketConnect()
    {
        CloseClient();
    }
}