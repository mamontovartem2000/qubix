using DG.Tweening;
using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player;
using Project.Core.Features.Player.Components;
using Project.Modules.Network;
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
            if(player != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(NetworkData.PlayerIdInRoom)) return;
            var entity = player.Read<PlayerAvatar>().Value;
            if(!entity.IsAlive()) return;

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
