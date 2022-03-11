using ME.ECS;
using UnityEngine.Serialization;

namespace Project.Features {
    #region usage

    

    using Components; using Modules; using Systems; using Features; using Markers;
    using Events.Components; using Events.Modules; using Events.Systems; using Events.Markers;
    
    namespace Events.Components {}
    namespace Events.Modules {}
    namespace Events.Systems {}
    namespace Events.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class EventsFeature : Feature
    {
        // public GlobalEvent PassLocalPlayer;
        public GlobalEvent OnTimeSynced;
        public GlobalEvent HealthChanged;
        public GlobalEvent AllPlayersReady;
        public GlobalEvent OnGameStarted;
        
        
        public GlobalEvent Defeat;
        public GlobalEvent Victory;
        public GlobalEvent OnGameFinished;
        public GlobalEvent Respawn;
        public GlobalEvent Kill;

        public GlobalEvent leftWeaponFired;
        public GlobalEvent rightWeaponFired;
        public GlobalEvent LeftweaponDepleted;
        public GlobalEvent RightWeaponDepleted;
            
        protected override void OnConstruct() {}
        protected override void OnDeconstruct() {}
    }
}