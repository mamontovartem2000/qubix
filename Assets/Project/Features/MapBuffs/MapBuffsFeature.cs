using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Features.MapBuffs.Systems.PowerUp;
using UnityEngine;

namespace Project.Features.MapBuffs
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class MapBuffsFeature : Feature
    {
        public MonoBehaviourViewBase PowerUpCrystal;
        public DataConfig PowerUp, RemovePowerUp;

        [HideInInspector] public ViewId PowerUpView;
        
        protected override void OnConstruct()
        {
            InitAllViews();

            AddSystem<PowerUpReturnFromDeadPlayerSystem>();
            AddSystem<PowerUpTimerSystem>();
            AddSystem<PowerUpCrystalSpawnSystem>();
            AddSystem<PowerUpCatch>();

            CreatePowerUpCrystalRespawnRequest(); //TODO: Need count tiles on map and spawn in cycle
        }

        private void InitAllViews()
        {
            PowerUpView = world.RegisterViewSource(PowerUpCrystal);
        }

        protected override void OnDeconstruct() { }

        public void CreatePowerUpCrystalRespawnRequest()
        {
            var entity = new Entity("PowerUpRespawnRequest");
            entity.Set(new PowerUpNeedRespawn());
            //TODO: Replace to oneshot entity
        }
    }
}