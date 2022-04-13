using DG.Tweening;
using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.Player.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class NewHealthScript : MonoBehaviour
    {
        public GlobalEvent HealthChangedEvent;
        public Image Healthbar;

        private void Start()
        {
            HealthChangedEvent.Subscribe(HealthChanged);
        }

        private void HealthChanged(in Entity player)
        {
            if(!SceneUtils.CheckLocalPlayer(player)) return;

            var entity = player.Read<PlayerAvatar>().Value;

            var fill = entity.Read<PlayerHealth>().Value / 100;

            Healthbar.DOFillAmount(fill, 0.5f);
            Healthbar.color = Color.Lerp(Color.red, Color.green, fill);
        }

        private void OnDestroy()
        {
            HealthChangedEvent.Unsubscribe(HealthChanged);
        }
    }
}
