using ME.ECS;

namespace Project.Common.Events
{
    #region usage

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    #endregion

    public sealed class EventsFeature : Feature
    {
        public GlobalEvent HealthChanged;
        public GlobalEvent AllPlayersReady;
        public GlobalEvent OnGameStarted;
        public GlobalEvent PassLocalPlayer;

        //UI control
        public GlobalEvent TimerTick;
        public GlobalEvent Screenshot;

        //Match result 
        public GlobalEvent Defeat;
        public GlobalEvent Victory;
        public GlobalEvent OnGameFinished;
        
        //Player death
        public GlobalEvent PlayerDeath;
        public GlobalEvent PlayerKill;
        
        //Weapon control
        public GlobalEvent leftWeaponFired;
        public GlobalEvent rightWeaponFired;
        public GlobalEvent LeftweaponDepleted;
        public GlobalEvent RightWeaponDepleted;

        //Skills UI  
        public GlobalEvent CooldownTick;
        public GlobalEvent SkillImageChange;

        //Tab 
        public GlobalEvent TabulationAddPlayer;
        public GlobalEvent TabulationScreenNumbersChanged;

        //EMP UI effects
        public GlobalEvent EMPActive;
        public GlobalEvent EMPInactive;

        //Sound control
        public GlobalEvent PlaySound;
        public GlobalEvent PlaySoundPrivate;

        protected override void OnConstruct() { }
        protected override void OnDeconstruct() { }
    }
}