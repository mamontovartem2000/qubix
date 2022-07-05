using System;
using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using Project.Input.InputHandler.Markers;
using Project.Modules.Network;

namespace Project.Features.Player.Systems
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

        public NetworkModule Net;

        private PlayerFeature _feature;

        private Filter _playerFilter;

        private RPCId _movement;
        private RPCId _mouseLeft, _mouseRight, _lockDirection;
        private RPCId _firstSkill, _secondSkill, _thirdSkill, _fourthSkill;
        private RPCId _tabulation;
        void ISystemBase.OnConstruct()
        {
            Net = world.GetModule<NetworkModule>();

            this.GetFeature(out _feature);
            Net.RegisterObject(this);
            RegisterRPSs(Net);

            Filter.Create("Filter-Players")
                .With<PlayerTag>()
                .Push(ref _playerFilter);
        }

        private void RegisterRPSs(NetworkModule net)
        {
            _mouseLeft = net.RegisterRPC(new Action<MouseLeftMarker>(LeftMouse_RPC).Method);
            _mouseRight = net.RegisterRPC(new Action<MouseRightMarker>(RightMouse_RPC).Method);
            _lockDirection = net.RegisterRPC(new Action<LockDirectionMarker>(SpaceKey_RPC).Method);
            
            _firstSkill = net.RegisterRPC(new Action<FirstSkillMarker>(FirstSkill_RPC).Method);
            _secondSkill = net.RegisterRPC(new Action<SecondSkillMarker>(SecondSkill_RPC).Method);
            _thirdSkill = net.RegisterRPC(new Action<ThirdSkillMarker>(ThirdSkill_RPC).Method);
            _fourthSkill = net.RegisterRPC(new Action<FourthSkillMarker>(FourthSkill_RPC).Method);

            _movement = net.RegisterRPC(new Action<MovementMarker>(Movement_RPC).Method);
            
            _tabulation = net.RegisterRPC(new Action<TabulationMarker>(TabKey_RPC).Method);
        }

        void ISystemBase.OnDeconstruct() { }

        void IUpdate.Update(in float deltaTime)
        {
            if (world.GetMarker(out MovementMarker move)) Net.RPC(this, _movement, move);

            if (world.GetMarker(out MouseLeftMarker mlm)) Net.RPC(this, _mouseLeft, mlm);
            if (world.GetMarker(out MouseRightMarker mrm)) Net.RPC(this, _mouseRight, mrm);

            if (world.GetMarker(out LockDirectionMarker sm)) Net.RPC(this, _lockDirection, sm);

            if (world.GetMarker(out FirstSkillMarker first)) Net.RPC(this, _firstSkill, first);
            if (world.GetMarker(out SecondSkillMarker second)) Net.RPC(this, _secondSkill, second);
            if (world.GetMarker(out ThirdSkillMarker third)) Net.RPC(this, _thirdSkill, third);
            if (world.GetMarker(out FourthSkillMarker fourth)) Net.RPC(this, _fourthSkill, fourth);
            
            if (world.GetMarker(out TabulationMarker tm)) Net.RPC(this, _tabulation, tm);
        }

        private void Movement_RPC(MovementMarker move)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);

            player.Set(new MoveInput { Axis = move.Axis, Amount = move.Value });
        }

        private void LeftMouse_RPC(MouseLeftMarker mlm)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);

            switch (mlm.State)
            {
                case InputState.Pressed:
                    {
                        player.Set(new LeftWeaponShot());
                        break;
                    }
                case InputState.Released:
                    {
                        player.Remove<LeftWeaponShot>();
                        break;
                    }
            }
        }

        private void RightMouse_RPC(MouseRightMarker mrm)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);

            switch (mrm.State)
            {
                case InputState.Pressed:
                    {
                        player.Set(new RightWeaponShot());
                        break;
                    }
                case InputState.Released:
                    {
                        player.Remove<RightWeaponShot>();
                        break;
                    }
            }
        }

        private void SpaceKey_RPC(LockDirectionMarker sm)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);

            switch (sm.State)
            {
                case InputState.Pressed:
                    {
                        player.Set(new LockTarget());
                        break;
                    }
                case InputState.Released:
                    {
                        player.Remove<LockTarget>();
                        break;
                    }
            }
        }
        
        private void TabKey_RPC(TabulationMarker tm)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);

            switch (tm.State)
            {
                case InputState.Pressed:
                {
                    world.GetFeature<EventsFeature>().TabulationOn.Execute(player);
                    break;
                }
                case InputState.Released:
                {
                    world.GetFeature<EventsFeature>().TabulationOff.Execute(player);
                    break;
                }
            }
        }

        private void FirstSkill_RPC(FirstSkillMarker fsm)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<SkillEntities>().FirstSkill;

            if(!entity.Has<Cooldown>())
                entity.Set(new ActivateSkill(), ComponentLifetime.NotifyAllSystems);
        }

        private void SecondSkill_RPC(SecondSkillMarker ssm)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<SkillEntities>().SecondSkill;

            if(!entity.Has<Cooldown>())
                entity.Set(new ActivateSkill(), ComponentLifetime.NotifyAllSystems);
        }

        private void ThirdSkill_RPC(ThirdSkillMarker tsm)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<SkillEntities>().ThirdSkill;

            if(!entity.Has<Cooldown>())
                entity.Set(new ActivateSkill(), ComponentLifetime.NotifyAllSystems);
        }

        private void FourthSkill_RPC(FourthSkillMarker fsm)
        {
            var player = _feature.GetPlayerByID(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            if (!player.Has<PlayerAvatar>()) return;

            ref var entity = ref player.Get<SkillEntities>().FourthSkill;

            if(!entity.Has<Cooldown>())
                entity.Set(new ActivateSkill(), ComponentLifetime.NotifyAllSystems);
        }
    }
}