using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    [SerializeField] private TextMesh _textMesh;
    private const int _defaultNumber = 3;
    private int _number = 3;

    private void OnEnable()
    {
        _number = _defaultNumber;
        _textMesh.text = _number.ToString();
    }

    public void ChangeNumber()
    {
        _number -= 1;
        _textMesh.text = _number.ToString();
    }
}
