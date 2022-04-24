// using System;
// using System.Text;
// using FlatBuffers;
// using FlatMessages;
// using UnityEngine;
//
// // Use plugin namespace
// using HybridWebSocket;
//
// public class WebSocketDemo : MonoBehaviour {
//
// 	// Use this for initialization
// 	void Start () {
//
//         // Create WebSocket instance
//         WebSocket ws = WebSocketFactory.CreateInstance("ws://35.158.134.83:80/match");
//
//         // Add OnOpen event listenerx
//         ws.OnOpen += () =>
//         {
//             Debug.Log("WS connected!");
//             Debug.Log("WS state: " + ws.GetState().ToString());
//
//             ws.Send(Encoding.UTF8.GetBytes("Hello from Unity 3D!"));
//             
//                 // private void SendJoinRequest(string fullJoinRequest)
//                 // {
//                 //     FlatBufferBuilder builder = new FlatBufferBuilder(1);
//                 //     var mes = builder.CreateString(fullJoinRequest);
//                 //     var request = JoinRequest.CreateJoinRequest(builder, mes);
//                 //     var offset = SystemMessage.CreateSystemMessage(builder, (uint)DateTime.Now.Ticks, Payload.JoinRequest, request.Value); //TODO: maybe Utc; Ticks or miliseconds? Check all messages. Maybe delete time in this stru�t.
//                 //     builder.Finish(offset.Value);
//                 //
//                 //     var ms = builder.DataBuffer.ToArray(builder.DataBuffer.Position, builder.Offset);
//                 //     NetworkData.Connect.SendSystemMessage(ms);
//                 // }
//         };
//
//         // Add OnMessage event listener
//         ws.OnMessage += (byte[] msg) =>
//         {
//             // private void GetMessage(byte[] bytes)
//             // {
//                 byte[] buffer = new byte[msg.Length - 1];
//                 Array.Copy(msg, 1, buffer, 0, buffer.Length);
//
//                 SystemMessage data = SystemMessage.GetRootAsSystemMessage(new ByteBuffer(buffer));
//
//                 switch (data.PayloadType)
//                 {
//                     case Payload.NONE:
//                         Debug.Log("Payload type NONE!");
//                         break;
//                     case Payload.JoinResult:
//                         Debug.Log("Payload type JoinResult!");
//                         // GetJoinResult(data.PayloadAsJoinResult());
//                         break;
//                     case Payload.Start:
//                         Debug.Log("Payload type Start!");
//                         // SetStartGame(data.PayloadAsStart());
//                         break;
//                     case Payload.PlayerList:
//                         Debug.Log("Payload type PlayerList!");
//                         //SetPlayerList(data.PayloadAsPlayerList());
//                         break;
//                     case Payload.TimeRemaining:
//                         Debug.Log("Payload type TimeRemaining!");
//                         // SetTimeRemaining(data.PayloadAsTimeRemaining());
//                         break;
//                     default:
//                         Debug.Log("Unknown system message!");
//                         break;
//
//                     //TODO: Handle Shutdown
//                 }
//             // }
//             // Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));
//             //
//             // ws.Close();
//         };
//
//         // Add OnError event listener
//         ws.OnError += (string errMsg) =>
//         {
//             Debug.Log("WS error: " + errMsg);
//         };
//
//         // Add OnClose event listener
//         ws.OnClose += (WebSocketCloseCode code) =>
//         {
//             Debug.Log("WS closed with code: " + code.ToString());
//         };
//
//         // Connect to the server
//         ws.Connect();
//
//     }
// 	
// 	// Update is called once per frame
// 	void Update () {
// 		
// 	}
// }
