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
        // ChangeMap(Maps.Coliseum);
    }

    public void ChangeMap(Maps map)
    {
        switch (map)
        {
            case Maps.Black:
                ChangeColor(29, 15, 0);
                ChangeEmissionColor(7, 7, 1);
                ExtraChangeColors(1, 2, 3, 4, 5);
                ExtraChangeEmissionColors(7, 7, 6, 5, 4, 3);
                break;
            case Maps.Coliseum:
                ChangeColor(0, 15, 29);
                ChangeEmissionColor(7, 7, 1);
                ExtraChangeColors(29, 29, 29, 29, 29);
                ExtraChangeEmissionColors(7, 7, 6, 5, 4, 3);
                break;
            case Maps.Forest:
                ChangeColor(13, 22, 14);
                ChangeEmissionColor(3, 2, 3);
                ExtraChangeColors(13, 13, 13, 13, 13);
                ExtraChangeEmissionColors(3, 3, 3, 3, 3, 3);
                break;
            case Maps.Neon:
                ChangeColor(23, 19, 19);
                ChangeEmissionColor(1, 6, 6);
                ExtraChangeColors(20, 20, 4, 19, 30);
                ExtraChangeEmissionColors(5, 5, 5, 5, 5, 5);
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
        foreach (var item in _mainFloors) { item.SetColor("_EmissionColor", _emissionColorPalletes[floorColor] * 7); }
        foreach (var item in _props) { item.SetColor("_EmissionColor", _emissionColorPalletes[propColor] * 7); }
        foreach (var item in _walls) { item.SetColor("_EmissionColor", _emissionColorPalletes[wallColor] * 7); }
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
    private void ExtraChangeEmissionColors(int o, int a, int b, int c, int d, int e)
    {
        _extraObjects[0].SetColor("_EmissionColor", _emissionColorPalletes[o] * 7);
        _extraObjects[1].SetColor("_EmissionColor", _emissionColorPalletes[a] * 7);
        _extraObjects[2].SetColor("_EmissionColor", _emissionColorPalletes[b] * 7);
        _extraObjects[3].SetColor("_EmissionColor", _emissionColorPalletes[c] * 7);
        _extraObjects[4].SetColor("_EmissionColor", _emissionColorPalletes[d] * 7);
        _extraObjects[5].SetColor("_EmissionColor", _emissionColorPalletes[e] * 7);
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