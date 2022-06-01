using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views
{
    public class PlayerView : MonoBehaviourView
    {
        public MeshRenderer[] Rends;
        public Color Color;
        
        private bool _swap = true;

        public override void OnInitialize() {}
        public override void OnDeInitialize() { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();

            var current = entity.Read<PlayerHealth>().Value;
            var max = entity.Read<PlayerHealthDefault>().Value;

            var intencity = max / current * 1.25f;

            if (entity.Has<PlayerDamaged>())
            {
                foreach (var rend in Rends)
                {
                    rend.materials[0].EnableKeyword("_EMISSION");
                    rend.material.SetColor("_EmissionColor", Color * intencity);
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