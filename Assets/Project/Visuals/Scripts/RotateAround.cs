using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, -1000 * Time.deltaTime);
    }
}
