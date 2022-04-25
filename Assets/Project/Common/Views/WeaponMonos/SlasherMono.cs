using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views.WeaponMonos
{
    public class SlasherMono : MonoBehaviourView
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
            if (entity.Has<LeftWeaponShot>())
            {
                _anim.SetBool("Attack", true);
                Invoke(nameof(EndAnimation), 1f);
            }
        }

        private void EndAnimation()
        {
            //TODO: Added if (entity != null). Maybe change to event and delete Invoke
            if (!entity.IsAlive() && !entity.Has<LeftWeaponShot>())
                _anim.SetBool("Attack", false);
        }
    }
}