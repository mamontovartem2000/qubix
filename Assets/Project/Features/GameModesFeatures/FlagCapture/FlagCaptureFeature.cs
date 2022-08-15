using ME.ECS;
using ME.ECS.Collections;
using Project.Common.Utilities;

namespace Project.Features.GameModesFeatures.FlagCapture
{
    #region usage
    using Components;
    using Modules;
    using Systems;
    using Features;
    using Markers;
    using FlagCapture.Components;
    using FlagCapture.Modules;
    using FlagCapture.Systems;
    using FlagCapture.Markers;
    using ME.ECS.Views.Providers;
    using Project.Common.Components;

    namespace FlagCapture.Components { }
    namespace FlagCapture.Modules { }
    namespace FlagCapture.Systems { }
    namespace FlagCapture.Markers { }

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class FlagCaptureFeature : Feature
    {
        public MonoBehaviourView Flag;
        public MonoBehaviourView PlayerFlag;

        private ViewId _flagId, _playerFlagId;
        
        protected override void OnConstruct()
        {
            if (!world.HasSharedData<FlagCaptureMode>()) return;
            
            _flagId = world.RegisterViewSource(Flag);
            _playerFlagId = world.RegisterViewSource(PlayerFlag);
            
            AddSystem<FlagSpawnSystem>();
            AddSystem<SettingFlagSystem>();
            AddSystem<CatchFlagSystem>();
            AddSystem<DropFlagSystem>();
            AddSystem<FlagReturnSystem>();
            AddSystem<FlagEndGameSystem>();
            AddSystem<FlagCountSystem>();
        }

        protected override void OnConstructLate()
        {
            if (!world.HasSharedData<FlagCaptureMode>()) return;

            SpawnStartFlags();
        }

        protected override void OnDeconstruct() { }
        
        private void SpawnStartFlags()
        {
            var score = new DictionaryCopyable<int, int>();
            
            for (var i = 0; i < GameConsts.GameModes.FlagCapture.TEAM_FLAG_COUNT; i++)
            {
                score.Add(i + 1, 0);
                CreateFlagRespawnRequest(i + 1, 1f);
            }
            
            world.SetSharedData(new CapturedFlagsScore { Score = score });
        }

        public Entity SpawnFlag(int team)
        {
            var entity = new Entity("Flag");
            entity.Set(new FlagTag());
            entity.Set(new CollisionDynamic());
            entity.Set(new TeamTag { Value = team } );
            entity.InstantiateView(_flagId);

            return entity;
        }
        
        public void CreateFlagRespawnRequest(int team, float spawnDelay = 0f)
        {
            var entity = new Entity("RespawnFlag");
            entity.Set(new FlagNeedRespawn { SpawnDelay = spawnDelay });
            entity.Set(new TeamTag { Value = team } );
            //TODO: Replace to oneshot entity
        }
        
        public Entity SpawnFlagOnPlayer()
        {
            var entity = new Entity("FlagOnPlayer");
            entity.InstantiateView(_playerFlagId);
            return entity;
        }
        
        public void UpdateFlagScore(int team)
        {
            var entity = new Entity("UpdateFlagScore");
            entity.Set(new FlagCaptured { Team = team });
            //TODO: Replace to oneshot entity
        }
    }
}