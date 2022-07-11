using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    [SerializeField] private TextMesh _textMesh;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private GameObject _lomixBomb;
    private int _number = 3;
    
    public void ChangeNumber()
    {
        _number -= 1;
        _textMesh.text = _number.ToString();
    }

    public void Explosion()
    {
        _vfx.SetActive(true);
        _lomixBomb.SetActive(false);
    }
}
