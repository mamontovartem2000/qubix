using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSet : MonoBehaviour
{
    [SerializeField] private Material _material;

    private float _offSet;
    private void Update()
    {
        _offSet += 0.2f * Time.deltaTime;
        _material.SetTextureOffset("_BaseMap", new Vector2(0, _offSet));
    }

}
