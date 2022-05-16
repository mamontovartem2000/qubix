using ME.ECS;
using ME.ECS.Collections;
using Project.Common.Components;

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
            
        }
    }
}