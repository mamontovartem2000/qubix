using ME.ECS;
using Project.Common.Components;
using Project.Input.InputHandler.Markers;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Input.InputHandler.Modules
{
    #region usage

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

    public sealed class HandlePlayerInput : IModule, IUpdate
    {
        private PlayerInput _input;
        private InputHandlerFeature _feature;

        public World world { get; set; }

        void IModuleBase.OnConstruct()
        {
            _feature = world.GetFeature<InputHandlerFeature>();

            _input = new PlayerInput();
            _input.Enable();

            _input.Player.MoveForward.started += ctx => ForwardPressed();
            _input.Player.MoveForward.canceled += ctx => ForwardReleased();
            _input.Player.MoveBackward.started += ctx => BackwardPressed();
            _input.Player.MoveBackward.canceled += ctx => BackwardReleased();

            _input.Player.MoveLeft.started += ctx => LeftPressed();
            _input.Player.MoveLeft.canceled += ctx => LeftReleased();
            _input.Player.MoveRight.started += ctx => RightPressed();
            _input.Player.MoveRight.canceled += ctx => RightReleased();
            
            _input.Player.LeftShoot.started += ctx => world.AddMarker(new MouseLeftMarker {State = InputState.Pressed});
            _input.Player.LeftShoot.canceled += ctx => world.AddMarker(new MouseLeftMarker {State = InputState.Released});
            _input.Player.RightShoot.started +=ctx => world.AddMarker(new MouseRightMarker {State = InputState.Pressed});
            _input.Player.RightShoot.canceled += ctx => world.AddMarker(new MouseRightMarker {State = InputState.Released});
            _input.Player.LockDirection.started += ctx => world.AddMarker(new LockDirectionMarker {State = InputState.Pressed});
            _input.Player.LockDirection.canceled += ctx => world.AddMarker(new LockDirectionMarker {State = InputState.Released});

            _input.Player.Skill1.performed += ctx => world.AddMarker(new FirstSkillMarker {ActorID = NetworkData.SlotInRoom});
            _input.Player.Skill2.performed += ctx => world.AddMarker(new SecondSkillMarker {ActorID = NetworkData.SlotInRoom});
            _input.Player.Skill3.performed += ctx => world.AddMarker(new ThirdSkillMarker {ActorID = NetworkData.SlotInRoom});
            _input.Player.Skill4.performed += ctx => world.AddMarker(new FourthSkillMarker {ActorID = NetworkData.SlotInRoom});
            _input.Player.Tabulation.performed += ctx => world.AddMarker(new TabulationMarker {State = InputState.Pressed});
            _input.Player.Tabulation.canceled += ctx => world.AddMarker(new TabulationMarker {State = InputState.Released});
            
            _input.Player.Screenshot.performed += ctx => Screenshot();
        }

        private void Screenshot()
        {
            string date = System.DateTime.Now.ToString();
            date = date.Replace("/","-");
            date = date.Replace(" ","_");
            date = date.Replace(":","-");
            ScreenCapture.CaptureScreenshot( date + ".png", 2);
        }

        private void ForwardPressed()
        {
            var direction = MovementAxis.Vertical;
            var value = 1;

            if (_input.Player.MoveBackward.IsPressed())
            {
                value = 0;

                var left = _input.Player.MoveLeft.IsPressed() ? -1 : 0;
                var right = _input.Player.MoveRight.IsPressed() ? 1 : 0;

                if (_input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed())
                {
                    direction = MovementAxis.Horizontal;
                    value = left + right;
                }
            }
            
            SendMarker(direction, value);
        }

        private void ForwardReleased()
        {
            var direction = MovementAxis.Vertical;
            var value = 0;

            var left = _input.Player.MoveLeft.IsPressed() ? -1 : 0;
            var right = _input.Player.MoveRight.IsPressed() ? 1 : 0;
            
            if (_input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed())
            {
                direction = MovementAxis.Horizontal;
                value = left + right;
            }

            if (_input.Player.MoveBackward.IsPressed())
            {
                direction = MovementAxis.Vertical;
                value = -1;
            }

            SendMarker(direction, value);
        }
        
        private void BackwardPressed()
        {
            var direction = MovementAxis.Vertical;
            var value = -1;

            if (_input.Player.MoveForward.IsPressed())
            {
                value = 0;
                
                var left = _input.Player.MoveLeft.IsPressed() ? -1 : 0;
                var right = _input.Player.MoveRight.IsPressed() ? 1 : 0;

                if (_input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed())
                {
                    direction = MovementAxis.Horizontal;
                    value = left + right;
                }
            }
            
            SendMarker(direction, value);
        }

        private void BackwardReleased()
        {
            var direction = MovementAxis.Vertical;
            var value = 0;

            var left = _input.Player.MoveLeft.IsPressed() ? -1 : 0;
            var right = _input.Player.MoveRight.IsPressed() ? 1 : 0;
            
            if (_input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed())
            {
                direction = MovementAxis.Horizontal;
                value = left + right;
            }

            if (_input.Player.MoveForward.IsPressed())
            {
                direction = MovementAxis.Vertical;
                value = 1;
            }

            SendMarker(direction, value);
        }
        
        private void LeftPressed()
        {
            var direction = MovementAxis.Horizontal;
            var value = -1;

            if (_input.Player.MoveRight.IsPressed())
            {
                value = 0;

                var back = _input.Player.MoveBackward.IsPressed() ? -1 : 0;
                var forth = _input.Player.MoveForward.IsPressed() ? 1 : 0;

                if (_input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed())
                {
                    direction = MovementAxis.Vertical;
                    value = back + forth;
                }
            }

            SendMarker(direction, value);
        }

        private void LeftReleased()
        {
            var direction = MovementAxis.Horizontal;
            var value = 0;

            var back = _input.Player.MoveBackward.IsPressed() ? -1 : 0;
            var forth = _input.Player.MoveForward.IsPressed() ? 1 : 0;
            
            if (_input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed())
            {
                direction = MovementAxis.Vertical;
                value = back + forth;
            }

            if (_input.Player.MoveRight.IsPressed())
            {
                direction = MovementAxis.Horizontal;
                value = 1;
            }

            SendMarker(direction, value);
        }

        private void RightPressed()
        {
            var direction = MovementAxis.Horizontal;
            var value = 1;

            if (_input.Player.MoveLeft.IsPressed())
            {
                value = 0;
                
                var back = _input.Player.MoveBackward.IsPressed() ? -1 : 0;
                var forth = _input.Player.MoveForward.IsPressed() ? 1 : 0;

                if (_input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed())
                {
                    direction = MovementAxis.Vertical;
                    value = back + forth;
                }
            }
            
            SendMarker(direction, value);
        }

        private void RightReleased()
        {
            var direction = MovementAxis.Horizontal;
            var value = 0;

            var back = _input.Player.MoveBackward.IsPressed() ? -1 : 0;
            var forth = _input.Player.MoveForward.IsPressed() ? 1 : 0;
            
            if (_input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed())
            {
                direction = MovementAxis.Vertical;
                value = back + forth;
            }

            if (_input.Player.MoveLeft.IsPressed())
            {
                direction = MovementAxis.Horizontal;
                value = -1;
            }

            SendMarker(direction, value);
        }
        

        private void SendMarker(MovementAxis vec, int val)
        {
            world.AddMarker(new MovementMarker {Axis = vec, Value = val});
        }

        void IModuleBase.OnDeconstruct()
        {
            _input.Dispose();
        }

        void IUpdate.Update(in float deltaTime) {}
    }
}