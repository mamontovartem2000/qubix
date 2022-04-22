using ME.ECS;
using Project.Input.InputHandler.Markers;
using Project.Modules.Network;

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

			_input.Player.LeftShoot.started += ctx => LeftMousePressed();
			_input.Player.LeftShoot.canceled += ctx => LeftMouseReleased();

			_input.Player.RightShoot.started += ctx => RightMousePressed();
			_input.Player.RightShoot.canceled += ctx => RightMouseReleased();

			_input.Player.LockDirection.started += ctx => LockDirectionPressed();
			_input.Player.LockDirection.canceled += ctx => LockDirectionReleased();
			
			_input.Player.Skill1.performed += ctx => world.AddMarker(new SkillOneMarker {ActorID = 1});
			_input.Player.Skill2.performed += ctx => world.AddMarker(new SkillTwoMarker {ActorID = 1});
			_input.Player.Skill3.performed += ctx => world.AddMarker(new SkillThreeMarker {ActorID = 1});
			_input.Player.Skill4.performed += ctx => world.AddMarker(new SkillFourMarker {ActorID = 1});

		}

		private void LeftMousePressed()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			world.AddMarker(new MouseLeftMarker {ActorID = id, State = InputState.Pressed});
		}
		
		private void LeftMouseReleased()
		{
			var id = NetworkData.PlayerIdInRoom;

			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			world.AddMarker(new MouseLeftMarker {ActorID = id, State = InputState.Released});
		}
		
		private void RightMousePressed()
		{
			var id = NetworkData.PlayerIdInRoom;

			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			world.AddMarker(new MouseRightMarker {ActorID = id, State = InputState.Pressed});
		}
		
		private void RightMouseReleased()
		{
			var id = NetworkData.PlayerIdInRoom;

			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			world.AddMarker(new MouseRightMarker {ActorID = id, State = InputState.Released});
		}
		
		private void LockDirectionPressed()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			world.AddMarker(new LockDirectionMarker {ActorID = id, State = InputState.Pressed });
		}
		
		private void LockDirectionReleased()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			world.AddMarker(new LockDirectionMarker {ActorID = id, State = InputState.Released });
		}
		
		private void ForwardPressed()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			world.AddMarker(new ForwardMarker {ActorID = id, State = InputState.Pressed, Axis = MovementAxis.Vertical});
		}
		private void ForwardReleased()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			
			if (_input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed())
			{
				world.AddMarker(new ForwardMarker {ActorID = id, State = InputState.Released, Axis = MovementAxis.Horizontal});
			}
			else
			{
				world.AddMarker(new ForwardMarker {ActorID = id, State = InputState.Released, Axis = MovementAxis.Vertical});
			}
		}

		private void BackwardPressed()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;
			world.AddMarker(new BackwardMarker {ActorID = id, State = InputState.Pressed, Axis = MovementAxis.Vertical});
		}

		private void BackwardReleased()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;

			if (_input.Player.MoveLeft.IsPressed() || _input.Player.MoveRight.IsPressed())
			{
				world.AddMarker(new BackwardMarker {ActorID = id, State = InputState.Released, Axis = MovementAxis.Horizontal});
			}
			else
			{
				world.AddMarker(new BackwardMarker {ActorID = id, State = InputState.Released, Axis = MovementAxis.Vertical});
			}
		}

		private void LeftPressed()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;

			world.AddMarker(new LeftMarker {ActorID = id, State = InputState.Pressed, Axis = MovementAxis.Horizontal});	
		}
		
		private void LeftReleased()
		{
			var id = NetworkData.PlayerIdInRoom;
			// var id = Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer().Read<PlayerTag>().PlayerID;

			if (_input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed())
			{
				world.AddMarker(new LeftMarker {ActorID = id, State = InputState.Released, Axis = MovementAxis.Vertical});
			}
			else
			{
				world.AddMarker(new LeftMarker {ActorID = id, State = InputState.Released, Axis = MovementAxis.Horizontal});
			}
		}

		private void RightPressed()
		{
			var id = NetworkData.PlayerIdInRoom;
			
			world.AddMarker(new RightMarker {ActorID = id, State = InputState.Pressed, Axis = MovementAxis.Horizontal});
		}
		
		private void RightReleased()
		{
			var id = NetworkData.PlayerIdInRoom;

			if (_input.Player.MoveForward.IsPressed() || _input.Player.MoveBackward.IsPressed())
			{
				world.AddMarker(new RightMarker {ActorID = id, State = InputState.Released, Axis = MovementAxis.Vertical});
			}
			else
			{
				world.AddMarker(new RightMarker {ActorID = id, State = InputState.Released, Axis = MovementAxis.Horizontal});
			}
		}
		
		void IModuleBase.OnDeconstruct() 
		{
			_input.Dispose();
		}
		void IUpdate.Update(in float deltaTime) {}
	}
}