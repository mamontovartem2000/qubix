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
        private const int FlagCount = 2;
        public MonoBehaviourView Flag;
        public ViewId FlagId;

        protected override void OnConstruct()
        {
            FlagId = world.RegisterViewSource(Flag);

            AddSystem<FlagSpawnSystem>();
            AddSystem<CatchFlagSystem>();
            AddSystem<DropFlagSystem>();
        }

        protected override void OnConstructLate() => SpawnStartFlags();

        private void SpawnStartFlags()
        {
            for (int i = 0; i < FlagCount; i++)
            {
                var flag = SpawnFlag(i + 1);
                flag.Set<FlagNeedRespawn>();
            }
        }

        protected override void OnDeconstruct() { }

        public Entity SpawnFlag(int team)
        {
            var entity = new Entity("Flag");
            entity.Set(new FlagTag());
            entity.Set(new CollisionDynamic());
            entity.Set(new TeamTag { Value = team } );
            entity.InstantiateView(FlagId);

            return entity;
        }
    }
}