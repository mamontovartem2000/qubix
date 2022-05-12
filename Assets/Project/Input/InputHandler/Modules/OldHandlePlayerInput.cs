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
//             _input.Player.MoveForward.started += ctx => world.AddMarker(new ForwardMarker
//                 {State = InputState.Pressed, Axis = MovementAxis.Vertical});
//
//             _input.Player.MoveForward.canceled += ctx => world.AddMarker(new ForwardMarker
//             {
//                 State = InputState.Released,
//                 Axis = _input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed() ? MovementAxis.Horizontal : MovementAxis.Vertical
//             });
//
//             _input.Player.MoveBackward.started += ctx => world.AddMarker(new BackwardMarker
//                 {State = InputState.Pressed, Axis = MovementAxis.Vertical});
//
//             _input.Player.MoveBackward.canceled += ctx => world.AddMarker(new BackwardMarker
//             {
//                 State = InputState.Released,
//                 Axis = _input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed() ? MovementAxis.Horizontal : MovementAxis.Vertical
//             });
//             ;
//
//             _input.Player.MoveLeft.started += ctx => world.AddMarker(new LeftMarker
//                 {State = InputState.Pressed, Axis = MovementAxis.Horizontal});
//
//             _input.Player.MoveLeft.canceled += ctx => world.AddMarker(new LeftMarker
//             {
//                 State = InputState.Released,
//                 Axis = _input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed()
//                     ? MovementAxis.Vertical
//                     : MovementAxis.Horizontal
//             });
//
//             _input.Player.MoveRight.started += ctx => world.AddMarker(new RightMarker
//                 {State = InputState.Pressed, Axis = MovementAxis.Horizontal});
//
//             _input.Player.MoveRight.canceled += ctx => world.AddMarker(new RightMarker
//             {
//                 State = InputState.Released,
//                 Axis = _input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed()
//                     ? MovementAxis.Vertical
//                     : MovementAxis.Horizontal
//             });
//             
//             _input.Player.LeftShoot.started += ctx => world.AddMarker(new MouseLeftMarker {State = InputState.Pressed});
//             _input.Player.LeftShoot.canceled += ctx => world.AddMarker(new MouseLeftMarker {State = InputState.Released});
//             _input.Player.RightShoot.started +=ctx => world.AddMarker(new MouseRightMarker {State = InputState.Pressed});
//             _input.Player.RightShoot.canceled += ctx => world.AddMarker(new MouseRightMarker {State = InputState.Released});
//             _input.Player.LockDirection.started += ctx => world.AddMarker(new LockDirectionMarker {State = InputState.Pressed});
//             _input.Player.LockDirection.canceled += ctx => world.AddMarker(new LockDirectionMarker {State = InputState.Released});
//
//             _input.Player.Skill1.performed += ctx => world.AddMarker(new FirstSkillMarker {ActorID = NetworkData.PlayerIdInRoom});
//             _input.Player.Skill2.performed += ctx => world.AddMarker(new SecondSkillMarker {ActorID = NetworkData.PlayerIdInRoom});
//             _input.Player.Skill3.performed += ctx => world.AddMarker(new ThirdSkillMarker {ActorID = NetworkData.PlayerIdInRoom});
//             _input.Player.Skill4.performed += ctx => world.AddMarker(new FourthSkillMarker {ActorID = NetworkData.PlayerIdInRoom});
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