using ME.ECS;
using Project.Common.Components;
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

    public sealed class FlagEndGameSystem : ISystem, IAdvanceTick, IUpdate
    {
        private FlagCaptureFeature feature;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this.feature);
        }

        void ISystemBase.OnDeconstruct() { }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            Debug.Log("asdfsdef");
            if (!world.HasSharedData<EndOfGameStage>()) return;
            
            var stage = world.ReadSharedData<EndOfGameStage>().StageNumber;
            var winTeam = GetWinTeamNumber();
            
            if (stage == 1)
            {
                
            }
        }

        void IUpdate.Update(in float deltaTime) { }


        private int GetWinTeamNumber()
        {
            var score = world.ReadSharedData<CapturedFlagsScore>().Score;
            
            if (score[1] == score[2])
            {
                Debug.Log("Draw");
                return 0;
            }
            else if (score[1] > score[2])
            {
                Debug.Log("First win");
                return 1;
            }
            else
            {
                Debug.Log("Second win");
                return 2;
            }
        }
    }
}