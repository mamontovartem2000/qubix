using Project.Modules.Network;
using UnityEngine;

public class MapChanger : MonoBehaviour
{
    public static MapChanger Changer;

    [SerializeField] private Material[] _mainFloors;
    [SerializeField] private Material[] _extraObjects;
    [SerializeField] private Material[] _props;
    [SerializeField] private Material[] _walls;
    [SerializeField] private Material _bridge;
    [SerializeField] private Material _triangleTile;
    [SerializeField] private Color[] _colorPalletes;
    [SerializeField] private Color[] _emissionColorPalletes;
    [SerializeField] private Color[] _bridgesColor;
    private float _emissionIntensity;

    private void Awake()
    {
        Changer = this;
        if (NetworkData.Info != null)
            ChangeMap((Maps)NetworkData.Info.map_id);
        else
            ChangeMap(Maps.Colizei); // For Fake Connect
        // ChangeMap(Maps.Colizei);
    }

    public void ChangeMap(Maps map)
    {
        switch (map)
        {
            case Maps.Colizei:
                _emissionIntensity = 3;
                ChangeColor(20, 15, 0);
                ChangeEmissionColor(4, 4, 4);
                ExtraChangeColors(19, 19, 30, 4, 5);
                ExtraChangeEmissionColors(4, 6, 5, 4, 4);
                ChangeBridge(0, 7);
                ChangeTriangleTile(1, 1, 3);
                break;
            case Maps.Neon:
                _emissionIntensity = 6;
                ChangeColor(23, 19, 19);
                ChangeEmissionColor(5, 5, 5);
                ExtraChangeColors(20, 20, 4, 19, 30);
                ExtraChangeEmissionColors(5, 5, 5, 5, 5);
                ChangeTriangleTile(30, 8, 0);
                break;
            case Maps.Promo1:
                _emissionIntensity = 3;
                ChangeColor(5, 7, 5);
                ChangeEmissionColor(7, 7, 7);
                ExtraChangeColors(5, 5, 3, 4, 5);
                ExtraChangeEmissionColors(7, 6, 5, 6, 6);
                ChangeTriangleTile(7, 2, 3);
                break;
            case Maps.Promo2:
                _emissionIntensity = 4;
                ChangeColor(14, 10, 11);
                ChangeEmissionColor(8, 8, 8);
                ExtraChangeColors(19, 19, 30, 4, 5);
                ExtraChangeEmissionColors(4, 6, 5, 4, 4);
                ChangeTriangleTile(12, 2, 3);
                break;
            case Maps.Promo3:
                _emissionIntensity = 3;
                ChangeColor(5, 7, 5);
                ChangeEmissionColor(7, 7, 7);
                ExtraChangeColors(5, 5, 3, 4, 5);
                ExtraChangeEmissionColors(7, 6, 5, 6, 6);
                ChangeTriangleTile(7, 2, 3);
                break;
            case Maps.Promo4:
                _emissionIntensity = 4;
                ChangeColor(14, 10, 11);
                ChangeEmissionColor(8, 8, 8);
                ExtraChangeColors(19, 19, 30, 4, 5);
                ExtraChangeEmissionColors(4, 6, 5, 4, 4);
                ChangeTriangleTile(12, 2, 3);
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
        foreach (var item in _mainFloors) { item.SetColor("_EmissionColor", _emissionColorPalletes[floorColor] * _emissionIntensity); }
        foreach (var item in _props) { item.SetColor("_EmissionColor", _emissionColorPalletes[propColor] * _emissionIntensity); }
        foreach (var item in _walls) { item.SetColor("_EmissionColor", _emissionColorPalletes[wallColor] * _emissionIntensity); }
    }

    private void ExtraChangeColors(int a, int b, int c, int d, int e)
    {
        _extraObjects[0].SetColor("_BaseColor", _colorPalletes[a]);
        _extraObjects[1].SetColor("_BaseColor", _colorPalletes[b]);
        _extraObjects[2].SetColor("_BaseColor", _colorPalletes[c]);
        _extraObjects[3].SetColor("_BaseColor", _colorPalletes[d]);
        _extraObjects[4].SetColor("_BaseColor", _colorPalletes[e]);
    }
    private void ExtraChangeEmissionColors(int a, int b, int c, int d, int e)
    {
        _extraObjects[0].SetColor("_EmissionColor", _emissionColorPalletes[a] * _emissionIntensity);
        _extraObjects[1].SetColor("_EmissionColor", _emissionColorPalletes[b] * _emissionIntensity);
        _extraObjects[2].SetColor("_EmissionColor", _emissionColorPalletes[c] * _emissionIntensity);
        _extraObjects[3].SetColor("_EmissionColor", _emissionColorPalletes[d] * _emissionIntensity);
        _extraObjects[4].SetColor("_EmissionColor", _emissionColorPalletes[e] * _emissionIntensity);
    }

    private void ChangeBridge(int number, int intensity)
    {
        _bridge.color = _bridgesColor[number];
        _bridge.SetColor("_EmissionColor", _bridgesColor[number + 1] * intensity);
    }

    private void ChangeTriangleTile(int baseColor, int emission, int intensity)
    {
        _triangleTile.SetColor("_BaseColor", _colorPalletes[baseColor]);
        _triangleTile.SetColor("_EmissionColor", _emissionColorPalletes[emission] * intensity);
    }
}

public enum Maps
{
    Null,
    Colizei,
    Neon,
    Promo1,
    Promo2,
    Promo3,
    Promo4
}