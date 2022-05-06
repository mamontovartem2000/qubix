using ME.ECS;
using Project.Common.Views;
using UnityEngine;

namespace Project.Core.Features.SceneBuilder.Components {

    public struct MapConstruct : IComponent 
    {
        public TextAsset _sourceMap;
    }

    public struct DefaultComponents : IComponent
    {
        public TeleportParticle PortalView;
        public MineParticle MineView;
        public HealthParticle HealthView;
    }

}