using UnityEngine;

public class MapChanger : MonoBehaviour
{
    public static MapChanger Changer;

    [SerializeField] private Material[] _mainFloors;
    [SerializeField] private Material[] _extraObjects;
    [SerializeField] private Material[] _props;
    [SerializeField] private Material[] _walls;
    [SerializeField] private Color[] _colorPalletes;
    [SerializeField] private Color[] _emissionColorPalletes;

    private void Awake()
    {
        Changer = this;
        // ChangeMap(Maps.Neon);
    }

    public void ChangeMap(Maps map)
    {
        switch (map)
        {
            case Maps.Black:
                ChangeColor(28, 15, 0);
                ChangeEmissionColor(7, 7, 1);
                ExtraChangeColors(1, 2, 3, 4, 5);
                ExtraChangeEmissionColors(7, 6, 5, 4, 3);
                break;
            case Maps.Coliseum:
                ChangeColor(7, 22, 7);
                ChangeEmissionColor(0, 4, 0);
                ExtraChangeColors(8, 7, 6, 5, 4);
                ExtraChangeEmissionColors(0, 1, 2, 3, 4);
                break;
            case Maps.Forest:
                ChangeColor(13, 22, 14);
                ChangeEmissionColor(3, 2, 3);
                ExtraChangeColors(13, 13, 13, 13, 13);
                ExtraChangeEmissionColors(3, 3, 3, 3, 3);
                break;
            case Maps.Neon:
                ChangeColor(23, 19, 19);
                ChangeEmissionColor(1, 6, 6);
                ExtraChangeColors(20, 20, 4, 19, 30);
                ExtraChangeEmissionColors(1, 1, 1, 1, 1);
                break;
        }
    }

    private void ChangeColor(int floorColor, int propColor, int wallColor)
    {
        foreach (var item in _mainFloors) { item.SetColor("_BaseColor", _colorPalletes[floorColor]); }
        foreach (var item in _props) { item.SetColor("_BaseColor", _colorPalletes[propColor]); }
        foreach (var item in _walls) { item.SetColor("_BaseColor", _colorPalletes[wallColor]); }
    }

    private void ChangeEmissionColor(int floorColor, int propColor, int wallColor)
    {
        foreach (var item in _mainFloors) { item.SetColor("_EmissionColor", _emissionColorPalletes[floorColor] * 5); }
        foreach (var item in _props) { item.SetColor("_EmissionColor", _emissionColorPalletes[propColor] * 5); }
        foreach (var item in _walls) { item.SetColor("_EmissionColor", _emissionColorPalletes[wallColor] * 5); }
    }

    private void ExtraChangeColors(int a, int b, int c, int d, int e)
    {
        _extraObjects[0].SetColor("_BaseColor", _colorPalletes[a - 1]); //bridge
        _extraObjects[1].SetColor("_BaseColor", _colorPalletes[a]);
        _extraObjects[2].SetColor("_BaseColor", _colorPalletes[b]);
        _extraObjects[3].SetColor("_BaseColor", _colorPalletes[c]);
        _extraObjects[4].SetColor("_BaseColor", _colorPalletes[d]);
        _extraObjects[5].SetColor("_BaseColor", _colorPalletes[e]);
    }
    private void ExtraChangeEmissionColors(int a, int b, int c, int d, int e)
    {
        _extraObjects[1].SetColor("_EmissionColor", _emissionColorPalletes[a] * 15);
        _extraObjects[2].SetColor("_EmissionColor", _emissionColorPalletes[b] * 15);
        _extraObjects[3].SetColor("_EmissionColor", _emissionColorPalletes[c] * 15);
        _extraObjects[4].SetColor("_EmissionColor", _emissionColorPalletes[d] * 15);
        _extraObjects[5].SetColor("_EmissionColor", _emissionColorPalletes[e] * 15);
    }
}

[System.Serializable]
public enum Maps
{
    Black = 0,
    Coliseum = 1,
    Forest = 2,
    Neon = 3,
}