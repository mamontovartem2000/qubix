using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpearTest : MonoBehaviour
{
    [SerializeField] private Material _spearChainMateial;

    private void Update() { if (Input.GetKeyDown(KeyCode.Space)) { Tween(); } }

    private void Tween()
    {
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(_spearChainMateial.DOOffset(new Vector2(0, 0.7f), 0.1f))
            .AppendInterval(0)
            .Append(_spearChainMateial.DOOffset(new Vector2(0, 1), 0.8f));
    }
}
