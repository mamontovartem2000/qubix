// using ME.ECS;
// using Photon.Pun;
// using UnityEngine;
//
// namespace Project.Features.InputHandler.Modules
// {
//     #region usage
//     using Components;
//     using Modules;
//     using Systems;
//     using Features;
//     using Markers;
// #if ECS_COMPILE_IL2CPP_OPTIONS
//     [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
// #endif
//     #endregion
//     public sealed class HandlePlayerInput : IModule, IUpdate
//     {
//         private InputHandlerFeature feature;
//         public World world { get; set; }
//
//         void IModuleBase.OnConstruct()
//         {
//             this.feature = this.world.GetFeature<InputHandlerFeature>();
//         }
//
//         void IModuleBase.OnDeconstruct() {}
//
//         void IUpdate.Update(in float deltaTime)
//         {
//             if (Input.GetKeyDown(KeyCode.W))
//             {
//                 if (Input.GetKey(KeyCode.S))
//                 {
//                     world.AddMarker(new KeyReleasedMarker
//                         {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Backward});
//                 }
//                 else
//                 {
//                     world.AddMarker(new KeyPressedMarker
//                         {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Forward});
//                 }
//             }
//             
//             if (Input.GetKeyDown(KeyCode.S))
//             {
//                 if (Input.GetKey(KeyCode.W))
//                 {
//                     world.AddMarker(new KeyReleasedMarker
//                         {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Forward});
//                 }
//                 else
//                 {
//                     world.AddMarker(new KeyPressedMarker
//                         {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Backward});
//                 }
//             }
//             
//             if (Input.GetKeyUp(KeyCode.W))
//             {
//                 if (Input.GetKey(KeyCode.S))
//                 {
//                     world.AddMarker(new KeyPressedMarker()
//                         {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Backward});
//                 }
//                 else
//                 {
//                     world.AddMarker(new KeyReleasedMarker
//                         {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Forward});
//                 }
//             }
//             
//             if (Input.GetKeyUp(KeyCode.S))
//             {
//                 if (Input.GetKey(KeyCode.W))
//                 {
//                     world.AddMarker(new KeyPressedMarker
//                         {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Forward});
//                 }
//                 else
//                 {
//                     world.AddMarker(new KeyReleasedMarker
//                         {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Backward});
//                 }
//             }
//
//             if (Input.GetKeyDown(KeyCode.A))
//             {
//                 world.AddMarker(new KeyPressedMarker
//                     {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Left});
//             }
//
//             if (Input.GetKeyDown(KeyCode.D))
//             {
//                 world.AddMarker(new KeyPressedMarker
//                     {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Right});
//             }
//
//             if (Input.GetMouseButtonDown(0))
//             {
//                 world.AddMarker(new ButtonPressedMarker
//                     {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Button = Button.Left});
//                 // Debug.Log("Left Pressed");
//             }
//             
//             if (Input.GetMouseButtonDown(1))
//             {
//                 world.AddMarker(new ButtonPressedMarker
//                     {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Button = Button.Right});
//                 // Debug.Log("Right Pressed");
//
//             }
//
//             if (Input.GetMouseButtonUp(0))
//             {
//                 world.AddMarker(new ButtonReleasedMarker
//                     {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Button = Button.Left});
//                 // Debug.Log("Left Released");
//
//             }
//
//             if (Input.GetMouseButtonUp(1))
//             {
//                 world.AddMarker(new ButtonReleasedMarker
//                     {ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Button = Button.Right});
//                 // Debug.Log("Right Released");
//             }
//
//         }
//     }
// }