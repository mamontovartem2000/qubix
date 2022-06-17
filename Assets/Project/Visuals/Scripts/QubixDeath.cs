using DG.Tweening;
using UnityEngine;

namespace Project.Visuals.Scripts
{
    public class QubixDeath : MonoBehaviour
    {
        [SerializeField] private Material _material;
        private void Start()
        {
            _material.DOFloat(1, "DissolveAmount_", 5f);
        }
    }
}
