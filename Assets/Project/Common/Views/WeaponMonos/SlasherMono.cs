using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views.WeaponMonos
{
    public class SlasherMono : MonoBehaviourView
    {
        [SerializeField] private Animator _anim;

        private bool _isAttacking;
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