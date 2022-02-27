using ME.ECS;
using Project.Features.SceneBuilder.Components;
using UnityEngine;

namespace Project.Features.Player.Systems {
    #region usage

    

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class PlayerPortalSystem : ISystemFilter 
    {
        private PlayerFeature feature;
        private SceneBuilderFeature _builder;
        
        public World world { get; set; }
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this.feature);
            world.GetFeature(out _builder);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-PlayerPortalSystem")
                .With<PlayerTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if(entity.Has<TeleportPlayer>()) return;

            foreach (var pos in world.GetSharedData<MapComponents>().PortalsMap)
            {
                if (pos == entity.GetPosition())
                {
                    var newPos = _builder.GetRandomPortalPosition(entity.GetPosition());
                    _builder.MoveTo(_builder.PositionToIndex(entity.GetPosition()), _builder.PositionToIndex(newPos));

                    entity.Set(new TeleportPlayer());
                    entity.SetPosition(newPos);
                    entity.Get<PlayerMoveTarget>().Value = newPos;
                }
            }
        }
    }
}