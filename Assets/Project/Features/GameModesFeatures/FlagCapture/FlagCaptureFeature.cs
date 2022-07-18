using ME.ECS;
using Project.Common.Utilities;

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
        private ViewId _flagId;

        protected override void OnConstruct()
        {
            _flagId = world.RegisterViewSource(Flag);

            AddSystem<FlagSpawnSystem>();
            AddSystem<CatchFlagSystem>();
            AddSystem<DropFlagSystem>();
            AddSystem<FlagReturnSystem>();
        }

        protected override void OnConstructLate() => SpawnStartFlags();

        private void SpawnStartFlags()
        {
            for (int i = 0; i < Consts.GameModes.FlagCapture.FLAG_COUNT; i++)
            {
                var flag = SpawnFlag(i + 1);
                flag.Set(new FlagNeedRespawn(), ComponentLifetime.NotifyAllSystems);
            }
        }

        protected override void OnDeconstruct() { }

        public Entity SpawnFlag(int team)
        {
            var entity = new Entity("Flag");
            entity.Set(new FlagTag());
            entity.Set(new CollisionDynamic());
            entity.Set(new TeamTag { Value = team } );
            entity.InstantiateView(_flagId);

            return entity;
        }
    }
}