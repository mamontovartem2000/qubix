using ME.ECS;
using ME.ECS.Collections;
using Project.Common.Components;
using Project.Common.Utilities;
using UnityEngine;

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

    public sealed class FlagCountSystem : ISystemFilter
    {
        private FlagCaptureFeature feature;

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
            return Filter.Create("Filter-FlagCountSystem")
                .With<FlagCaptured>()
                .WithoutShared<GameFinished>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var team = entity.Read<FlagCaptured>().Team;
            entity.Destroy();
            
            if (world.HasSharedData<MatchPoint>())
            {
                Debug.Log("Win in MatchPoint");
                world.SetSharedData(new WinningTeam {Team = team}, ComponentLifetime.NotifyAllSystems);
                return;
            }

            ref var score = ref world.GetSharedData<CapturedFlagsScore>().Score;

            if (score[1] == 0 && score[2] == 0)
            {
                feature.FirstCapturedFlag = team;
            }
            
            score[team] += 1;

            FlagEvents.UpdateFlagScore(score[1], score[2]);

            var winTeam = GetWinningTeamByFlagCount(score);
            
            if (winTeam == 0) return;
            
            world.SetSharedData(new WinningTeam { Team = winTeam }, ComponentLifetime.NotifyAllSystems);
        }
        
        private int GetWinningTeamByFlagCount(DictionaryCopyable<int, int> score)
        {
            var winCount = GameConsts.GameModes.FlagCapture.WIN_FLAG_COUNT;

            if (score[1] == winCount)
            {
                Debug.Log($"Team 1 win. {winCount} flags collected");
                return 1;
            }
            else if (score[2] == winCount)
            {
                Debug.Log($"Team 2 win. {winCount} flags collected");
                return 2;
            }

            return 0;
        }
    }
}