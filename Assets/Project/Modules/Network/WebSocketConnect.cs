using System.Linq;
using UnityEngine.Events;
using WebSocketSharp;

public class WebSocketConnect
{
    public UnityAction<byte[]> GetMessage;
    private WebSocket _socket;

    public WebSocketConnect(string url)
    {
        _socket = new WebSocket(url);
        _socket.Connect();
        _socket.OnMessage += (sender, e) => GetMessage.Invoke(e.RawData);
    }

    public void SendMessage(byte[] message)
    {
        try
        {
            byte[] type = new byte[1] { 0 };
            byte[] result = type.Concat(message).ToArray();
            _socket.Send(result);
        }
        catch { }
    }

    public void SendSystemMessage(byte[] message)
    {
        try
        {
            byte[] type = new byte[1] { 1 };
            byte[] result = type.Concat(message).ToArray();
            _socket.Send(result);
        }
        catch { }
    }

    public void CloseClient()
    {
        if (_socket != null)
        {
            _socket.Close();
        }
    }

    ~WebSocketConnect()
    {
        CloseClient();
    }
}
