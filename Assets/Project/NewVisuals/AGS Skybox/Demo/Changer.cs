using UnityEngine;

public class Changer : MonoBehaviour
{
    #region Public Fields

    public Material Preset_1;
    public Material Preset_2;
    public Material Preset_3;
    public Material Preset_4;
    public Material Preset_5;

    #endregion

    #region Public Methods

    public void SetPreset1()
    {
        RenderSettings.skybox = Preset_1;
    }

    public void SetPreset2()
    {
        RenderSettings.skybox = Preset_2;
    }

    public void SetPreset3()
    {
        RenderSettings.skybox = Preset_3;
    }

    public void SetPreset4()
    {
        RenderSettings.skybox = Preset_4;
    }

    public void SetPreset5()
    {
        RenderSettings.skybox = Preset_5;
    }

    #endregion
}