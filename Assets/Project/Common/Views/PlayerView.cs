using DG.Tweening;
using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace Project.Core.Features.Player.Views
{
    public class PlayerView : MonoBehaviourView
    {
        // [SerializeField] private Image _healthBar;
        [SerializeField] private Transform _body;

        public override bool applyStateJob => true;

        public override void OnInitialize()
        {
            // TweenUp();
        }
        public override void OnDeInitialize() { }
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
        }
        
        private void TweenUp()
        {
            _body.DOMoveY(0.1f, 0.5f).OnComplete(() => { TweenDown(); });
        }

        private void TweenDown()
        {
            _body.DOMoveY(0f, 0.5f).OnComplete(() => { TweenUp(); });
        }
    }
}