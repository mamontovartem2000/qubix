using ME.ECS;
using ME.ECS.Collections;
using ME.ECS.DataConfigs;
using Project.Common.Utilities;
using UnityEngine;

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
        public MonoBehaviourView StandingFlag_Red, StandingFlag_Blue, PlayerFlag_Red, PlayerFlag_Blue ;
        public DataConfig FlagBearerFirstStage, FlagBearerSecondStage, RemoveFlagBearer;

        [HideInInspector] public int FirstCapturedFlag;
        private ViewId _flagRedId, _flagBlueId, _playerFlagRedId, _playerFlagBlueId;
        
        protected override void OnConstruct()
        {
            if (!world.HasSharedData<FlagCaptureMode>()) return;
            
            _flagBlueId = world.RegisterViewSource(StandingFlag_Blue);
            _flagRedId = world.RegisterViewSource(StandingFlag_Red);
            _playerFlagBlueId = world.RegisterViewSource(PlayerFlag_Blue);
            _playerFlagRedId = world.RegisterViewSource(PlayerFlag_Red);
            
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
                CreateFlagRespawnRequest(i + 1, 0f);
            }
            
            world.SetSharedData(new CapturedFlagsScore { Score = score });
        }

        public Entity SpawnFlag(int team)
        {
            var entity = new Entity("Flag");
            entity.Set(new FlagTag());
            entity.Set(new CollisionDynamic());
            entity.Set(new TeamTag { Value = team } );
            
            if (team == 1)
                entity.InstantiateView(_flagRedId);
            else if (team == 2)
                entity.InstantiateView(_flagBlueId);

            return entity;
        }
        
        public void CreateFlagRespawnRequest(int team, float spawnDelay = GameConsts.GameModes.FlagCapture.FLAG_RESPAWN_TIME)
        {
            var entity = new Entity("RespawnFlag");
            entity.Set(new FlagNeedRespawn { SpawnDelay = spawnDelay });
            entity.Set(new TeamTag { Value = team } );
            //TODO: Replace to oneshot entity
        }
        
        public Entity SpawnFlagOnPlayer(int team)
        {
            var entity = new Entity("FlagOnPlayer");
            
            if (team == 1)
                entity.InstantiateView(_playerFlagRedId);
            else if (team == 2)
                entity.InstantiateView(_playerFlagBlueId);
            
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