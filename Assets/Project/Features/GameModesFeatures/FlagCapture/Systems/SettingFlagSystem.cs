using ME.ECS;
using Project.Common.Components;

namespace Project.Features.GameModesFeatures.FlagCapture.Systems
{
    #region usage
#pragma warning disable
    using Project.Components;
    using Project.Modules;
    using Project.Systems;
    using Project.Markers;
    using Components;
    using Modules;
    using Systems;
    using Markers;

#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class SettingFlagSystem : ISystemFilter
    {
        private FlagCaptureFeature _feature;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-SettingFlagSystem")
                .With<FlagTag>()
                .With<Collided>()
                .With<FlagOnSpawn>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var player = entity.Read<Collided>().ApplyTo;

            if (!player.Has<CarriesTheFlag>()) return;
            
            var playerTeam = player.Read<TeamTag>().Value;
            
            if (playerTeam == entity.Read<TeamTag>().Value)
            {
                _feature.UpdateFlagScore(playerTeam);
                _feature.CreateFlagRespawnRequest(player.Read<CarriesTheFlag>().Team);
                
                var carry = player.Read<CarriesTheFlag>();
                carry.Flag.Destroy();
                
                _feature.RemoveFlagBearer.Apply(player);
                entity.Remove<Collided>();
            }
        }
    }
}