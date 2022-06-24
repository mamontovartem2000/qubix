using System;
using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.Avatar;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Mechanics.Features.Avatar.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class AvatarRotationSystem : ISystemFilter
    {
        private AvatarFeature feature;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this.feature);
        }

        void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-AvatarRotationSystem")
                .With<AvatarTag>()
                .With<FaceDirection>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref readonly var dir = ref entity.Read<FaceDirection>().Value;
            ref readonly var input = ref entity.Owner().Read<MoveInput>();
            var direction = input.Axis == MovementAxis.Vertical ? new float3(1, 0, 0) : new float3(0, 0, -1);

            var rot = entity.GetRotation();

            if (input.Amount != 0)
            {
                if (!entity.Owner().Has<LockTarget>())
                {
                    entity.Get<FaceDirection>().Value = direction * input.Amount;
                }
            }

            var newRot = quaternion.LookRotation(dir, new float3(0, 1, 0));
            entity.SetRotation(Quaternion.RotateTowards(rot, newRot, Consts.Movement.ROTATION_SPEED));

        }
    }
}