using DG.Tweening;
using UnityEngine;

namespace Project.Visuals.Scripts
{
    public class QubixMoveTween : MonoBehaviour
    {
        [SerializeField] private Transform _body;

        private void Start()
        {
            TweenUp();
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
