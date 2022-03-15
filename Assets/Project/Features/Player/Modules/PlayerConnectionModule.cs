﻿using ME.ECS;
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
    
        private PlayerFeature _feature;

        void IModuleBase.OnConstruct() 
        {
            world.GetFeature(out _feature);
        }
        
        void IModuleBase.OnDeconstruct() {}

        void IUpdate.Update(in float deltaTime)
        {
            if (world.GetMarker(out NetworkSetActivePlayer nsap))
            {
                _feature.OnLocalPlayerConnected(nsap.ActorID);
            }
            
            if (world.GetMarker(out NetworkPlayerConnectedTimeSynced npc))
            {
                _feature.OnGameStarted();
            }
            
            if (world.GetMarker(out NetworkPlayerDisconnected npd))
            {
                _feature.OnLocalPlayerDisconnected(npd.ActorID);
            }

            // if (world.GetMarker(out SelectColorMarker scm))
            // {
            //     _feature.OnSelectColor(scm.ActorID, scm.ColorID);
            // }

            // if (world.GetMarker(out PlayerReadyMarker prm))
            // {
            //     _feature.OnPlayerReady(prm.ActorID);
            // }

            // if (world.GetMarker(out GameStartedMarker gsm))
            // {
            //     _feature.OnGameStartedComplete();
            // }
        }
    }
}
