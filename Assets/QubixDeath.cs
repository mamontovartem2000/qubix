using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QubixDeath : MonoBehaviour
{
    [SerializeField] private Material _material;
    private void Start()
    {
        _material.DOFloat(1, "DissolveAmount_", 5f);
    }
}
