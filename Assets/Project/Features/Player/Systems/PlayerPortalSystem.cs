using ME.ECS;
using Project.Features.Projectile.Components;
using Project.Features.SceneBuilder.Components;

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
    using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class PlayerPortalSystem : ISystemFilter 
    {
        private PlayerFeature _feature;
        private SceneBuilderFeature _scene;
        
        public World world { get; set; }
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this._feature);
            world.GetFeature(out _scene);
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
                    var newPos = _scene.GetRandomPortalPosition(entity.GetPosition());
                    entity.Set(new TeleportPlayer());
                    
                    _scene.MoveTo(_scene.PositionToIndex(entity.GetPosition()), _scene.PositionToIndex(newPos));

                    entity.SetPosition(newPos);
                    entity.Get<PlayerMoveTarget>().Value = newPos;
                    return;
                }
            }
        }
    }
}