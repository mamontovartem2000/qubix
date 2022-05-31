using DG.Tweening;
using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Features.Player.Views
{
    public class PlayerView : MonoBehaviourView
    {
        public MeshRenderer[] Rends;
        
        private bool _swap = true;

        public override void OnInitialize() {}
        public override void OnDeInitialize() { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();

            if (entity.Has<PlayerDamaged>())
            {
                foreach (var rend in Rends)
                {
                    rend.materials[0].EnableKeyword("_EMISSION");
                }
            }
            else
            {
                foreach (var rend in Rends)
                {
                    rend.materials[0].DisableKeyword("_EMISSION");
                }
                
                _swap = true;
            }
        }
    }
}