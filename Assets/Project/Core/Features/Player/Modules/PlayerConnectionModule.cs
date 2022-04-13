using ME.ECS;
using Project.Markers;

namespace Project.Core.Features.Player.Modules
{
    #region usage
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
            
            if (world.GetMarker(out NetworkPlayerDisconnected npd))
            {
                _feature.OnLocalPlayerDisconnected(npd.ActorID);
            }

            if (world.GetMarker(out NetworkPlayerConnectedTimeSynced npcts))
            {
                _feature.OnGameStarted(npcts.ActorID);
            }
        }
    }
}
