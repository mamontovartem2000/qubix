using ME.ECS;
using Project.Features.Player.Components;
using Project.Features.SceneBuilder.Components;

namespace Project.Features.CollisionHandler.Systems 
{
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
    public sealed class AmmoCollisionSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private CollisionHandlerFeature _feature;
        private Filter _ammoFilter;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
            
            Filter.Create("trap-filter")
                .With<RocketAmmoTag>()
                .Push(ref _ammoFilter);
        }
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-AmmoCollisionSystem")
                .With<PlayerTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var collectible in _ammoFilter)
            {
                if (entity.GetPosition() == collectible.GetPosition())
                {
                    entity.Get<RightWeapon>().Ammo += 10;
                    world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity);
                    collectible.Destroy();
                }
            }   
        }
    }
}