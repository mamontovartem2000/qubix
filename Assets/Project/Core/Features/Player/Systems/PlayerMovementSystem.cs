using ME.ECS;
using Project.Core.Features.GameState.Components;
using Project.Core.Features.Player.Components;
using Project.Core.Features.SceneBuilder;
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

    public sealed class PlayerMovementSystem : ISystemFilter
    {
        private PlayerFeature _feature;
        private SceneBuilderFeature _scene;
        
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);
            world.GetFeature(out _scene);
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
                .WithoutShared<GameFinished>()
                .Push();
        }

        private float _smoothTurn;

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var current = entity.Read<FaceDirection>().Value;

            // Y Axis rotation;
            var targetAngle = Mathf.Atan2(current.x, current.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(entity.GetRotation().eulerAngles.y, targetAngle, ref _smoothTurn, 0.5f * deltaTime);
            
            entity.SetRotation(Quaternion.Euler(0f, angle, 0f));
            
            if (Vector3.Distance(entity.GetPosition(), entity.Read<PlayerMoveTarget>().Value) <= 0)
            {               
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
                        Vector3 faceDirection = entity.Read<FaceDirection>().Value;

                        var direction = entity.Read<PlayerIsMoving>().Forward ? faceDirection : -faceDirection;

                        if (!SceneUtils.IsWalkable(entity.GetPosition(), direction)) return;

                        Vector3 positon = entity.GetPosition();
                        _scene.Move(positon, positon + direction);

                        entity.Set(new PlayerMovementSpeed {Value = entity.Read<PlayerIsMoving>().Forward ? 4 : 2}); //TODO: Вынести скорость в инспектор 
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