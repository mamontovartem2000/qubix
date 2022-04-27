// using System;
// using System.Linq;
// using UnityEditor;
// using UnityEngine;
//
// [Serializable]
// public class AGSSkyboxMaterialEditor : MaterialEditor
// {
//     #region Private Fields
//
//     private Texture _Logo;
//
//     #endregion
//
//     #region Public Methods
//
//     public override void Awake()
//     {
//         base.Awake();
//         _Logo = Resources.Load("ags_logo") as Texture;
//     }
//
//     public override void OnInspectorGUI()
//     {
//         if (!isVisible)
//             return;
//
//         var targetMaterial = target as Material;
//         var keywords = targetMaterial.shaderKeywords;
//
//         GUI.skin.label.richText = true;
//         EditorGUIUtility.labelWidth = 170;
//
//         EditorGUI.BeginChangeCheck();
//
//         DrawLogo();
//
//         #region General
//
//         var skyColor = targetMaterial.GetColor("_SkyColor");
//         GUILayout.BeginVertical(GUI.skin.button);
//         skyColor = EditorGUILayout.ColorField("Sky Color", skyColor);
//         GUILayout.EndVertical();
//
//         #endregion
//
//         #region Nebula
//
//         var nebulaDensity = targetMaterial.GetFloat("_NebulaDensity");
//         var nebulaOffset = targetMaterial.GetFloat("_NebulaOffset");
//         var nebulaAnimSpeed = targetMaterial.GetFloat("_NebulaAnimSpeed");
//         var nebulaColor = targetMaterial.GetColor("_NebulaColor");
//
//         GUILayout.Space(3);
//         GUILayout.BeginVertical(GUI.skin.button);
//         GUILayout.Label("<b><size=13>NEBULA LAYER SETTINGS</size></b>");
//         GUILayout.Space(2);
//         var useNebula = keywords.Contains("AGS_USE_NEBULA_ON");
//         useNebula = GUILayout.Toggle(useNebula, " Enable");
//         if (!useNebula)
//             GUI.enabled = false;
//
//         nebulaDensity = EditorGUILayout.Slider("Density", nebulaDensity, 0, 1);
//         nebulaOffset = EditorGUILayout.Slider("Seed", nebulaOffset, 0, 1000);
//         nebulaAnimSpeed = EditorGUILayout.Slider("Animation Speed", nebulaAnimSpeed, 0, 0.5f);
//         nebulaColor = EditorGUILayout.ColorField("Color", nebulaColor);
//
//         GUI.enabled = true;
//         GUILayout.EndVertical();
//
//         #endregion
//
//         #region Clouds
//
//         var cloudDensity = targetMaterial.GetFloat("_CloudDensity");
//         var cloudVisibility = targetMaterial.GetFloat("_CloudVisibility");
//         var cloudSharpness = targetMaterial.GetFloat("_CloudSharpness");
//         var cloudOffset = targetMaterial.GetFloat("_CloudOffset");
//         var cloudAnimSpeed = targetMaterial.GetFloat("_CloudAnimSpeed");
//         var cloudColor = targetMaterial.GetColor("_CloudColor");
//
//         GUILayout.Space(3);
//         GUILayout.BeginVertical(GUI.skin.button);
//
//         GUILayout.Label("<b><size=13>CLOUDS LAYER SETTINGS</size></b>");
//         GUILayout.Space(2);
//         var useClouds = keywords.Contains("AGS_USE_CLOUDS_ON");
//         useClouds = GUILayout.Toggle(useClouds, " Enable");
//         if (!useClouds)
//             GUI.enabled = false;
//
//         cloudDensity = EditorGUILayout.Slider("Density", cloudDensity, 0, 10);
//         cloudVisibility = EditorGUILayout.Slider("Visibility", cloudVisibility, 1, 10);
//         cloudSharpness = EditorGUILayout.Slider("Sharpness", cloudSharpness, 0.5f, 1.5f);
//         cloudOffset = EditorGUILayout.Slider("Seed", cloudOffset, 0, 1000);
//         cloudAnimSpeed = EditorGUILayout.Slider("Animation Speed", cloudAnimSpeed, 0, 0.5f);
//         cloudColor = EditorGUILayout.ColorField("Color", cloudColor);
//
//         GUI.enabled = true;
//         GUILayout.EndVertical();
//
//         #endregion
//
//         #region Trails
//
//         var trailComplexity = targetMaterial.GetFloat("_TrailComplexity");
//         var trailVisibility = targetMaterial.GetFloat("_TrailVisibility");
//         var trailSize = targetMaterial.GetFloat("_TrailSize");
//         var trailOffset = targetMaterial.GetFloat("_TrailOffset");
//         var trailAnimSpeed = targetMaterial.GetFloat("_TrailAnimSpeed");
//         var trailColor = targetMaterial.GetColor("_TrailColor");
//
//         GUILayout.Space(3);
//         GUILayout.BeginVertical(GUI.skin.button);
//
//         GUILayout.Label("<b><size=13>TRAILS LAYER SETTINGS</size></b>");
//         GUILayout.Space(2);
//         var useTrails = keywords.Contains("AGS_USE_TRAILS_ON");
//         useTrails = GUILayout.Toggle(useTrails, " Enable");
//         if (!useTrails)
//             GUI.enabled = false;
//
//         trailComplexity = EditorGUILayout.Slider("Complexity", trailComplexity, 0.5f, 3);
//         trailVisibility = EditorGUILayout.Slider("Visibility", trailVisibility, 0, 0.9f);
//         trailSize = EditorGUILayout.Slider("Size", trailSize, 0, 1);
//         trailOffset = EditorGUILayout.Slider("Seed", trailOffset, 0, 1000);
//         trailAnimSpeed = EditorGUILayout.Slider("Animation Speed", trailAnimSpeed, 0, 2);
//         trailColor = EditorGUILayout.ColorField("Color", trailColor);
//
//         GUI.enabled = true;
//         GUILayout.EndVertical();
//
//         #endregion
//
//         #region Stars
//
//         var starsAmount = targetMaterial.GetFloat("_StarsAmount");
//         var starsLightRandomness = targetMaterial.GetFloat("_StarsLightRandomness");
//         var starsSize = targetMaterial.GetFloat("_StarsSize");
//         var starsColorVariation = targetMaterial.GetFloat("_StarsColorVariation");
//         var starsBlinkingSpeed = targetMaterial.GetFloat("_StarsBlinkingSpeed");
//         var starsColorOne = targetMaterial.GetColor("_StarsColorOne");
//         var starsColorTwo = targetMaterial.GetColor("_StarsColorTwo");
//
//         GUILayout.Space(3);
//         GUILayout.BeginVertical(GUI.skin.button);
//
//         GUILayout.Label("<b><size=13>STARS LAYER SETTINGS</size></b>");
//         GUILayout.Space(2);
//         var useStars = keywords.Contains("AGS_USE_STARS_ON");
//         useStars = GUILayout.Toggle(useStars, " Enable");
//         if (!useStars)
//             GUI.enabled = false;
//
//         starsAmount = EditorGUILayout.Slider("Amount", starsAmount, 0, 1);
//         starsLightRandomness = EditorGUILayout.Slider("Brightness Variation", starsLightRandomness, 0, 1);
//         starsSize = EditorGUILayout.Slider("Initial Size", starsSize, 0, 1);
//         starsColorVariation = EditorGUILayout.Slider("Color Variation", starsColorVariation, 0, 1);
//         starsBlinkingSpeed = EditorGUILayout.Slider("Blinking Speed", starsBlinkingSpeed, 0, 1);
//         starsColorOne = EditorGUILayout.ColorField("Base Color", starsColorOne);
//         starsColorTwo = EditorGUILayout.ColorField("Blinking Color", starsColorTwo);
//
//         GUI.enabled = true;
//         GUILayout.EndVertical();
//
//         #endregion
//
//         #region Saving
//
//         if (EditorGUI.EndChangeCheck())
//         {
//             var newKeywords = new string[]
//             {
//                 useNebula ? "AGS_USE_NEBULA_ON" : "AGS_USE_NEBULA_OFF",
//                 useClouds ? "AGS_USE_CLOUDS_ON" : "AGS_USE_CLOUDS_OFF",
//                 useTrails ? "AGS_USE_TRAILS_ON" : "AGS_USE_TRAILS_OFF",
//                 useStars ? "AGS_USE_STARS_ON" : "AGS_USE_STARS_OFF"
//             };
//             targetMaterial.shaderKeywords = newKeywords;
//
//             targetMaterial.SetColor("_SkyColor", skyColor);
//
//             targetMaterial.SetFloat("_NebulaDensity", nebulaDensity);
//             targetMaterial.SetFloat("_NebulaOffset", nebulaOffset);
//             targetMaterial.SetFloat("_NebulaAnimSpeed", nebulaAnimSpeed);
//             targetMaterial.SetColor("_NebulaColor", nebulaColor);
//
//             targetMaterial.SetFloat("_CloudDensity", cloudDensity);
//             targetMaterial.SetFloat("_CloudVisibility", cloudVisibility);
//             targetMaterial.SetFloat("_CloudSharpness", cloudSharpness);
//             targetMaterial.SetFloat("_CloudOffset", cloudOffset);
//             targetMaterial.SetFloat("_CloudAnimSpeed", cloudAnimSpeed);
//             targetMaterial.SetColor("_CloudColor", cloudColor);
//
//             targetMaterial.SetFloat("_TrailComplexity", trailComplexity);
//             targetMaterial.SetFloat("_TrailVisibility", trailVisibility);
//             targetMaterial.SetFloat("_TrailSize", trailSize);
//             targetMaterial.SetFloat("_TrailOffset", trailOffset);
//             targetMaterial.SetFloat("_TrailAnimSpeed", trailAnimSpeed);
//             targetMaterial.SetColor("_TrailColor", trailColor);
//
//             targetMaterial.SetFloat("_StarsAmount", starsAmount);
//             targetMaterial.SetFloat("_StarsLightRandomness", starsLightRandomness);
//             targetMaterial.SetFloat("_StarsSize", starsSize);
//             targetMaterial.SetFloat("_StarsColorVariation", starsColorVariation);
//             targetMaterial.SetFloat("_StarsBlinkingSpeed", starsBlinkingSpeed);
//             targetMaterial.SetColor("_StarsColorOne", starsColorOne);
//             targetMaterial.SetColor("_StarsColorTwo", starsColorTwo);
//
//             EditorUtility.SetDirty(targetMaterial);
//         }
//
//         #endregion
//     }
//
//     #endregion
//
//     #region Private Methods
//
//     private void DrawLogo()
//     {
//         GUILayout.BeginHorizontal();
//         GUILayout.FlexibleSpace();
//         GUILayout.Label(_Logo, GUILayout.Width(280), GUILayout.Height(102));
//         GUILayout.FlexibleSpace();
//         GUILayout.EndHorizontal();
//         GUILayout.Space(3);
//     }
//
//     #endregion
// }