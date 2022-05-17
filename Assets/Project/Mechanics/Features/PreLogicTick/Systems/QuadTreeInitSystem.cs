using ME.ECS;
using ME.ECS.Collections;
using Project.Common.Components;
using Project.Core;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Mechanics.Features.PreLogicTick.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class QuadTreeInitSystem : ISystem, IAdvanceTick
    {
        public World world { get; set; }
        public static NativeArray<QuadElement<Entity>> array;

        private PreLogicTickFeature _feature;
        private Filter _collisionFilter;
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);

            Filter.Create("collision-Filter")
                .With<CollisionTag>()
                .Push(ref _collisionFilter);
        }

        void ISystemBase.OnDeconstruct() {}
        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            var width = SceneUtils.Width;
            var height = SceneUtils.Height;
            array = new NativeArray<QuadElement<Entity>>(_collisionFilter.Count, Allocator.TempJob);

            var i = 0;
            
            foreach (var coll in _collisionFilter)
            {
                var pos = coll.GetPosition();
                if (pos.x < 0 || pos.x > SceneUtils.Width || pos.z < 0 || pos.z > SceneUtils.Height) continue;
                
                array[i++] = new QuadElement<Entity>() {element = coll, pos = coll.GetPosition().XZ()};
            }
            
            NativeQuadTreeUtils.PrepareTick(new AABB2D(new Vector2(width/2, height/2), new float2(width/2, height/2)),
                array, array.Length);
        }
    }
}