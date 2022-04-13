using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player.Components;
using Project.Input.InputHandler.Markers;
using Project.Modules;
using System;
using ME.ECS.Transform;
using UnityEngine;

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

        private PlayerFeature _feature;

        private Filter _playerFilter;

        private RPCId _forward, _backward, _left, _right, _mouseLeft, _mouseRight, _lockDirection;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            var net = world.GetModule<NetworkModule>();
            net.RegisterObject(this);
            RegisterRPSs(net);

            Filter.Create("Filter-Players")
                .With<PlayerTag>()
                .Push(ref _playerFilter);
        }

        private void RegisterRPSs(NetworkModule net)
        {
            _forward = net.RegisterRPC(new Action<ForwardMarker>(ForwardKey_RPC).Method);
            _backward = net.RegisterRPC(new Action<BackwardMarker>(BackwardKey_RPC).Method);
            _left = net.RegisterRPC(new Action<LeftMarker>(LeftKey_RPC).Method);
            _right = net.RegisterRPC(new Action<RightMarker>(RightKey_RPC).Method);
            _mouseLeft = net.RegisterRPC(new Action<MouseLeftMarker>(LeftMouse_RPC).Method);
            _mouseRight = net.RegisterRPC(new Action<MouseRightMarker>(RightMouse_RPC).Method);
            _lockDirection = net.RegisterRPC(new Action<LockDirectionMarker>(SpaceKey_RPC).Method);
        }

        void ISystemBase.OnDeconstruct() {}

        void IUpdate.Update(in float deltaTime)
        {
            if (world.GetMarker(out ForwardMarker fm))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _forward, fm);
            }

            if (world.GetMarker(out BackwardMarker bm))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _backward, bm);
            }

            if (world.GetMarker(out LeftMarker lm))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _left, lm);
            }

            if (world.GetMarker(out RightMarker rm))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _right, rm);
            }

            if (world.GetMarker(out MouseLeftMarker mlm))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _mouseLeft, mlm);
            }

            if (world.GetMarker(out MouseRightMarker mrm))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _mouseRight, mrm);
            }

            if (world.GetMarker(out LockDirectionMarker sm))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _lockDirection, sm);
            }
        }

        private void ForwardKey_RPC(ForwardMarker fm)
        {
            var player = _feature.GetPlayer(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            // var player = world.GetFeature<PlayerFeature>().GetLocalPlayer(fm.ActorID);
            
            // if (!entity.IsAlive()) return;
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<PlayerAvatar>().Value; 
            
            entity.Get<MoveInput>().Axis = fm.Axis;

            switch (fm.State)
            {
                case InputState.Pressed:
                {
                    entity.Get<MoveInput>().Value.y += 1;
                    break;
                }
                case InputState.Released:
                {
                    entity.Get<MoveInput>().Value.y -= 1;
                    break;
                }
            }
        }

        private void BackwardKey_RPC(BackwardMarker bm)
        {
            var player = _feature.GetPlayer(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            // if (!entity.IsAlive()) return;
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<PlayerAvatar>().Value; 

            entity.Get<MoveInput>().Axis = bm.Axis;

            switch (bm.State)
            {
                case InputState.Pressed:
                {
                    entity.Get<MoveInput>().Value.y -= 1;
                    break;
                }
                case InputState.Released:
                {
                    entity.Get<MoveInput>().Value.y += 1;
                    break;
                }
            }
        }

        private void LeftKey_RPC(LeftMarker lm)
        {
            var player = _feature.GetPlayer(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            // if (!entity.IsAlive()) return;
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<PlayerAvatar>().Value; 

            entity.Get<MoveInput>().Axis = lm.Axis;

            switch (lm.State)
            {
                case InputState.Pressed:
                {
                    entity.Get<MoveInput>().Value.x -= 1;
                    break;
                }
                case InputState.Released:
                {
                    entity.Get<MoveInput>().Value.x += 1;
                    break;
                }
            }
        }

        private void RightKey_RPC(RightMarker rm)
        {
            var player = _feature.GetPlayer(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            // if (!entity.IsAlive()) return;
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<PlayerAvatar>().Value; 

            entity.Get<MoveInput>().Axis = rm.Axis;

            switch (rm.State)
            {
                case InputState.Pressed:
                {
                    entity.Get<MoveInput>().Value.x += 1;
                    break;
                }
                case InputState.Released:
                {
                    entity.Get<MoveInput>().Value.x -= 1;
                    break;
                }
            }
        }

        private void LeftMouse_RPC(MouseLeftMarker mlm)
        {
            var player = _feature.GetPlayer(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            // if (!entity.IsAlive()) return;
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<PlayerAvatar>().Value; 

            var nodes = entity.Get<Nodes>().items;

            foreach (var node in nodes)
            {
                switch (mlm.State)
                {
                    case InputState.Pressed:
                    {
                        node.Set(new LeftWeaponShot());
                        node.Set(new MeleeActive());
                        break;
                    }
                    case InputState.Released:
                    {
                        node.Remove<LeftWeaponShot>();
                        node.Remove<LinearActive>();
                        break;
                    }
                }
            }
        }

        private void RightMouse_RPC(MouseRightMarker mrm)
        {
            var player = _feature.GetPlayer(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            // if (!entity.IsAlive()) return;
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<PlayerAvatar>().Value; 
            
            var nodes = entity.Get<Nodes>().items;

            foreach (var node in nodes)
            {
                switch (mrm.State)
                {
                    case InputState.Pressed:
                    {
                        node.Set(new RightWeaponShot());
                        break;
                    }
                    case InputState.Released:
                    {
                        node.Remove<RightWeaponShot>();
                        break;
                    }
                }
            }
        }

        private void SpaceKey_RPC(LockDirectionMarker sm)
        {
            var player = _feature.GetPlayer(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            // if (!entity.IsAlive()) return;
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<PlayerAvatar>().Value; 

            switch (sm.State)
            {
                case InputState.Pressed:
                {
                    entity.Set(new LockTarget());
                    break;
                }
                case InputState.Released:
                {
                    entity.Remove<LockTarget>();
                    break;
                }
            }
        }
    }
}