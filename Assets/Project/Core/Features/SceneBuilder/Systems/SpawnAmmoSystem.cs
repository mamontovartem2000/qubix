using ME.ECS;
using Project.Core.Features.Player.Components;
using Project.Core.Features.SceneBuilder.Components;

namespace Project.Core.Features.SceneBuilder.Systems 
{
    #region usage

    

    #pragma warning disable
#pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class SpawnAmmoSystem : ISystemFilter 
    {
        public World world { get; set; }
        
        private SceneBuilderFeature _feature;
        private ViewId _rocketID, _rifleID;
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this._feature);
            _rocketID = world.RegisterViewSource(_feature.RocketAmmoView);
            _rifleID = world.RegisterViewSource(_feature.RifleAmmoView);
        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-SpawnAmmoSystem")
                .With<AmmoTileTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var spawner = ref entity.Get<AmmoTileTag>();
            
            if(spawner.Spawned) return;

            if (spawner.Timer - deltaTime > 0)
            {
                spawner.Timer -= deltaTime;
            }
            else
            {
                var rnd = world.GetRandomRange(0, 10);
                var ammo = new Entity("ammo");

                if (rnd % 2 == 0)
                {
                    ammo.Set(new AmmoTag {WeaponType = WeaponType.Rocket, MaxAmmoCount = 10, AmmoCount = 5, WeaponCooldown = 0.5f, Spawner = entity});
                    ammo.InstantiateView(_rocketID);
                }
                else
                {
                    ammo.Set(new AmmoTag {WeaponType = WeaponType.Rifle, MaxAmmoCount = 4, AmmoCount = 2, WeaponCooldown = 4f, Spawner = entity});
                    ammo.InstantiateView(_rifleID);
                }

                ammo.SetPosition(entity.GetPosition());

                spawner.Spawned = true;
                spawner.Timer = spawner.TimerDefault;
            }
        }
    }
}