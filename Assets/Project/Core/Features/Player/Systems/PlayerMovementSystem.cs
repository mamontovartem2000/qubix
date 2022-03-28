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
                .WithoutShared<GameFinished>()
                .With<MoveInput>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            int movingDir = entity.Read<MoveInput>().Value;
            Vector3 positon = entity.GetPosition();        

            if (movingDir != 0)
            {
                if ((entity.Read<PlayerMoveTarget>().Value - positon).sqrMagnitude <= 0.05f)
                {
                    entity.SetPosition(Vector3Int.CeilToInt(entity.Read<PlayerMoveTarget>().Value));
                    Vector3 faceDirection = entity.Read<FaceDirection>().Value;
                    Vector3 newTarget = entity.Read<PlayerMoveTarget>().Value + faceDirection * movingDir;

                    if (SceneUtils.IsWalkable(newTarget))
                    {
                        _scene.Move(entity.Read<PlayerMoveTarget>().Value, newTarget);
                        entity.Get<PlayerMoveTarget>().Value = newTarget;

                        if (entity.Has<TeleportPlayer>())
                        {
                            if (entity.Read<TeleportPlayer>().NeedDelete)
                                entity.Remove<TeleportPlayer>();
                            else
                                entity.Get<TeleportPlayer>().NeedDelete = true;
                        }                      
                    }
                }
            }

            float speed = 0;
            if (movingDir == 1)
                speed = entity.Read<PlayerMovementSpeed>().Forward;
            else
                speed = entity.Read<PlayerMovementSpeed>().Backward;

            entity.SetPosition(Vector3.MoveTowards(positon, entity.Read<PlayerMoveTarget>().Value, speed * deltaTime));          
        }
    }
}