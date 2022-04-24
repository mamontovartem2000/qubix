using System.Linq;
using NativeWebSocket;
using UnityEngine;
using UnityEngine.Events;

public class WebSocketConnect
{
    public UnityAction<byte[]> GetMessage;
    public WebSocket Socket;

    public WebSocketConnect()
    {
        CreateConnect();
    }

    private async void CreateConnect()
    {
        Socket = new WebSocket("ws://35.158.134.83:80/match");
        Socket.OnMessage += (e) => GetMessage.Invoke(e);


        Socket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        await Socket.Connect();
    }

    public void SendMessage(byte[] message)
    {
        byte[] type = new byte[1] { 0 };
        byte[] result = type.Concat(message).ToArray();
        Socket.Send(result);
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
            Debug.Log("yeet");
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