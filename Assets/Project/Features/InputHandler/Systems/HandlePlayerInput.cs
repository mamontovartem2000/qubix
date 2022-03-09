using ME.ECS;
using Photon.Pun;
using Project.Features.InputHandler.Markers;

namespace Project.Features.InputHandler.Systems
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
        private InputHandlerFeature _feature;
        private PlayerInput _input; 
        public World world { get; set; }

        void IModuleBase.OnConstruct()
        {
            this._feature = this.world.GetFeature<InputHandlerFeature>();
            _input = new PlayerInput();
            _input.Enable();
            SubscribeToMethods();
        }

        void IModuleBase.OnDeconstruct() {}

        private void SubscribeToMethods()
        {
            _input.Player.MoveForward.started += ctx => StartMoveForward();
            _input.Player.MoveForward.canceled += ctx => EndMoveForward();

            _input.Player.MoveBackward.started += ctx => StartMoveBackward();
            _input.Player.MoveBackward.canceled += ctx => EndMoveBackward();

            _input.Player.RotateLeft.performed += ctx => RotateLeft();
            _input.Player.RotateRight.performed += ctx => RotateRight();

            _input.Player.LeftShoot.started += ctx => StartLeftShoot();
            _input.Player.LeftShoot.canceled += ctx => ReleseLeftShoot();

            _input.Player.RightShoot.started += ctx => StartRightShoot();
            _input.Player.RightShoot.canceled += ctx => ReleseRightShoot();
        }

        private void StartMoveForward()
        {
            if (_input.Player.MoveBackward.IsPressed())
            {
                world.AddMarker(new KeyReleasedMarker
                { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Backward });
            }
            else
            {
                world.AddMarker(new KeyPressedMarker
                { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Forward });
            }
        }

        private void StartMoveBackward()
        {
            if (_input.Player.MoveForward.IsPressed())
            {
                world.AddMarker(new KeyReleasedMarker
                { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Forward });
            }
            else
            {
                world.AddMarker(new KeyPressedMarker
                { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Backward });
            }
        }

        private void EndMoveForward()
        {
            if (_input.Player.MoveBackward.IsPressed())
            {
                world.AddMarker(new KeyPressedMarker()
                { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Backward });
            }
            else
            {
                world.AddMarker(new KeyReleasedMarker
                { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Forward });
            }
        }

        private void EndMoveBackward()
        {
            if (_input.Player.MoveForward.IsPressed())
            {
                world.AddMarker(new KeyPressedMarker
                { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Forward });
            }
            else
            {
                world.AddMarker(new KeyReleasedMarker
                { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Backward });
            }
        }

        private void RotateLeft()
        {
            world.AddMarker(new KeyPressedMarker
            { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Left });
        }

        private void RotateRight()
        {
            world.AddMarker(new KeyPressedMarker
            { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Key = Key.Right });
        }

        private void StartLeftShoot()
        {
            world.AddMarker(new ButtonPressedMarker
            { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Button = Button.Left });
            // Debug.Log("Left Pressed");
        }

        private void StartRightShoot()
        {
            world.AddMarker(new ButtonPressedMarker
            { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Button = Button.Right });
            // Debug.Log("Right Pressed");
        }

        private void ReleseLeftShoot()
        {
            world.AddMarker(new ButtonReleasedMarker
            { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Button = Button.Left });
            // Debug.Log("Left Released");
        }

        private void ReleseRightShoot()
        {
            world.AddMarker(new ButtonReleasedMarker
            { ActorID = PhotonNetwork.LocalPlayer.ActorNumber, Button = Button.Right });
            // Debug.Log("Right Released");
        }

        public void Update(in float deltaTime)
        {
            
        }
    }
}