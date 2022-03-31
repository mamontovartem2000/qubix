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

			_input.Player.MoveForward.started += ctx => world.AddMarker(new ForwardMarker {ActorID = 1, State = InputState.Pressed, Axis = MovementAxis.Vertical});
			_input.Player.MoveForward.canceled += ctx => ForwardReleased();

			_input.Player.MoveBackward.started += ctx => world.AddMarker(new BackwardMarker {ActorID = 1, State = InputState.Pressed, Axis = MovementAxis.Vertical});
			_input.Player.MoveBackward.canceled += ctx => BackwardReleased();

			_input.Player.MoveLeft.started += ctx => world.AddMarker(new LeftMarker {ActorID = 1, State = InputState.Pressed, Axis = MovementAxis.Horizontal});
			_input.Player.MoveLeft.canceled += ctx => LeftReleased();
			
			_input.Player.MoveRight.started += ctx => world.AddMarker(new RightMarker {ActorID = 1, State = InputState.Pressed, Axis = MovementAxis.Horizontal});
			_input.Player.MoveRight.canceled += ctx => RightReleased();

			_input.Player.LeftShoot.started += ctx => world.AddMarker(new MouseLeftMarker {ActorID = 1, State = InputState.Pressed});
			_input.Player.LeftShoot.canceled += ctx => world.AddMarker(new MouseLeftMarker {ActorID = 1, State = InputState.Released});

			_input.Player.RightShoot.started += ctx => world.AddMarker(new MouseRightMarker {ActorID = 1, State = InputState.Pressed});
			_input.Player.RightShoot.canceled += ctx => world.AddMarker(new MouseRightMarker {ActorID = 1, State = InputState.Released});

			_input.Player.LockDirection.started += ctx => world.AddMarker(new LockDirectionMarker { ActorID = 1, State = InputState.Pressed });
			_input.Player.LockDirection.canceled += ctx => world.AddMarker(new LockDirectionMarker { ActorID = 1, State = InputState.Released });

			_input.Player.Skill1.performed += ctx => world.AddMarker(new SkillOneMarker {ActorID = 1});
			_input.Player.Skill2.performed += ctx => world.AddMarker(new SkillTwoMarker {ActorID = 1});
			_input.Player.Skill3.performed += ctx => world.AddMarker(new SkillThreeMarker {ActorID = 1});
			_input.Player.Skill4.performed += ctx => world.AddMarker(new SkillFourMarker {ActorID = 1});

		}

		private void ForwardReleased()
		{
			if (_input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed())
			{
				world.AddMarker(new ForwardMarker {ActorID = 1, State = InputState.Released, Axis = MovementAxis.Horizontal});
			}
			else
			{
				world.AddMarker(new ForwardMarker {ActorID = 1, State = InputState.Released, Axis = MovementAxis.Vertical});
			}
		}

		private void BackwardReleased()
		{
			if (_input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed())
			{
				world.AddMarker(new BackwardMarker {ActorID = 1, State = InputState.Released, Axis = MovementAxis.Horizontal});
			}
			else
			{
				world.AddMarker(new BackwardMarker {ActorID = 1, State = InputState.Released, Axis = MovementAxis.Vertical});
			}
		}

		private void LeftReleased()
		{
			if (_input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed())
			{
				world.AddMarker(new LeftMarker {ActorID = 1, State = InputState.Released, Axis = MovementAxis.Vertical});
			}
			else
			{
				world.AddMarker(new LeftMarker {ActorID = 1, State = InputState.Released, Axis = MovementAxis.Horizontal});
			}
		}

		private void RightReleased()
		{
			if (_input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed())
			{
				world.AddMarker(new RightMarker {ActorID = 1, State = InputState.Released, Axis = MovementAxis.Vertical});
			}
			else
			{
				world.AddMarker(new RightMarker {ActorID = 1, State = InputState.Released, Axis = MovementAxis.Horizontal});
			}
		}
		
		void IModuleBase.OnDeconstruct() {}
		void IUpdate.Update(in float deltaTime) {}
	}
}