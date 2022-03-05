﻿using ME.ECS;
using Project.Features.Player.Components;
using Project.Utilities;
using UnityEngine;

namespace Project.Features.Player.Views {
    
    using ME.ECS.Views.Providers;
    
    public class PlayerView : MonoBehaviourView
    {
        public Renderer[] Parts;
        public GameObject _RightWeapon;
        
        public override bool applyStateJob => true;

        public override void OnInitialize()
        {
            foreach (var part in Parts)
            {
                part.sharedMaterial = entity.Read<PlayerTag>().Material;
            }
        }

        public override void OnDeInitialize() {}

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
            _RightWeapon.SetActive(entity.Has<RightWeapon>());
        }
    }
}