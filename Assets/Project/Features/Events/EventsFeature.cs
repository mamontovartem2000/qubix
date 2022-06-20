using ME.ECS;

namespace Project.Core.Features.Events {
    #region usage
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class EventsFeature : Feature
    {
        // public GlobalEvent PassLocalPlayer;
        public GlobalEvent HealthChanged;
        public GlobalEvent AllPlayersReady;
        public GlobalEvent OnGameStarted;
        public GlobalEvent PassLocalPlayer;

        public GlobalEvent TimerTick;
        
        public GlobalEvent Defeat;
        public GlobalEvent Victory;
        
        public GlobalEvent OnGameFinished;
        public GlobalEvent PlayerDeath;
        public GlobalEvent PlayerKill;

        public GlobalEvent leftWeaponFired;
        public GlobalEvent rightWeaponFired;
        public GlobalEvent LeftweaponDepleted;
        public GlobalEvent RightWeaponDepleted;

        public GlobalEvent CooldownTick;
        public GlobalEvent SkillImageChange;

        public GlobalEvent TabulationOn;
        public GlobalEvent TabulationOff;
        public GlobalEvent TabulationAddPlayer;
        public GlobalEvent TabulationScreenNumbersChanged;
        public GlobalEvent TabulationScreenNewPlayerStats;
            
        protected override void OnConstruct() {}
        protected override void OnDeconstruct() {}
    }
}