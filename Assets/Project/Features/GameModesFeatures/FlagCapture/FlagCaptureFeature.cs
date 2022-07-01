using ME.ECS;

namespace Project.Features.GameModesFeatures
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
        public ViewId FlagId;

        protected override void OnConstruct()
        {
            FlagId = world.RegisterViewSource(Flag);

            AddSystem<FlagRespawn>();
            AddSystem<DropFlagSystem>();

        }

        protected override void OnDeconstruct()
        {

        }

    }

}