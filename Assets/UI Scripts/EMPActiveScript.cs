using ME.ECS;
using Project.Common.Components;
using Project.Features.Player;
using Project.Modules.Network;
using UnityEngine;

public class EMPActiveScript : MonoBehaviour
{
    public GlobalEvent EMPOn;
    public GlobalEvent EMPOff;
    public GameObject[] EMPImage;
    private void Start()
    {
        EMPOn.Subscribe(EMPActive);
        EMPOff.Subscribe(EMPInactive);
    }

    private void EMPActive(in Entity entity)
    {
        if (entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
        
        foreach (var image in EMPImage)
        {
            image.SetActive(true);
        }
    }
    private void EMPInactive(in Entity entity)
    {
        if (entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
        
        foreach (var image in EMPImage)
        {
            image.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        EMPOn.Unsubscribe(EMPActive);
        EMPOff.Unsubscribe(EMPInactive);

    }
}
