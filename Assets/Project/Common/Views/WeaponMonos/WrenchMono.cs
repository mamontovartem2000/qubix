using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Assets.Dima.Scripts
{
    using ME.ECS.Views.Providers;

    public class WrenchMono : MonoBehaviourView
    {
        [SerializeField] private Animator _anim; 
        
        public override bool applyStateJob => true;
        public override void OnInitialize() { }
        public override void OnDeInitialize() { }
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetParent().GetRotation();

            if (!entity.IsAlive()) return;
            _anim.SetBool("Attack", entity.Has<LeftWeaponShot>());
        }
    }
}