using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactColorChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] _particlesTest;
    [SerializeField] private Vector3 _targerTransformPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(_particlesTest[0], _targerTransformPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(_particlesTest[1], _targerTransformPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(_particlesTest[2], _targerTransformPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(_particlesTest[3], _targerTransformPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(_particlesTest[4], _targerTransformPos, Quaternion.identity);
        }
    }
}
