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
        private float _currentVelocity;
        private float _smoothTurn = 0.05f;
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
            if (!entity.Read<PlayerIsRotating>().Busy)
            {
                if ((entity.Read<PlayerMoveTarget>().Value - entity.GetPosition()).sqrMagnitude > 0.05f)
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
             
                entity.Get<PlayerIsRotating>().Busy = true;
            }
                     
            entity.SetRotation(Quaternion.RotateTowards(entity.GetRotation(), Quaternion.LookRotation(entity.Read<FaceDirection>().Value), 40f));

            if (entity.GetRotation() == Quaternion.LookRotation(entity.Read<FaceDirection>().Value))
            {
                //Debug.Log("End Rotation");
                entity.Remove<PlayerIsRotating>();
            }

            //Debug.Log($"{entity.GetRotation()}, look {Quaternion.LookRotation(entity.Get<FaceDirection>().Value)} ");

            #region AnotherRotation
            //var current = entity.Read<FaceDirection>().Value;
            //var targetAngle = Mathf.Atan2(current.x, current.z) * Mathf.Rad2Deg;
            //var angle = Mathf.SmoothDampAngle(entity.GetRotation().eulerAngles.y, targetAngle, ref _currentVelocity, _smoothTurn);

            //entity.SetRotation(Quaternion.Euler(0f, angle, 0f));

            //Debug.Log($"face {angle}, vrot {targetAngle}");

            //if (targetAngle < 0)
            //    targetAngle += 360;

            //if (Mathf.Round(angle) == targetAngle)
            //{
            //    Debug.Log("End Rotation");
            //    entity.Remove<PlayerIsRotating>();
            //}
            #endregion         
        }
    }
}