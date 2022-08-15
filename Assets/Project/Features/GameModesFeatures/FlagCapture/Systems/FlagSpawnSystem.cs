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
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class FlagSpawnSystem : ISystemFilter
    {
        private FlagCaptureFeature feature;
        public World world { get; set; }
        private Filter _spawnersFilter;


        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this.feature);

            Filter.Create("Spawners-Filter")
                .With<FlagSpawnerTag>()
                .Push(ref _spawnersFilter);
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-FlagSpawnSystem")
                .With<FlagNeedRespawn>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            ref var time = ref entity.Get<FlagNeedRespawn>().SpawnDelay;

            if (time > 0f)
            {
                time -= deltaTime;
                return;
            }

            var team = entity.Read<TeamTag>().Value;
            var spawner = GetSpawnerByTeam(team);
            SceneUtils.ModifyFree(spawner.GetPosition(), false);
            
            var flag = feature.SpawnFlag(team);
            flag.Set(new FlagOnSpawn());
            flag.SetPosition(spawner.GetPosition());
                
            entity.Destroy();
        }

        private Entity GetSpawnerByTeam(int team)
        {
            foreach (var spawner in _spawnersFilter)
            {
                if (spawner.Read<TeamTag>().Value == team) 
                    return spawner;
            }
            
            return Entity.Empty;
        }
    }
}