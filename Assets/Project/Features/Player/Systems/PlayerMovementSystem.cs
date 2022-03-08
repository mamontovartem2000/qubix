using ME.ECS;
using Project.Features.Projectile.Components;
using Project.Features.SceneBuilder.Components;
using UnityEngine;

namespace Project.Features.Player.Systems
{
    #region usage



#pragma warning disable
    using Project.Components;
    using Project.Modules;
    using Project.Systems;
    using Project.Markers;
    using Components;
    using Modules;
    using Systems;
    using Markers;

#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

    public sealed class PlayerMovementSystem : ISystemFilter
    {
        private PlayerFeature feature;
        private SceneBuilderFeature _builder;
        
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this.feature);
            world.GetFeature(out _builder);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-PlayerMovementSystem")
                .With<PlayerTag>()
                .Push();
        }

        private float _smoothTurn;

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var current = entity.Read<PlayerTag>().FaceDirection;

            // Y Axis rotation;
             var targetAngle = Mathf.Atan2(current.x, current.z) * Mathf.Rad2Deg;
             var angle = Mathf.SmoothDampAngle(entity.GetRotation().eulerAngles.y, targetAngle, ref _smoothTurn,
                 0.5f * deltaTime);
            
             entity.SetRotation(Quaternion.Euler(0f, angle, 0f));
            
            if (Vector3.Distance(entity.GetPosition(), entity.Read<PlayerMoveTarget>().Value) <= 0)
            {
                if (entity.Has<PlayerIsRotating>())
                {
                    if (current == Vector3.forward)
                    {
                        entity.Get<PlayerTag>().FaceDirection =
                            entity.Read<PlayerIsRotating>().Clockwise ? Vector3.right : Vector3.left;
                    }
                    else if (current == Vector3.left)
                    {
                        entity.Get<PlayerTag>().FaceDirection =
                            entity.Read<PlayerIsRotating>().Clockwise ? Vector3.forward : Vector3.back;
                    }
                    else if (current == Vector3.back)
                    {
                        entity.Get<PlayerTag>().FaceDirection =
                            entity.Read<PlayerIsRotating>().Clockwise ? Vector3.left : Vector3.right;
                    }
                    else if (current == Vector3.right)
                    {
                        entity.Get<PlayerTag>().FaceDirection =
                            entity.Read<PlayerIsRotating>().Clockwise ? Vector3.back : Vector3.forward;
                    }

                    entity.Remove<PlayerIsRotating>();
                }
                
                if (entity.Has<PlayerHasStopped>())
                {
                    if (entity.Has<PlayerIsMoving>())
                    {
                        entity.Remove<PlayerIsMoving>();
                    }
                }
                else
                {
                    if (entity.Has<PlayerIsMoving>())
                    {
                        var direction = entity.Read<PlayerIsMoving>().Forward
                            ? entity.Read<PlayerTag>().FaceDirection
                            : -entity.Read<PlayerTag>().FaceDirection;

                        if (!_builder.IsWalkable(entity.GetPosition(), direction)) return;

                        _builder.MoveTo(
                            _builder.PositionToIndex( entity.GetPosition()),
                            _builder.PositionToIndex(entity.GetPosition() + direction));

                        entity.Set(new PlayerMovementSpeed {Value = entity.Read<PlayerIsMoving>().Forward ? 4 : 2});
                        entity.Set(new PlayerMoveTarget {Value = entity.GetPosition() + direction});
                    }
                }
            }
            else
            {
                if (entity.Has<TeleportPlayer>())
                    entity.Remove<TeleportPlayer>();
                
                entity.SetPosition(Vector3.MoveTowards(entity.GetPosition(), entity.Read<PlayerMoveTarget>().Value,
                    entity.Read<PlayerMovementSpeed>().Value * deltaTime));
            }
        }
    }
}