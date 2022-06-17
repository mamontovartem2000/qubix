﻿using ME.ECS;
using ME.ECS.Views.Providers;

namespace Project.Visuals.Scripts.HeadAttach {
    public class CrownMono : MonoBehaviourView {
        
        public override bool applyStateJob => true;
        public override void OnInitialize() { }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
        }  
    }
    
}