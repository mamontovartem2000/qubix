using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Core.Features.Player.Views
{
    public class PlayerView : MonoBehaviourView
    {
        public Renderer[] Parts;

        public override bool applyStateJob => true;

        public override void OnInitialize()
        {
            foreach (var part in Parts)
            {
                //part.sharedMaterial = entity.Read<PlayerMaterial>().Material;
            }
        }

        public override void OnDeInitialize() {}

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();                
        }
    }
}