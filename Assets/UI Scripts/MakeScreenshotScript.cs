using System.Collections;
using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;
using Project.Features.Player;
using Project.Modules.Network;
using UnityEngine;

public class MakeScreenshotScript : MonoBehaviour
{
    public GlobalEvent ScreenShot;
    [SerializeField] private GameObject UIStaticCore, UIDynamicCore;
    
    private void Start()
    {
        ScreenShot.Subscribe(UIControl);
    }

    private void UIControl(in Entity entity)
    {
        if (entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;

        UIStaticCore.SetActive(false);
        UIDynamicCore.SetActive(false);
        string date = System.DateTime.Now.ToString();
        date = date.Replace("/","-");
        date = date.Replace(" ","_");
        date = date.Replace(":","-");
        ScreenCapture.CaptureScreenshot( date + ".png", 2);
        StopAllCoroutines();
        StartCoroutine(UISetActiveOn());
    }

    private IEnumerator UISetActiveOn()
    {
        yield return new WaitForSeconds(1);
        UIStaticCore.SetActive(true);
        UIDynamicCore.SetActive(true);
    }

    private void OnDestroy()
    {
        ScreenShot.Unsubscribe(UIControl);
    }
}
