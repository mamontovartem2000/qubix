using ME.ECS;
using ME.ECS.Collections;
using Project.Common.Components;
using Project.Core;
using Unity.Collections;
using UnityEngine;

namespace Project.Mechanics.Features.CollisionHandler.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class CollisionDetectionSystem : ISystem, IAdvanceTick
    {
        public World world { get; set; }
        
        private CollisionHandlerFeature _feature;
        private Filter _collisionFilter;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);

            Filter.Create("CollisionDetectionSystem-Filter")
                .With<CollisionTag>()
                .Push(ref _collisionFilter);
        }

        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            var results = new NativeList<QuadElement<Entity>>(Allocator.TempJob);

            foreach (var entity in _collisionFilter)
            {
                results.Clear();
                NativeQuadTreeUtils.GetResults(entity.GetPosition().XZ(), 1f, results);

                foreach (var item in results)
                {
                    if (item.element == entity) continue;
                    if (item.element.Has<CollisionStatic>() && entity.Has<CollisionStatic>()) continue;

                    if (item.element.Has<CircleRect>() && !item.element.Has<Collided>())
                    {
                        // if (entity.Has<CircleRect>())
                        // {
                        //     // if (CollisionUtils.CircleSquareCollision(entity, item.element))
                        //         Debug.Log(CollisionUtils.CircleCircleCollision(entity, item.element));
                        // }
                        // else 
                        if (entity.Has<SquareRect>())
                        {
                            if (CollisionUtils.CircleSquareCollision(item.element, entity))
                            {
                                item.element.Set(new Collided());
                            }
                            // Debug.Log(CollisionUtils.CircleSquareCollision(item.element, entity));
                        }
                        // else if (entity.Has<TriangleRect>())
                        // CollisionUtils.CircleTriangleCollision();
                    }
                    else if (item.element.Has<SquareRect>())
                    {
                        if (entity.Has<CircleRect>() && !entity.Has<Collided>())
                        {
                            if (CollisionUtils.CircleSquareCollision(entity, item.element))
                            {
                                entity.Set(new Collided());
                            }
                            // Debug.Log(CollisionUtils.CircleSquareCollision(entity, item.element));
                        }
                        // else if (entity.Has<SquareRect>())
                        //     CollisionUtils.SquareSquareCollision();
                        // else if (entity.Has<TriangleRect>())
                        //     CollisionUtils.SquareTriangleCollision();
                    }
                    // }
                    // else if (item.element.Has<TriangleRect>())
                    // {
                    //     if (entity.Has<CircleRect>())
                    //         CollisionUtils.CircleTriangleCollision();
                    //     else if (entity.Has<SquareRect>())
                    //         CollisionUtils.SquareTriangleCollision();
                    //     else if (entity.Has<TriangleRect>())
                    //         CollisionUtils.TriangleTriangleCollision();
                    // }
                }

            }


            results.Dispose();
        }
    }
}