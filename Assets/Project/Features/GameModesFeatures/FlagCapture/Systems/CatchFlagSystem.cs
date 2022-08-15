using Assets.Project.Common.Components;
using ME.ECS;

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
    using Project.Common.Components;
    using Project.Common.Utilities;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class CatchFlagSystem : ISystemFilter
    {
        private FlagCaptureFeature _feature;
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-CatchFlagSystem")
                .With<FlagTag>()
                .With<Collided>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var player = entity.Read<Collided>().ApplyTo;

            if (player.Has<PlayerTag>() == false) return;

            if (player.Read<TeamTag>().Value == entity.Read<TeamTag>().Value)
            {
                if (entity.Has<FlagOnSpawn>())
                {
                    entity.Remove<Collided>();
                    return;
                }

                _feature.CreateFlagRespawnRequest(entity.Read<TeamTag>().Value, GameConsts.GameModes.FlagCapture.FLAG_RESPAWN_TIME);
            }
            else
            {
                var playerFlag = _feature.SpawnFlagOnPlayer();
                playerFlag.SetParent(player.Avatar());
                playerFlag.SetLocalPosition(fp3.zero);
                playerFlag.SetRotation(fpquaternion.zero);
                
                player.Set(new CarriesTheFlag { Team = entity.Read<TeamTag>().Value, Flag = playerFlag});
                player.Avatar().Set(new HealingBuff
                {
                    TimeInterval = GameConsts.GameModes.FlagCapture.Buffs.HEALING_TIME_INTERVAL,
                    HealsPercent = GameConsts.GameModes.FlagCapture.Buffs.HEALTH_REGENERATION_PERCENTAGE
                });
            }
            
            SceneUtils.ModifyFree(entity.GetPosition(), true);
            entity.Destroy();
        }
    }
}