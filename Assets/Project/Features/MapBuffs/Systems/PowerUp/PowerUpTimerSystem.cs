using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;

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

    public sealed class PowerUpTimerSystem : ISystemFilter
    {
        private MapBuffsFeature feature;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this.feature);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-PowerUpTimerSystem")
                .With<PlayerTag>()
                .With<PowerUpTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var time = ref entity.Get<PowerUpTag>().Time;
            time += deltaTime;

            if (time >= GameConsts.MapBuffs.POWER_UP_LIFETIME)
            {
                feature.CreatePowerUpCrystalRespawnRequest();
                feature.RemovePowerUp.Apply(entity);
                entity.Remove<PowerUpTag>();
            }
        }
    }
}