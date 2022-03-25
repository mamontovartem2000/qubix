using ME.ECS;

namespace Project.Features.CollisionHandler.Systems 
{
    #region usage
<<<<<<< Updated upstream:Assets/Project/Features/CollisionHandler/Systems/ExplosionSystem.cs
    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
=======
    using Components;
    using UnityEngine;
#if ECS_COMPILE_IL2CPP_OPTIONS
>>>>>>> Stashed changes:Assets/Project/Mechanics/Features/CollisionHandler/Systems/ExplosionSystem.cs
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class ExplosionSystem : ISystemFilter 
    {
        public World world { get; set; }
        private CollisionHandlerFeature _feature;
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-ExplosionSystem")
                .With<ExplosionTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity.Get<ExplosionTag>().Value - deltaTime > 0)
            {
                entity.Get<ExplosionTag>().Value -= deltaTime;
            }
            else
            {
                entity.Destroy();
            }            
        }
    }
}