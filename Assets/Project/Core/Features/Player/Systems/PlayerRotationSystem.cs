using ME.ECS;
using Project.Core.Features.GameState.Components;
using Project.Core.Features.Player.Components;
using UnityEngine;

namespace Project.Core.Features.Player.Systems
{
    #region usage
#pragma warning disable
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class PlayerRotationSystem : ISystemFilter
    {
        private PlayerFeature _feature;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-PlayerRotationSystem")
                .With<PlayerIsRotating>()
                .WithoutShared<GameFinished>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            if (Vector3.Distance(entity.GetPosition(), entity.Read<PlayerMoveTarget>().Value) > 0)
                return;

            var faceDirection = entity.Read<FaceDirection>().Value;
            bool clockwiseRotation = entity.Read<PlayerIsRotating>().Clockwise;
            Vector3 buffer = Vector3.zero;

            if (faceDirection == Vector3.forward)
                buffer = clockwiseRotation ? Vector3.right : Vector3.left;
            else if (faceDirection == Vector3.left)
                buffer = clockwiseRotation ? Vector3.forward : Vector3.back;
            else if (faceDirection == Vector3.back)
                buffer = clockwiseRotation ? Vector3.left : Vector3.right;
            else if (faceDirection == Vector3.right)
                buffer = clockwiseRotation ? Vector3.back : Vector3.forward;

            if (buffer != Vector3.zero)
                entity.Get<FaceDirection>().Value = buffer;

            entity.Remove<PlayerIsRotating>();
        }
    }
}