using ME.ECS;
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
    using Project.Common.Components;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class DropFlagSystem : ISystemFilter
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
            return Filter.Create("Filter-DropFlagSystem")
                .With<PlayerDead>()
                .With<CarriesTheFlag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            Debug.Log("ssdaefwe");
            Entity flag = _feature.SpawnFlag(entity.Read<CarriesTheFlag>().Team);
            var pos = entity.Read<PlayerMoveTarget>().Value;
            flag.SetPosition(pos);
            SceneUtils.ModifyFree(pos, false);
        }
    }
}