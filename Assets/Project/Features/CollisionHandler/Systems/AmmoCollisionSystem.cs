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
                .With<AmmoTag>()
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
            foreach (var ammoEntity in _ammoFilter)
            {
                if (entity.GetPosition() == ammoEntity.GetPosition())
                {
                    ref var ammo = ref ammoEntity.Get<AmmoTag>();

                    if (entity.Has<RightWeapon>())
                    {
                        ref var weapon = ref entity.Get<RightWeapon>();

                        if (weapon.Type == ammo.WeaponType)
                        {
                            weapon.Count = weapon.Count + ammo.AmmoCount < weapon.MaxCount ? weapon.Count + ammo.AmmoCount : weapon.MaxCount;
                            world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity);
                            ammo.Spawner.Get<AmmoTileTag>().Spawned = false;
                        
                            ammoEntity.Destroy();
                        }
                    }
                    else
                    {
                        entity.Set(new RightWeapon {Type = ammo.WeaponType, MaxCount = ammo.MaxAmmoCount, Count = ammo.AmmoCount, Cooldown = ammo.WeaponCooldown});
                        world.GetFeature<EventsFeature>().rightWeaponFired.Execute(entity);
                        ammo.Spawner.Get<AmmoTileTag>().Spawned = false;

                        ammoEntity.Destroy();
                    }
                }
            }   
        }
    }
}