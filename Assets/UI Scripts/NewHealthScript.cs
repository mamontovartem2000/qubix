using DG.Tweening;
using ME.ECS;
using Project.Core.Features;
using Project.Core.Features.Player.Components;
using UnityEngine;
using UnityEngine.UI;

public class NewHealthScript : MonoBehaviour
{
    public GlobalEvent HealthChangedEvent;
    public Image Healthbar;

    private void Start()
    {
        HealthChangedEvent.Subscribe(HealthChanged);
    }
    
    private void HealthChanged(in Entity entity)
    {
        if(!SceneUtils.CheckLocalPlayer(entity)) return;

        var fill = entity.Read<PlayerHealth>().Value / 100;

        Healthbar.DOFillAmount(fill, 0.5f);
        Healthbar.color = Color.Lerp(Color.red, Color.green, fill);
    }

    private void OnDestroy()
    {
        HealthChangedEvent.Unsubscribe(HealthChanged);
    }
}
