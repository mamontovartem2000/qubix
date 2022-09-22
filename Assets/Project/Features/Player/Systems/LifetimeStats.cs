using System;
using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;
using Project.Modules.Network;

namespace Project.Features.Player.Systems
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

    public sealed class LifetimeStats : ISystemFilter
    {
        public static LifetimeStats Stats;
        private List<double> _lifeTimeList = new List<double>();

        private PlayerFeature feature;
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this.feature);
            Stats = this;
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-LifetimeStats")
                .With<PlayerTag>()
                .With<PlayerDead>()
                .With<SpawnTime>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            if (entity == Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom))
            {
                var delta = (DateTime.UtcNow - entity.Read<SpawnTime>().Value).TotalSeconds;
                _lifeTimeList.Add((delta));
                entity.Remove<SpawnTime>();
            }
        }

        public int RoundedAverageLifetime()
        {
            if (_lifeTimeList.Count == 0)
            {
                var player = Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom);
                var time = (DateTime.UtcNow - player.Read<SpawnTime>().Value).TotalSeconds;
                return (int)Math.Round(time);
            }
            
            double sumTime = 0;
            
            foreach (var time in _lifeTimeList)
            {
                sumTime += time;
            }

            var avgTime = sumTime / _lifeTimeList.Count;

            return (int)Math.Round(avgTime);
        }
    }
}