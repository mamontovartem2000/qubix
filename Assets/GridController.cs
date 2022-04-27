using System;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    [SerializeField] private bool _left;
    [SerializeField] private GameObject _statusPrefab;
    
    private GridLayoutGroup _group;
    private void Awake()
    {
        _group = GetComponent<GridLayoutGroup>();
        var size = GetComponent<RectTransform>().sizeDelta.y;

        _group.cellSize = new Vector2(size, size);
        _group.childAlignment = _left ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight;
    }

    public void SpawnStatusEffect()
    {
        Instantiate(_statusPrefab, this.transform);
    }
}
