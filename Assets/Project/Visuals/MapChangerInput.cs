using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangerInput : MonoBehaviour
{
    [SerializeField] private MapChanger _mapChanger;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _mapChanger.Maps = Maps.Black;
            _mapChanger.ChangeMap();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _mapChanger.Maps = Maps.Coliseum;
            _mapChanger.ChangeMap();
        }
    }
}
