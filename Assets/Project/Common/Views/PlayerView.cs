using System;
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

        public override void OnInitialize()
        {
        }

        public override void OnDeInitialize()
        {
        }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
            
            var intensity = 6 * (entity.Read<PlayerHealthDefault>().Value - entity.Read<PlayerHealth>().Value) /
                            entity.Read<PlayerHealthDefault>().Value;

            foreach (var rend in Rends)
            {
                rend.material.SetColor("_EmissionColor", Color * intensity);
            }
        }
    }
}