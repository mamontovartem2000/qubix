using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectScaler : MonoBehaviour
{
    public enum Orientation
    {
        Horizontal, Vertical
    }

    private RectTransform _rect;

    [SerializeField] private bool _runtime;
    [SerializeField] private RectTransform _parent;
    [SerializeField] private Orientation _orient;
    [SerializeField] private float _scale;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();

        Rescale();
    }

    private void Update()
    {
        if (_runtime)
        {
            Rescale();
        }
    }

    private void Rescale()
    {
        switch (_orient)
        {
            case Orientation.Horizontal:
                {
                    _rect.sizeDelta = new Vector2(_parent.sizeDelta.x, _parent.sizeDelta.x) * _scale;
                    _rect.anchoredPosition = Vector2.zero;
                    break;
                }
            case Orientation.Vertical:
                {
                    _rect.sizeDelta = new Vector2(_parent.sizeDelta.y, _parent.sizeDelta.y) * _scale;
                    break;
                }
        }
    }
}
