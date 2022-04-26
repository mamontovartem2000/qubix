using DG.Tweening;
using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player;
using Project.Modules.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class NewWeaponryScript : MonoBehaviour
    {
        public GlobalEvent LeftAmmoChanged;
        public GlobalEvent RightAmmoChanged;
        public GlobalEvent RightWeaponDepleted;

        public Image LeftWeaponAmmoImage;
        public Image RightWeaponAmmoImage;

        public TextMeshProUGUI LeftWeaponAmmoText;
        public TextMeshProUGUI RightWeaponAmmoText;

        private void Start()
        {
            LeftAmmoChanged.Subscribe(RefreshLeftAmmo);
            RightAmmoChanged.Subscribe(RefreshRightAmmo);
            RightWeaponDepleted.Subscribe(ReloadRightWeapon);
        }

        private void RefreshLeftAmmo(in Entity player)
        {
            if(player != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.PlayerIdInRoom)) return;

            var entity = player.Read<PlayerAvatar>().Value.Read<WeaponEntities>().LeftWeapon;
            if (!entity.Has<LinearWeapon>()) return;
            
            var fill = (float)entity.Read<AmmoCapacity>().Value / entity.Read<AmmoCapacityDefault>().Value;
         
            LeftWeaponAmmoImage.fillAmount = fill;
            LeftWeaponAmmoText.SetText((fill * 100).ToString("###") + "%");
        }

        private void RefreshRightAmmo(in Entity player)
        {
            if(player != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.PlayerIdInRoom)) return;

            var entity = player.Read<PlayerAvatar>().Value.Read<WeaponEntities>().RightWeapon;
            
            var fill = (float)entity.Read<AmmoCapacity>().Value / entity.Read<AmmoCapacityDefault>().Value;
         
            RightWeaponAmmoImage.fillAmount = fill;
            RightWeaponAmmoText.SetText(entity.Read<AmmoCapacity>().Value.ToString());
        }
    
        private void ReloadRightWeapon(in Entity player)
        {
            if(player != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.PlayerIdInRoom)) return;
            
            var entity = player.Read<PlayerAvatar>().Value.Read<WeaponEntities>().RightWeapon;

            var time = entity.Read<ReloadTimeDefault>().Value;
        
            RightWeaponAmmoImage.fillAmount = 0;        

            RightWeaponAmmoImage.DOFillAmount(1, time);
        }

        private void OnDestroy()
        {
            LeftAmmoChanged.Unsubscribe(RefreshLeftAmmo);
            RightAmmoChanged.Unsubscribe(RefreshRightAmmo);
            RightWeaponDepleted.Unsubscribe(ReloadRightWeapon);
        }
    }
}
