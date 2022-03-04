using ME.ECS;
using Photon.Pun;
using Project.Markers;
using UnityEngine;

namespace Project.Features.Player.Modules {
    #region usage

    

    using Components; using Modules; using Systems; using Features; using Markers;
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class PlayerConnectionModule : IModule, IUpdate 
    {
        public World world { get; set; }
    
        private PlayerFeature feature;

        void IModuleBase.OnConstruct() 
        {
            this.feature = this.world.GetFeature<PlayerFeature>();
        }
        
        void IModuleBase.OnDeconstruct() {}

        void IUpdate.Update(in float deltaTime)
        {
            if (world.GetMarker(out NetworkSetActivePlayer nsap))
            {
                feature.OnLocalPlayerConnected(nsap.Player.ActorNumber);
            }
            
            if (world.GetMarker(out NetworkPlayerConnectedTimeSynced npc))
            {
                feature.OnGameStarted(npc.ActorID);
            }

            if (world.GetMarker(out NetworkPlayerDisconnected npd))
            {
                feature.OnLocalPlayerDisconnected(npd.Player.ActorNumber);
            }
        }
    }
}
