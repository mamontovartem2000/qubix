using DG.Tweening;
using ME.ECS;
using Project.Common.Components;
using Project.Core.Features;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewWeaponryScript : MonoBehaviour
{
    public GlobalEvent LeftAmmoChanged;
    public GlobalEvent RightAmmoChanged;
    public GlobalEvent RightWeaponDepleted;

    public Image LeftWeaponAmmoImage;
    public Image RightWeaponAmmoImage;

    public Image LeftWeaponIcon;
    public Image RightWeaponIcon;
    
    public TextMeshProUGUI LeftWeaponAmmoText;
    public TextMeshProUGUI RightWeaponAmmoText;

    private void Start()
    {
        LeftAmmoChanged.Subscribe(RefreshLeftAmmo);
        RightAmmoChanged.Subscribe(RefreshRightAmmo);
        RightWeaponDepleted.Subscribe(ReloadRightWeapon);
        
    }

    private void RefreshLeftAmmo(in Entity entity)
    {
        if (!SceneUtils.CheckLocalPlayer(entity.GetParent())) return;
        if (!entity.Has<LinearWeapon>()) return;

        var fill = (float)entity.Read<AmmoCapacity>().Value / entity.Read<AmmoCapacityDefault>().Value;
         
        LeftWeaponAmmoImage.fillAmount = fill;
        LeftWeaponAmmoText.SetText((fill * 100).ToString("###") + "%");
    }

    private void RefreshRightAmmo(in Entity entity)
    {
        if (!SceneUtils.CheckLocalPlayer(entity.GetParent())) return;
        
        var fill = (float)entity.Read<AmmoCapacity>().Value / entity.Read<AmmoCapacityDefault>().Value;
         
        RightWeaponAmmoImage.fillAmount = fill;
        RightWeaponAmmoText.SetText(entity.Read<AmmoCapacity>().Value.ToString());
    }
    
    private void ReloadRightWeapon(in Entity entity)
    {
        if (!SceneUtils.CheckLocalPlayer(entity.GetParent())) return;

        var time = entity.Read<ReloadTimeDefault>().Value;
        
        RightWeaponAmmoImage.fillAmount = 0;        
        RightWeaponIcon.fillAmount = 0;        

        RightWeaponAmmoImage.DOFillAmount(1, time);
        RightWeaponIcon.DOFillAmount(1, time);
    }

    private void OnDestroy()
    {
        LeftAmmoChanged.Unsubscribe(RefreshLeftAmmo);
        RightAmmoChanged.Unsubscribe(RefreshRightAmmo);
        RightWeaponDepleted.Unsubscribe(ReloadRightWeapon);
    }
}
