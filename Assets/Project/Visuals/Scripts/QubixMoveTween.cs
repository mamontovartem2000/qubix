using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
