using ME.ECS;
<<<<<<< Updated upstream:Assets/Project/Features/Player/Systems/PlayerMovementSystem.cs
using Project.Features.Components;
using Project.Features.Projectile.Components;
=======
using Project.Core.Features.Player.Components;
using Project.Core.Features.SceneBuilder;
>>>>>>> Stashed changes:Assets/Project/Core/Features/Player/Systems/PlayerMovementSystem.cs
using UnityEngine;

namespace Project.Features.Player.Systems
{
    #region usage



#pragma warning disable
    using Components;
    using Markers;
    using Modules;
    using Project.Components;
    using Project.Markers;
    using Project.Modules;
    using Project.Systems;
    using Project.Utilities;
    using Systems;

#pragma warning restore

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
        private SceneBuilderFeature _scene;


        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            this.GetFeature(out _scene);
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
                .With<Moving>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            int movingDir = entity.Read<Moving>().Direction;
            Vector3 positon = entity.GetPosition();

            float speed = 0;
            if (movingDir == 1)
                speed = entity.Read<PlayerMovementSpeed>().Forward;
            else
                speed = entity.Read<PlayerMovementSpeed>().Backward;


            if (movingDir != 0)
            {
                Vector3 faceDirection = entity.Read<FaceDirection>().Value;

                if (!SceneUtils.IsWalkable(entity.GetPosition(), faceDirection * movingDir)) return;     
                
                entity.SetPosition(Vector3.MoveTowards(positon, positon + faceDirection * movingDir, speed * deltaTime));       
            }
            else
            {
                //TODO: Возможно надо округлять в другу сторону, если игрок успел уйти на миллиметр с центра и остановился.
                int lastDir = entity.Read<Moving>().LastDirection;
                Vector3 target = Vector3.zero;

                if (lastDir == 1)
                    target = Vector3Int.CeilToInt(positon);
                else if (lastDir == -1)
                    target = Vector3Int.FloorToInt(positon);

                entity.SetPosition(Vector3.MoveTowards(positon, target, speed * deltaTime));
            }

            Vector3 posCell = Vector3Int.FloorToInt(positon);
            Vector3 targetCell = Vector3Int.CeilToInt(positon);
            _scene.Move(posCell, targetCell);
        }
    }
}