using ME.ECS;
using Project.Common.Components;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Core.Features.Player.Systems
{
#pragma warning disable
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class InputSafeCheckSystem : ISystemFilter
    {
        public World world { get; set; }

        private PlayerFeature _feature;
        
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
        }
        void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-InputSafeCheckSystem")
                .With<MoveInput>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if(entity.Get<Owner>().Value != _feature.GetPlayerByID(NetworkData.SlotInRoom)) return;
            var input = _feature.PlayerInput;
            ref readonly var axis = ref entity.Read<MoveInput>().Axis;
            ref readonly var value = ref entity.Read<MoveInput>().Value;
            
            var back = input.Player.MoveBackward.IsPressed() ? -1 : 0;
            var forth = input.Player.MoveForward.IsPressed() ? 1 : 0;
            var left = input.Player.MoveLeft.IsPressed() ? -1 : 0;
            var right = input.Player.MoveRight.IsPressed() ? 1 : 0;

            var hor = left + right;
            var ver = forth + back;

            var current = axis == MovementAxis.Horizontal ? hor : ver;
            
            if (value != current)
            {
                Debug.Log("yeet");
            }
        }
    }
}