using ME.ECS;
using Project.Common.Components;

namespace Project.Features.MapBuffs.Systems.PowerUp
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

    public sealed class PowerUpCrystalSpawnSystem : ISystemFilter
    {
        private MapBuffsFeature feature;
        private Filter _powerUpTiles;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this.feature);
            
            Filter.Create("PowerUpTileFilter")
                .With<PowerUpTileTag>()
                .Without<Spawned>()
                .Push(ref _powerUpTiles);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-PowerUpCrystalSpawnSystem")
                .With<PowerUpNeedRespawn>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var tile in _powerUpTiles) //TODO: foreach is bad idea
            {
                if (tile.Has<Spawned>()) continue;

                entity.Destroy();
                var crystal = CreateNewPowerUpCrystal(tile);
                crystal.SetPosition(tile.GetPosition());
                tile.Set(new Spawned());
                return;
            }
        }
        
        private Entity CreateNewPowerUpCrystal(Entity owner)
        {
            var entity = new Entity("PowerUp");
            entity.Set(new PowerUpTag());
            entity.Set(new CollisionDynamic());
            entity.Set(new Owner() { Value = owner});
            entity.InstantiateView(feature.PowerUpView);

            return entity;
        }
    }
}