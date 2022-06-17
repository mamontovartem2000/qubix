using UnityEngine;

namespace Project.Visuals.Scripts
{
    public class TestMercanie : MonoBehaviour
    {
        [SerializeField] private Material _mat;
        [SerializeField] private float _emissionIntensity;

        private bool _minus;


        private void Update()
        {
            _mat.SetColor("_EmissionColor", _mat.color * _emissionIntensity);
            ChangeIntensity();
        }

        private void ChangeIntensity()
        {
            if (!_minus)
            {
                _emissionIntensity += 4 * Time.deltaTime;
                if (_emissionIntensity > 3)
                {
                    _minus = true;
                }
            }
            else
            {
                _emissionIntensity -= 4 * Time.deltaTime;
                if (_emissionIntensity < 2.6)
                {
                    _minus = false;
                }
            }
        }
    }
}

