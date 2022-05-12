// using ME.ECS;
// using Project.Common.Components;
// using Project.Input.InputHandler.Markers;
// using Project.Modules.Network;
// using UnityEngine;
//
// namespace Project.Input.InputHandler.Modules
// {
//     #region usage
//
// #if ECS_COMPILE_IL2CPP_OPTIONS
//     [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
// #endif
//
//     #endregion
//
//     public sealed class HandlePlayerInput : IModule, IUpdate
//     {
//         private PlayerInput _input;
//         private InputHandlerFeature _feature;
//
//         public World world { get; set; }
//
//         void IModuleBase.OnConstruct()
//         {
//             _feature = world.GetFeature<InputHandlerFeature>();
//
//             _input = new PlayerInput();
//             _input.Enable();
//
//             _input.Player.MoveForward.started += ctx => ForwardPressed();
//             _input.Player.MoveForward.canceled += ctx => ForwardReleased();
//             _input.Player.MoveBackward.started += ctx => BackwardPressed();
//             _input.Player.MoveBackward.canceled += ctx => BackwardReleased();
//             _input.Player.MoveLeft.started += ctx => LeftPressed();
//             _input.Player.MoveLeft.canceled += ctx => LeftReleased();
//             _input.Player.MoveRight.started += ctx => RightPressed();
//             _input.Player.MoveRight.canceled += ctx => RightReleased();
//             _input.Player.LeftShoot.started += ctx => world.AddMarker(new MouseLeftMarker {State = InputState.Pressed});
//             _input.Player.LeftShoot.canceled += ctx => world.AddMarker(new MouseLeftMarker {State = InputState.Released});
//             _input.Player.RightShoot.started += ctx => world.AddMarker(new MouseRightMarker {State = InputState.Pressed});
//             _input.Player.RightShoot.canceled += ctx => world.AddMarker(new MouseRightMarker {State = InputState.Released});
//             _input.Player.LockDirection.started += ctx => world.AddMarker(new LockDirectionMarker {State = InputState.Pressed});
//             _input.Player.LockDirection.canceled += ctx => world.AddMarker(new LockDirectionMarker {State = InputState.Released});
//             _input.Player.Skill1.performed += ctx => world.AddMarker(new FirstSkillMarker {ActorID = NetworkData.PlayerIdInRoom});
//             _input.Player.Skill2.performed += ctx => world.AddMarker(new SecondSkillMarker {ActorID = NetworkData.PlayerIdInRoom});
//             _input.Player.Skill3.performed += ctx => world.AddMarker(new ThirdSkillMarker {ActorID = NetworkData.PlayerIdInRoom});
//             _input.Player.Skill4.performed += ctx => world.AddMarker(new FourthSkillMarker {ActorID = NetworkData.PlayerIdInRoom});
//         }
//
//         private void ForwardPressed()
//         {
//             if (_input.Player.MoveBackward.IsPressed())
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.zero});
//             }
//             else
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.right});
//             }
//         }
//
//         private void ForwardReleased()
//         {
//             if (_input.Player.MoveBackward.IsPressed())
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.left});
//             }
//             else if(_input.Player.MoveLeft.IsPressed())
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.forward});
//             }
//             else if (_input.Player.MoveRight.IsPressed())
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.back});
//             }
//             else
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.zero});
//             }
//         }
//
//         private void BackwardPressed()
//         {
//             if (_input.Player.MoveForward.IsPressed())
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.zero});
//             }
//             else
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.left});
//             }
//         }
//
//         private void BackwardReleased()
//         {
//             if (_input.Player.MoveForward.IsPressed())
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.right});
//             }
//             else if(_input.Player.MoveLeft.IsPressed())
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.forward});
//             }
//             else if (_input.Player.MoveRight.IsPressed())
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.back});
//             }
//             else
//             {
//                 world.AddMarker(new MovementMarker {Direction = Vector3.zero});
//             }
//         }
//
//         private void LeftPressed()
//         {
//             
//         }
//
//         private void LeftReleased()
//         {
//             
//         }
//
//         private void RightPressed()
//         {
//            
//         }
//
//         private void RightReleased()
//         {
//             
//         }
//
//         void IModuleBase.OnDeconstruct()
//         {
//             _input.Dispose();
//         }
//
//         void IUpdate.Update(in float deltaTime)
//         {
//         }
//     }
// }