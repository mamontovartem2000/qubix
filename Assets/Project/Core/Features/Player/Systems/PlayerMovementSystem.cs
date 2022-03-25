using ME.ECS;
using Project.Core.Features.Player.Components;
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
        public World world { get; set; }
        
        private PlayerFeature _feature;

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
            return Filter.Create("Filter-PlayerMovementSystem")
                .With<PlayerTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var target = ref entity.Get<PlayerMoveTarget>().Value;

            if (entity.GetPosition() != target)
            {
                entity.SetPosition(Vector3.MoveTowards(entity.GetPosition(),entity.Read<PlayerMoveTarget>().Value,
                    entity.Read<PlayerMovementSpeed>().Value * deltaTime));

                target += entity.Read<PlayerTag>().FaceDirection * (entity.Read<PlayerMovementSpeed>().Value * deltaTime);

            }
            
            // if (Vector3.Distance(entity.GetPosition(), entity.Read<PlayerMoveTarget>().Value) <= 0)
            // {
            //     if (entity.Has<PlayerHasStopped>())
            //     {
            //         if (entity.Has<PlayerIsMoving>())
            //         {
            //             entity.Remove<PlayerIsMoving>();
            //         }
            //     }
            //     else
            //     {
            //         if (entity.Has<PlayerIsMoving>())
            //         {
            //             var direction = entity.Read<PlayerIsMoving>().Forward
            //                 ? entity.Read<PlayerTag>().FaceDirection
            //                 : -entity.Read<PlayerTag>().FaceDirection;
            //
            //             entity.Set(new PlayerMovementSpeed {Value = entity.Read<PlayerIsMoving>().Forward ? 4 : 2});
            //             entity.Set(new PlayerMoveTarget {Value = entity.GetPosition() + direction});
            //         }
            //     }
            // }
            // else
            // {
            //     entity.SetPosition(Vector3.MoveTowards(entity.GetPosition(), entity.Read<PlayerMoveTarget>().Value,
            //         entity.Read<PlayerMovementSpeed>().Value * deltaTime));
            // }
        }
    }
}