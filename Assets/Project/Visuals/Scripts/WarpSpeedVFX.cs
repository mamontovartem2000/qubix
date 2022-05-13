using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WarpSpeedVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect _warpSpeedVFX;
    [SerializeField] private MeshRenderer _cylinderVFX;
    [SerializeField] private float _rate = 0.02f;
    [SerializeField] private float _delay;

    private bool _warpActive;

    private void Start()
    {
        _warpSpeedVFX.Stop();
        _warpSpeedVFX.SetFloat("WarpAmount", 0);
        _cylinderVFX.material.SetFloat("Active_",0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _warpActive = true;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _warpActive = false;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }
    }

    IEnumerator ActivateParticles()
    {
        float amount = _warpSpeedVFX.GetFloat("WarpAmount");
        if (_warpActive)
        {
            _warpSpeedVFX.Play();
            while (amount < 1 & _warpActive)
            {
                amount += _rate;
                _warpSpeedVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            while (amount > 0 & !_warpActive)
            {
                amount -= _rate;
                _warpSpeedVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.1f);

                if (amount <= 0 + _rate)
                {
                    amount = 0;
                    _warpSpeedVFX.SetFloat("WapAmount", amount);
                    _warpSpeedVFX.Stop();
                }
            }
        }
    }

    IEnumerator ActivateShader()
    {     
        float amount = _cylinderVFX.material.GetFloat("Active_");
        if (_warpActive)
        {
            yield return new WaitForSeconds(_delay);
            while (amount < 1 & _warpActive)
            {
                amount += _rate;
                _cylinderVFX.material.SetFloat("Active_", amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            while (amount > 0 & !_warpActive)
            {
                amount -= _rate;
                _cylinderVFX.material.SetFloat("Active_", amount);
                yield return new WaitForSeconds(0.1f);

                if (amount <= 0 + _rate)
                {
                    amount = 0;
                    _cylinderVFX.material.SetFloat("Active_", amount);
                }
            }
        }
    }
}
