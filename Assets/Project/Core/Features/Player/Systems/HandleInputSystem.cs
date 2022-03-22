using System;
using ME.ECS;
using Project.Core.Features.Player.Components;
using Project.Input.InputHandler;
using Project.Input.InputHandler.Markers;
using Project.Modules;
using Key = Project.Input.InputHandler.Markers.Key;

namespace Project.Core.Features.Player.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

    public sealed class HandleInputSystem : ISystem, IUpdate
    {
        public World world { get; set; }
        
        private InputHandlerFeature _feature;
        private Filter _playerFilter;

        void ISystemBase.OnConstruct()
        {
            var network = world.GetModule<NetworkModule>();
            network.RegisterObject(this);
            
            RegisterRPCs(network);

            this.GetFeature(out this._feature);

            Filter.Create("Filter-Players")
                .With<PlayerTag>()
                .Push(ref _playerFilter);
        }

        void ISystemBase.OnDeconstruct(){}

        void IUpdate.Update(in float deltaTime)
        {
            if (world.GetMarker(out KeyPressedMarker keyPressed))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _keyPressed, keyPressed);
            }

            if (world.GetMarker(out KeyReleasedMarker keyReleased))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _keyReleased, keyReleased);
            }
            
            if (world.GetMarker(out ButtonPressedMarker buttonPressed))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _buttonPressed, buttonPressed);
            }

            if (world.GetMarker(out ButtonReleasedMarker buttonReleased))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _buttonReleased, buttonReleased);
            }
        }
        
        private Entity GetEntityByID(int actorID)
        {
            foreach (var entity in _playerFilter)
            {
                if (entity.Read<PlayerTag>().PlayerID == actorID)
                {
                    return entity;
                }
            }
    
            return Entity.Empty;
        }

        private RPCId _keyPressed, _keyReleased;
        private RPCId _buttonPressed, _buttonReleased;
        
        private void RegisterRPCs(NetworkModule net)
        {
            _keyPressed = net.RegisterRPC(new Action<KeyPressedMarker>(KeyPressed_RPC).Method);
            _keyReleased = net.RegisterRPC(new Action<KeyReleasedMarker>(KeyReleased_RPC).Method);
            _buttonPressed = net.RegisterRPC(new Action<ButtonPressedMarker>(ButtonPressed_RPC).Method);
            _buttonReleased = net.RegisterRPC(new Action<ButtonReleasedMarker>(ButtonReleased_RPC).Method);
        }

        private void KeyPressed_RPC(KeyPressedMarker kpm)
        {
            var entity = GetEntityByID(kpm.ActorID);
            
            if(!entity.IsAlive()) return;

            switch (kpm.Key)
            {
                case Key.Forward:
                {
                    entity.Set(new PlayerIsMoving {Forward = true});
                    
                    if (entity.Has<PlayerHasStopped>())
                    {
                        entity.Remove<PlayerHasStopped>();
                    }
                }
                    break;
                case Key.Backward:
                {
                    entity.Set(new PlayerIsMoving {Forward = false});

                    if (entity.Has<PlayerHasStopped>())
                    {
                        entity.Remove<PlayerHasStopped>();
                    }
                }
                    break;
                case Key.Left:
                {
                    entity.Set(new PlayerIsRotating {Clockwise = false});
                }
                    break;
                case Key.Right:
                {
                    entity.Set(new PlayerIsRotating {Clockwise = true});
                }
                    break;
            }
        }
        
        private void KeyReleased_RPC(KeyReleasedMarker krm)
        {
            var entity = GetEntityByID(krm.ActorID);
            if(!entity.IsAlive()) return;

            switch (krm.Key)
            {
                case Key.Forward:
                {
                    entity.Set(new PlayerHasStopped());
                }
                    break;
                case Key.Backward:
                {
                    entity.Set(new PlayerHasStopped());
                }
                    break;
            }
        }
        
        private void ButtonPressed_RPC(ButtonPressedMarker bpm)
        {
            var entity = GetEntityByID(bpm.ActorID);
            if(!entity.IsAlive()) return;

            switch (bpm.Button)
            {
                case Button.Left:
                {
                    entity.Set(new LeftWeaponShot());
                }
                    break;
                case Button.Right:
                {
                    entity.Set(new RightWeaponShot());
                }
                    break;
            }
        }
        private void ButtonReleased_RPC(ButtonReleasedMarker brm)
        {
            var entity = GetEntityByID(brm.ActorID);
            if(!entity.IsAlive()) return;

            switch (brm.Button)
            {
                case Button.Left:
                {
                    entity.Remove<LeftWeaponShot>();
                }
                    break;
                case Button.Right:
                {
                    entity.Remove<RightWeaponShot>();
                }
                    break;
            }
        }
    }
}