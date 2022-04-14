using System;
using System.IO;
using UnityEngine;
public class testmapSerializer : MonoBehaviour
{
    private Transform[] _tiles;
    private string[] _stringPositions;
    [SerializeField]private Vector3[] _results;

    private void Awake()
    {
        var count = transform.childCount;
        
        _tiles = new Transform[count];
        _stringPositions = new string[count];
        _results = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            _tiles[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < _tiles.Length; i++)
            {
                _stringPositions[i] = JsonUtility.ToJson(new TilePosition(_tiles[i].position));
                _results[i] = _tiles[i].position;

                //File.Create(Application.dataPath + "\\map.txt");
                File.WriteAllLines(Application.dataPath + "\\Resources\\map.txt", _stringPositions);
            }
        }
    }

    [Serializable]
    public class TilePosition
    {
        public Vector3 Position;

        public TilePosition(Vector3 p)
        {
            Position = p;
        }
    }
}