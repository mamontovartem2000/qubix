using ME.ECS;
using Project.Input.InputHandler.Markers;

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

			_input.Player.MoveForward.started += ctx => world.AddMarker(new ForwardMarker {ActorID = 1, State = InputState.Pressed});
			_input.Player.MoveForward.canceled += ctx => world.AddMarker(new ForwardMarker {ActorID = 1, State = InputState.Released});

			_input.Player.MoveBackward.started += ctx => world.AddMarker(new BackwardMarker {ActorID = 1, State = InputState.Pressed});
			_input.Player.MoveBackward.canceled += ctx => world.AddMarker(new BackwardMarker {ActorID = 1, State = InputState.Released});

			_input.Player.RotateLeft.performed += ctx => world.AddMarker(new LeftMarker {ActorID = 1, State = InputState.Pressed});
			_input.Player.RotateRight.performed += ctx => world.AddMarker(new RightMarker {ActorID = 1, State = InputState.Pressed});

			_input.Player.LeftShoot.started += ctx => world.AddMarker(new MouseLeftMarker {ActorID = 1, State = InputState.Pressed});
			_input.Player.LeftShoot.canceled += ctx => world.AddMarker(new MouseLeftMarker {ActorID = 1, State = InputState.Released});

			_input.Player.RightShoot.started += ctx => world.AddMarker(new MouseRightMarker {ActorID = 1, State = InputState.Pressed});
			_input.Player.RightShoot.canceled += ctx => world.AddMarker(new MouseRightMarker {ActorID = 1, State = InputState.Released});

			_input.Player.LockDirection.started += ctx => world.AddMarker(new LockDirectionMarker { ActorID = 1, State = InputState.Pressed });
			_input.Player.LockDirection.canceled += ctx => world.AddMarker(new LockDirectionMarker { ActorID = 1, State = InputState.Released });
		}

		void IModuleBase.OnDeconstruct() {}
		void IUpdate.Update(in float deltaTime) {}
	}
}